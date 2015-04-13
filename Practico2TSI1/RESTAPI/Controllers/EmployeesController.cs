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
using PresentationLayerConsole;


namespace RESTAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET api/employees
        public IEnumerable<Employee> Get()
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();

                IEnumerable<Employee> list = blHandler.GetAllEmployees();
                return list;
            }
        }

        // GET api/employees/5
        public IHttpActionResult Get(int id)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();
                try
                {
                    Employee e = blHandler.GetEmployee(id);
                    return Ok(e);
                }
                catch (Exception E)
                {
                    return NotFound();
                }
            }
        }

        // POST api/employees/PartTime
        [HttpPost]
        [Route("~/api/employees/PartTime")]
        public HttpResponseMessage PostPartTime([FromBody]PartTimeEmployee pte)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();
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
        }

        // POST api/employees/FullTime
        //[Route("api/employees/FullTime")]
        [HttpPost]
        [Route("~/api/employees/FullTime")]
        public HttpResponseMessage PostFullTime([FromBody]FullTimeEmployee fte)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();
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
        }

        // PUT api/employees/FullTime/{id}
        [HttpPut]
        [Route("~/api/employees/FullTime/{id}")]
        public HttpResponseMessage PutFullTime(int id, [FromBody]FullTimeEmployee fte)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();

                Employee emp = blHandler.GetEmployee(id);
                if (emp != null)
                {
                    if (fte != null)
                    {
                        emp.Id = id;
                        emp.Name = fte.Name;
                        emp.StartDate = fte.StartDate;
                        ((FullTimeEmployee)emp).Salary = fte.Salary;
                        blHandler.UpdateEmployee(emp);
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, fte);
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, id);
                }
            }
        }

        // PUT api/employees/PartTime/{id}
        [HttpPut]
        [Route("~/api/employees/PartTime/{id}")]
        public HttpResponseMessage PutPartTime(int id, [FromBody]PartTimeEmployee pte)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();

                Employee emp = blHandler.GetEmployee(id);
                if (emp != null)
                {
                    if (pte != null)
                    {
                        emp.Id = id;
                        emp.Name = pte.Name;
                        emp.StartDate = pte.StartDate;
                        ((PartTimeEmployee)emp).HourlyDate = pte.HourlyDate;
                        blHandler.UpdateEmployee(emp);
                        return Request.CreateResponse(HttpStatusCode.OK, emp);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotAcceptable, pte);
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, id);
                }
            }
        }

        [HttpGet]
        [Route("~/api/employees/CalcPartTime/{id}/{hours}")]
        public HttpResponseMessage CalcPartTime(int id, int hours)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();
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
        }

        [HttpGet]
        [Route("~/api/employees/searchEmployees/{searchTerm}")]
        public IEnumerable<Employee> SearchEmployees(string searchTerm)
        {
            using (UnityContainer container = new UnityContainer())
            {
                Helper.RegisterTypes(container);
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();
                IEnumerable<Employee> list = blHandler.SearchEmployees(searchTerm);
                return list;
            }

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
