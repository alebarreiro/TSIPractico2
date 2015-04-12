using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using DataAccessLayer;
using Shared.Entities;

namespace MVCPresentationLayer.Controllers
{
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
        public ActionResult AgregarEmpleado()
        {

            return Json(new { success = "Valid" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Employees/ModificarEmpleado
        public ActionResult ModificarEmpleado()
        {
            return View("ModificarEmpleado");
        }

        // GET: Employees/BorrarEmpleado
        public ActionResult BorrarEmpleado()
        {
            return View("BorrarEmpleado");
        }

    }
}