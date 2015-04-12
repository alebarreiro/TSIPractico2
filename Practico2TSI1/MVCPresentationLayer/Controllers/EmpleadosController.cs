using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Empleados/AgregarEmpleado
        public ActionResult AgregarEmpleado(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: /Empleados/BorrarEmpleado
        public ActionResult BorrarEmpleado(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // GET: /Empleados/ModificarEmpleado
        public ActionResult ModificarEmpleado(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}