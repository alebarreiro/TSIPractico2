using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DALEmployeesMock : IDALEmployees
    {
        private List<Employee> employeesRepository = new List<Employee>()
        {
            new PartTimeEmployee(){Id=1, HourlyDate = 100},
            new PartTimeEmployee(){Id=2, HourlyDate = 150},
            new PartTimeEmployee(){Id=3, HourlyDate = 200},
            new PartTimeEmployee(){Id=4, HourlyDate = 250},
            new PartTimeEmployee(){Id=5, HourlyDate = 300},
            new FullTimeEmployee(){},
            new FullTimeEmployee(){},
            new FullTimeEmployee(){},
            new FullTimeEmployee(){},
            new FullTimeEmployee(){},
        };

        public void AddEmployee(Employee emp)
        {
            employeesRepository.Add(emp);
        }

        public void DeleteEmployee(int id)
        {
            Employee e = employeesRepository.Where(n => n.Id == id).First();
            employeesRepository.Remove(e);        
        }

        public void UpdateEmployee(Employee emp)
        {
            foreach(Employee e in employeesRepository.Where(n=> n.Id == emp.Id)){
                e.Name = emp.Name;
                e.StartDate = emp.StartDate;
            }       
        }

        public List<Employee> GetAllEmployees()
        {
            return employeesRepository;
        }

        public Employee GetEmployee(int id)
        {
            return employeesRepository.Where(n => n.Id == id).First();
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            return employeesRepository.Where(e => e.Name.Equals(searchTerm)).ToList();
        }

        public Employee GetEmployeeByEmail(string email)
        {
            throw new NotImplementedException();
        }

    }
}
