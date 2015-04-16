using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Shared.Entities;
using BusinessLogicLayer;

//remove
using System.Diagnostics;

namespace RESTAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private IBLEmployees blHandler;

        public EmployeesController()
        {
            using (UnityContainer container = new UnityContainer())
            {
                container.LoadConfiguration();
                blHandler = container.Resolve<IBLEmployees>();
            }
        }

        // GET api/employees
        public List<Employee> Get()
        {
            List<Employee> list = blHandler.GetAllEmployees();
            Debug.WriteLine("RESTAPI::EMPLEADOS:");
            foreach (var e in list)
            {
                Debug.WriteLine(e.Id);
            }
            return list;
            
        }

        // GET api/employees/5
        public Employee Get(int id)
        {
            try
            {
                return blHandler.GetEmployee(id);
            }
            catch (Exception E)
            {
                return null;
            }     
        }

        // GET api/employees/5
        [Route("~/api/employees/mail/{email}")]
        public Employee Get(string email)
        {
            try
            {
                Employee e = blHandler.GetEmployeeByEmail(email);
                return e;

            }
            catch (Exception E)
            {
                return null;

            }
        }

        // POST api/employees/PartTime
        [HttpPost]
        [Route("~/api/employees/PartTime")]
        public HttpResponseMessage PostPartTime([FromBody]PartTimeEmployee pte)
        {
            if (pte != null)
            {
                try
                {
                    blHandler.AddEmployee(pte);
                    var response = Request.CreateResponse(HttpStatusCode.Created);
                    return response;
                }
                catch (Exception E)
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, pte);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, pte);
            }
            
        }

        // POST api/employees/FullTime
        //[Route("api/employees/FullTime")]
        [HttpPost]
        [Route("~/api/employees/FullTime")]
        public HttpResponseMessage PostFullTime([FromBody]FullTimeEmployee fte)
        {    
            if (fte != null)
            {
                try
                {
                    blHandler.AddEmployee(fte);
                    var response = Request.CreateResponse(HttpStatusCode.Created);
                    return response;
                }
                catch (Exception E)
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, fte);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable, fte);
            }      
        }

        // PUT api/employees/FullTime/{id}
        [HttpPut]
        [Route("~/api/employees/FullTime")]
        public HttpResponseMessage PutFullTime([FromBody]FullTimeEmployee fte)
        {
            Employee emp = blHandler.GetEmployee(fte.Id);
            if (emp != null)
            {
                if (fte != null)
                {
                    blHandler.UpdateEmployee(fte);
                    return Request.CreateResponse(HttpStatusCode.OK, fte);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, fte);
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, fte.Id);
            }            
        }

        // PUT api/employees/PartTime/{id}
        [HttpPut]
        [Route("~/api/employees/PartTime")]
        public HttpResponseMessage PutPartTime([FromBody]PartTimeEmployee pte)
        {
            Debug.WriteLine("REST:" + pte.Id+ pte.Name);
            Employee emp = blHandler.GetEmployee(pte.Id);
            if (emp != null)
            {
                if (pte != null)
                {
                    blHandler.UpdateEmployee(pte);
                    return Request.CreateResponse(HttpStatusCode.OK, pte);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, pte);
                }

            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, pte.Id);
            }            
        }

        [HttpGet]
        [Route("~/api/employees/CalcPartTime/{id}/{hours}")]
        public HttpResponseMessage CalcPartTime(int id, int hours)
        {
            try
            {
                Employee e = blHandler.GetEmployee(id);
                double mount = blHandler.CalcPartTimeEmployeeSalary(id, hours);
                return Request.CreateResponse(HttpStatusCode.OK, mount);
            }
            catch (Exception E)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }           
        }

        [HttpGet]
        [Route("~/api/employees/searchEmployees/{searchTerm}")]
        public List<Employee> SearchEmployees(string searchTerm)
        {
                List<Employee> list = blHandler.SearchEmployees(searchTerm);
                return list;
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                blHandler.DeleteEmployee(id);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception E)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, id);
            }
        }
    }
}
