using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Entities;
using RestSharp;
using RestSharp.Deserializers;
using System.Configuration;

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
            var request = new RestRequest("api/employees", Method.POST);
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
            var request = new RestRequest("api/employees", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(emp);
            client.Execute(request);
        }

        public List<Employee> GetAllEmployees()
        {
            var request = new RestRequest("api/employees", Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<List<Employee>>(request);
            return response.Data;
        }

        public Employee GetEmployee(int id)
        {
            var request = new RestRequest("api/employees/{id}", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("id", id.ToString());
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                try
                {
                    var ob = deserial.Deserialize<PartTimeEmployee>(response);
                    return ob;
                }
                catch (Exception e)
                {
                    var ob = deserial.Deserialize<FullTimeEmployee>(response);
                    return ob;
                }
            }
            else
            {
                return null;
            }
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            var request = new RestRequest("api/employees", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("searchterm", searchTerm);
            var response = client.Execute<List<Employee>>(request);
            return response.Data;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            var request = new RestRequest("api/employees", Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddUrlSegment("email", email);
            var response = client.Execute(request);
            if (response.ErrorException != null)
            {
                RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
                try
                {
                    var ob = deserial.Deserialize<PartTimeEmployee>(response);
                    return ob;
                }
                catch (Exception e)
                {
                    var ob = deserial.Deserialize<FullTimeEmployee>(response);
                    return ob;
                }
            }
            else
            {
                return null;
            }
        }


    }
}
