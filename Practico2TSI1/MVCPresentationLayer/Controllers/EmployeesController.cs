using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVCPresentationLayer.Controllers
{

    [Serializable]
    public class DatosAgregarEmpleado
    {
        public string nombre { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string tipoEmpleado { get; set; }
        public Int32 salario { get; set; }

    }

    [Serializable]
    public class DatosBorrarEmpleado
    {
        public Int32 idEmpleado { get; set; }
    }

    public class EmployeesController : Controller
    {
        

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
            
            if (Request.IsAjaxRequest())
            {
                if (nuevoEmpleado.tipoEmpleado.Equals("partTime"))
                {
                    PartTimeEmployee partEmp = new PartTimeEmployee();
                    partEmp.StartDate = DateTime.Now;
                    partEmp.Name = nuevoEmpleado.nombre;
                    partEmp.Email = nuevoEmpleado.mail;
                    partEmp.Password = nuevoEmpleado.password;
                    partEmp.HourlyDate = nuevoEmpleado.salario;
                    //faltaria la parte del usuario
                    IBLEmployees bl = new BLEmployees(new DALEmployeesEF());
                    try
                    {
                        bl.AddEmployee(partEmp);
                    }
                    catch (Exception e)
                    {
                        //devolver error json
                        Dictionary<string, object> error = new Dictionary<string, object>();
                        error.Add("ErrorCode", -1);
                        error.Add("ErrorMessage", "Something really bad happened");
                        Console.WriteLine(e.Message);
                        return Json(error);
                        
                    }
                    
                }
                else if (nuevoEmpleado.tipoEmpleado.Equals("fullTime"))
                {
                    FullTimeEmployee fullEmp = new FullTimeEmployee();
                    fullEmp.Name = nuevoEmpleado.nombre;
                    fullEmp.Salary = nuevoEmpleado.salario;
                    fullEmp.Email = nuevoEmpleado.mail;
                    fullEmp.Password = nuevoEmpleado.password;
                    fullEmp.StartDate = DateTime.Now;
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
                        Console.WriteLine(e.Message);
                        error.Add("ErrorMessage", "Something really bad happened");
                        return Json(error);
                    }
                }
                else
                {
                    //devolver error json
                    Dictionary<string, object> error = new Dictionary<string, object>();
                    error.Add("ErrorCode", -1);
                    error.Add("ErrorMessage", "Something really bad happened");
                    return Json(error);
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
                        Debug.WriteLine(e.Message);
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
                        Debug.WriteLine(e.Message);
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
                    Debug.WriteLine(e.Message);
                    error.Add("ErrorMessage", "Something really bad happened");
                    return Json(error);
                }
            }
            else
            {
                Dictionary<string, object> error = new Dictionary<string, object>();
                Debug.WriteLine("No entra");
                error.Add("ErrorCode", -1);
                error.Add("ErrorMessage", "Something really bad happened");
                return Json(error);
            }
        }

    }
}