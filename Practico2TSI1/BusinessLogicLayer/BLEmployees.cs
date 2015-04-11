using DataAccessLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Exception;

namespace BusinessLogicLayer
{
    public class BLEmployees : IBLEmployees
    {
       private IDALEmployees _dal;

        public BLEmployees(IDALEmployees dal)
        {
            _dal = dal;
        }

        public void AddEmployee(Employee emp)
        {
            _dal.AddEmployee(emp);
        }

        public void DeleteEmployee(int id)
        {
            _dal.DeleteEmployee(id);
        }

        public void UpdateEmployee(Employee emp)
        {
            _dal.UpdateEmployee(emp);
        }

        public List<Employee> GetAllEmployees()
        {
            return _dal.GetAllEmployees();
        }

        public Employee GetEmployee(int id)
        {
            return _dal.GetEmployee(id);
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            return _dal.SearchEmployees(searchTerm);
        }

        public double CalcPartTimeEmployeeSalary(int idEmployee, int hours)
        {
            Employee e = _dal.GetEmployee(idEmployee);
            if (e != null)
            {
                if (e is FullTimeEmployee)
                {
                    throw new EmployeeExc("Warn:Empleado seleccionado es FullTime");
                }
                else
                {
                    return ((PartTimeEmployee)e).HourlyDate * hours;
                }
            }
            else
                throw new EmployeeExc("Warn:Empleado no existe");
            
        }

        public void InitChat()
        {
            WebsocketChat ws = new WebsocketChat();
        }

    }
}
