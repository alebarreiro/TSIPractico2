using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using RestSharp;
using RestSharp.Deserializers;
using System.Configuration;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataAccessLayer.Transformers;

//GET POST DELETE
namespace DataAccessLayer
{
    public class DALEmployeesREST: IDALEmployees
    {
        private string RESTURL;
        private RestClient client;

        public DALEmployeesREST()
        {
            RESTURL = ConfigurationManager.AppSettings["RESTURL"];
            client = new RestClient(RESTURL);
        }

        public void AddEmployee(Employee emp)
        {
            RestRequest request = null; ;
            if (emp is PartTimeEmployee)
            {
                request = new RestRequest("api/employees/PartTime", Method.POST);
            }
            else
            {
                request = new RestRequest("api/employees/FullTime", Method.POST);
            }
            request.RequestFormat = DataFormat.Json;
            request.AddBody(emp);
            client.Execute(request);
        }

        public void DeleteEmployee(int id)
        {
            var request = new RestRequest("api/employees/{id}", Method.DELETE);
            request.AddUrlSegment("id",id.ToString());
            request.RequestFormat = DataFormat.Json;
            client.Execute(request);
        }

        public void UpdateEmployee(Employee emp)
        {
            RestRequest request = null; ;
            if (emp is PartTimeEmployee)
            {
                request = new RestRequest("api/employees/PartTime/{id}", Method.PUT);
                //request.AddUrlSegment("id", emp.Id.ToString());
                request.AddBody((PartTimeEmployee)emp);
            }
            else
            {
                request = new RestRequest("api/employees/FullTime/{id}", Method.PUT);
                //request.AddUrlSegment("id", emp.Id.ToString());
                request.AddBody((FullTimeEmployee)emp);
            }
            request.RequestFormat = DataFormat.Json;
            client.Execute(request);
        }

        public List<Employee> GetAllEmployees()
        {
            var request = new RestRequest("api/employees", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<List<string>>(request);
            var converter = new EmployeeConverter();
            List<Employee> list = new List<Employee>();


            foreach (var e in response.Data)
            {
                Debug.WriteLine(e);
                Employee emp = JsonConvert.DeserializeObject<Employee>(e, converter);
                list.Add(emp);
            }

            return list;
        }

        public Employee GetEmployee(int id)
        {
            var request = new RestRequest("api/employees/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var response = client.Execute(request);

            Employee e = null;

            var converter = new EmployeeConverter();

            if (response.Content.Equals("null"))
            {
                return null;
            }
            else
            {
                e = JsonConvert.DeserializeObject<Employee>(response.Content, converter);
                return e;
            }
            
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            var request = new RestRequest("api/employees/searchEmployees/{search}", Method.GET);
            request.AddUrlSegment("search", searchTerm);

            var response = client.Execute<List<string>>(request);
            Debug.WriteLine("contenido" + response.Content);
            var converter = new EmployeeConverter();
            List<Employee> list = new List<Employee>();

            foreach (var e in response.Data)
            {
                Debug.WriteLine(e);
                Employee emp = JsonConvert.DeserializeObject<Employee>(e, converter);
                list.Add(emp);
            }

            return list;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            var request = new RestRequest("api/employees/{email}", Method.GET);
            request.AddUrlSegment("email", email);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            Debug.WriteLine(response.Content);
            Employee e = null;

            var converter = new EmployeeConverter();

            if (response.Content.Equals("null"))
            {
                return null;
            }
            else
            {
                e = JsonConvert.DeserializeObject<Employee>(response.Content, converter);
                return e;
            }
        }


    }
}
