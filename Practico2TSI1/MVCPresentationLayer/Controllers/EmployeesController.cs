using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;

namespace MVCPresentationLayer.Controllers
{
    public class EmployeesController : Controller
    {
        private IBLEmployees ibl = new BLEmployees(new DALEmployeesREST());

        [Serializable]
        public class DatosAgregarEmpleado{
            public int id { get; set; }
            public string nombre { get; set; }
            public string mail { get; set; }
            public string tipoEmpleado { get; set; }
            public int salario { get; set; }
        }

        [Serializable]
        public class DatosBorrarEmpleado{
            public int idEmpleado { get; set; }
        }

        // GET: Employees
        public ActionResult Index()
        {
            List<Employee> empleados = ibl.GetAllEmployees();
            ViewBag.Empleados = empleados;
            ViewBag.Pagina = "Empleados";
            if (empleados.Count() == 0)
            {
                ViewBag.HayEmpleados = false;
            }
            else
            {
                ViewBag.HayEmpleados = true;
            }
            return View("ListarEmpleados");
        }

        // GET: Employees/ListarEmpleados
        public ActionResult ListarEmpleados()
        {
            
            return View("ListarEmpleados");
        }

        // GET: Employees/AgregarEmpleado
        [HttpPost]
        public ActionResult AgregarEmpleado(DatosAgregarEmpleado nuevoEmpleado)
        {

            if (Request.IsAjaxRequest())
            {
                if (nuevoEmpleado.tipoEmpleado.Equals("partTime"))
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var random = new Random();
                    var pass = new string(Enumerable.Repeat(chars, 6)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());
                    PartTimeEmployee partEmp = new PartTimeEmployee();
                    partEmp.StartDate = DateTime.Now;
                    partEmp.Name = nuevoEmpleado.nombre;
                    partEmp.HourlyDate = nuevoEmpleado.salario;
                    partEmp.FirstLogin = true;
                    partEmp.Password = pass;
                    partEmp.Email = nuevoEmpleado.mail;
                    //faltaria la parte del usuario
                   
                    try
                    {
                        ibl.AddEmployee(partEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Dictionary<string, object> error = new Dictionary<string, object>();
                        error.Add("ErrorCode", -1);
                        error.Add("ErrorMessage", "Something really bad happened");
                        return Json(error);
                    }
                    
                }
                else if (nuevoEmpleado.tipoEmpleado.Equals("fullTime"))
                {
                    var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var random = new Random();
                    var pass = new string(Enumerable.Repeat(chars, 6)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());
                    FullTimeEmployee fullEmp = new FullTimeEmployee();
                    fullEmp.Name = nuevoEmpleado.nombre;
                    fullEmp.Salary = nuevoEmpleado.salario;
                    fullEmp.StartDate = DateTime.Now;
                    fullEmp.FirstLogin = true;
                    fullEmp.Password = pass;
                    fullEmp.Email = nuevoEmpleado.mail;
                    //falta la parte del usuario
                    
                    try
                    {
                        ibl.AddEmployee(fullEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Dictionary<string, object> error = new Dictionary<string, object>();
                        error.Add("ErrorCode", -1);
                        error.Add("ErrorMessage", "Something really bad happened");
                        return Json(error);
                    }
                }
                else
                {
                    //devolver error
                }
                return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened");
                return Json(error);
            }
        }

        // GET: Employees/ModificarEmpleado
        [HttpPost]
        public ActionResult ModificarEmpleado(DatosAgregarEmpleado nuevoEmpleado)
        {
            if (Request.IsAjaxRequest())
            {
                if (nuevoEmpleado.tipoEmpleado.Equals("partTime"))
                {
                    PartTimeEmployee partEmp = new PartTimeEmployee();
                    partEmp.StartDate = DateTime.Now;
                    partEmp.Name = nuevoEmpleado.nombre;
                    partEmp.HourlyDate = nuevoEmpleado.salario;
                    partEmp.Email = nuevoEmpleado.mail;
                    partEmp.Id = nuevoEmpleado.id;
                    //faltaria la parte del usuario
                    try
                    {
                        ibl.UpdateEmployee(partEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Dictionary<string, object> error = new Dictionary<string, object>();
                        error.Add("ErrorCode", -1);
                        error.Add("ErrorMessage", "Something really bad happened");
                        return Json(error);
                    }

                }
                else if (nuevoEmpleado.tipoEmpleado.Equals("fullTime"))
                {
                    FullTimeEmployee fullEmp = new FullTimeEmployee();
                    fullEmp.Name = nuevoEmpleado.nombre;
                    fullEmp.Salary = nuevoEmpleado.salario;
                    fullEmp.StartDate = DateTime.Now;
                    fullEmp.Email = nuevoEmpleado.mail;
                    fullEmp.Id = nuevoEmpleado.id;
                    //falta la parte del usuario
                    try
                    {
                        ibl.UpdateEmployee(fullEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Dictionary<string, object> error = new Dictionary<string, object>();
                        error.Add("ErrorCode", -1);
                        error.Add("ErrorMessage", "Something really bad happened");
                        return Json(error);
                    }
                }
                else
                {
                    //devolver error
                }
                return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened");
                return Json(error);
            }
        }

        // GET: Employees/BorrarEmpleado
        [HttpPost]
        public ActionResult BorrarEmpleado(DatosBorrarEmpleado empBorrar)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    ibl.DeleteEmployee(empBorrar.idEmpleado);
                    return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    Dictionary<string, object> error = new Dictionary<string, object>();
                    error.Add("ErrorCode", -1);
                    error.Add("ErrorMessage", "Something really bad happened");
                    return Json(error);
                }
            }
            else
            {
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened");
                return Json(error);
            }
        }

        // GET: Employees/CalcPartTime/{id}
        [HttpGet]
        public ActionResult CalcPartTime(int id)
        {
            IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
            Employee e = bl.GetEmployee(id);
            if (e != null)
            {
                if (e is PartTimeEmployee)
                {
                    e = (PartTimeEmployee)e;
                    return Json(new { success = "Valid", hourlyRate = ((PartTimeEmployee)e).HourlyDate, partTime = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = "Valid", partTime = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Dictionary<string, object> error = new Dictionary<string, object>();
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened.");
                return Json(error);
            }
        }

    }
}