using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCPresentationLayer.Models;
using DataAccessLayer;
using Shared.Entities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Security;

namespace MVCPresentationLayer.Controllers
{
    [Serializable]
    public class LogForm
    {
        public string mail { get; set; }
        public string password { get; set; }
    }


   // [Authorize]
    public class AccountController : Controller
    {
        private IDALEmployees dal = new DALEmployeesREST();
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
     //   [AllowAnonymous]
      //  [ValidateAntiForgeryToken]
        public ActionResult Login(LogForm model)
        {

            if (Request.IsAjaxRequest())
            {
                Debug.WriteLine("MVCLAYER:");
                Employee e = dal.GetEmployeeByEmail(model.mail);

                if (e != null)
                {
                    Debug.WriteLine(e.Email);
                    Debug.WriteLine(e.Password);
                }

                if (e == null||!e.Password.Equals(model.password))
                {

                    //devolver error json
                    Dictionary<string, object> error = new Dictionary<string, object>();
                    error.Add("ErrorCode", -1);
                    error.Add("ErrorMessage", "Usuario o contraseña invalidos.");
                    return Json(error);
                }
                FormsAuthentication.SetAuthCookie(e.Name, false);
                ViewBag.LoggedIn = true;
                return Redirect("~/Employees");
            }
            //return Redirect("~/Employees");
            Dictionary<string, object> valid = new Dictionary<string, object>();
            valid.Add("success", "Valid");
            ViewBag.LoggedIn = true;
            //return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
            return Json(valid);
        }

        // POST: /Account/LogOff

        [HttpPost]
        public ActionResult Logout()
        {

            Debug.WriteLine("LogOFF");
            AuthenticationManager.SignOut();
            //not cache
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session.Clear();  // This may not be needed -- but can't hurt
            Session.Abandon();

            // Clear authentication cookie
            HttpCookie rFormsCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            rFormsCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rFormsCookie);

            // Clear session cookie 
            HttpCookie rSessionCookie = new HttpCookie("ASP.NET_SessionId", "");
            rSessionCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(rSessionCookie);
            return RedirectToAction("Index", "Home");

        }





        public ActionResult UpdatePassword(LogForm model)
        {

            if (Request.IsAjaxRequest())
            {
                Employee e = dal.GetEmployeeByEmail(model.mail);
                e.Password = model.password;
                try
                {
                    dal.UpdateEmployee(e);
                }
                catch (Exception E)
                {
                    //devolver error json
                    Dictionary<string, object> error = new Dictionary<string, object>();
                    error.Add("ErrorCode", -1);
                    error.Add("ErrorMessage", "No se pudo actualizar contraseña");
                    return Json(error);
                }


                return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //devolver error json
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened");
                return Json(error);
            }
            

        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}