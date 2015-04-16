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
        [Serializable]
        public class DatosAgregarEmpleado{
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
            IBLEmployees ibl = new BLEmployees(new DALEmployeesEF());
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
            Debug.WriteLine(nuevoEmpleado.mail);
            Debug.WriteLine(nuevoEmpleado.nombre);
            Debug.WriteLine(nuevoEmpleado.salario);
            Debug.WriteLine(nuevoEmpleado.mail);

            if (Request.IsAjaxRequest())
            {
                if (nuevoEmpleado.tipoEmpleado.Equals("partTime"))
                {
                    PartTimeEmployee partEmp = new PartTimeEmployee();
                    partEmp.StartDate = DateTime.Now;
                    partEmp.Name = nuevoEmpleado.nombre;
                    partEmp.HourlyDate = nuevoEmpleado.salario;
                    partEmp.FirstLogin = true;
                    //faltaria la parte del usuario
                    IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                    try
                    {
                        bl.AddEmployee(partEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Debug.WriteLine("ERROR!");
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
                    fullEmp.FirstLogin = true;
                    //falta la parte del usuario
                    IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                    try
                    {
                        bl.AddEmployee(fullEmp);
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
                    //faltaria la parte del usuario
                    IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                    try
                    {
                        bl.UpdateEmployee(partEmp);
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
                    //falta la parte del usuario
                    IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                    try
                    {
                        bl.UpdateEmployee(fullEmp);
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
                IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                try
                {
                    bl.DeleteEmployee(empBorrar.idEmpleado);
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

    }
}