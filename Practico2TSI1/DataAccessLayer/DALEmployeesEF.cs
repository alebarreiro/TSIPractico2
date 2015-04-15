using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace DataAccessLayer
{
    public class EmployeesEFContext : DbContext
    {
        public EmployeesEFContext()
           // : base("name=Employees")
        {
            var ddlcopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FullTimeEmployee>().Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("FULL_TIME_EMP");
                    m.Property(p => p.Id).HasColumnName("ID");
                    m.Property(p => p.Name).HasColumnName("NAME");
                    m.Property(p => p.StartDate).HasColumnName("START_DATE");
                    m.Property(p => p.Salary).HasColumnName("SALARY");
                    m.Property(p => p.Email).HasColumnName("EMAIL");
                    m.Property(p => p.Password).HasColumnName("PASSWORD");
                });

            modelBuilder.Entity<PartTimeEmployee>().Map(m =>
                {
                    m.MapInheritedProperties();
                    m.ToTable("PART_TIME_EMP");
                    m.Property(p => p.Id).HasColumnName("ID");
                    m.Property(p => p.Name).HasColumnName("NAME");
                    m.Property(p => p.StartDate).HasColumnName("START_DATE");
                    m.Property(p => p.HourlyDate).HasColumnName("RATE");
                    m.Property(p => p.Email).HasColumnName("EMAIL");
                    m.Property(p => p.Password).HasColumnName("PASSWORD");
                });
        }
    }

    public class ContextInitialize : System.Data.Entity.DropCreateDatabaseAlways<EmployeesEFContext>
    {
        protected override void Seed(EmployeesEFContext context)
        {
            FullTimeEmployee[] FEmployees = { new FullTimeEmployee{Id = 1, Name="Alguien", StartDate = Convert.ToDateTime("10-12-2015") } };
            foreach (FullTimeEmployee f in FEmployees)
            {
                context.Employees.Add(f);
            }
            context.SaveChanges();
        }
    }



    public class DALEmployeesEF : IDALEmployees
    {
        public void AddEmployee(Employee emp)
        {
            using (var context = new EmployeesEFContext())
            {
                try
                {
                    //id problem
                    var query = context.Employees.OrderByDescending(e => e.Id).FirstOrDefault();
                    if (query == null)
                        emp.Id = 1;
                    else
                        emp.Id = query.Id + 1;
                    context.Employees.Add(emp);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var context = new EmployeesEFContext())
            {
                try 
                { 
                    var query = from e in context.Employees
                                where e.Id == id
                                select e;
                    Employee emp = query.First();
                    context.Employees.Remove(emp);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public void UpdateEmployee(Employee emp)
        {
            Debug.WriteLine("update employee:" + emp.Id + emp.Name);
            using (var context = new EmployeesEFContext())
            {
                try
                {
                    var query = from e in context.Employees
                                where e.Id == emp.Id
                                select e;
                    foreach (Employee e in query)
                    {
                        e.Name = emp.Name;
                        e.StartDate = emp.StartDate;
                        e.Email = emp.Email;
                        e.Password = emp.Password;
                        if (emp is FullTimeEmployee)
                        {
                            ((FullTimeEmployee)e).Salary = ((FullTimeEmployee)emp).Salary;
                        }
                        else
                        {
                            ((PartTimeEmployee)e).HourlyDate = ((PartTimeEmployee)emp).HourlyDate;
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            using(var context = new EmployeesEFContext()){
                try
                {
                    var query = from e in context.Employees
                                orderby e.Id
                                select e;
                    return query.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public Employee GetEmployee(int id)
        {
            using (var context = new EmployeesEFContext())
            {
                try
                {
                    var query = from e in context.Employees
                                where e.Id == id
                                select e;
                    return query.First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public List<Employee> SearchEmployees(string searchTerm)
        {
            Debug.WriteLine("busqueda:" + searchTerm);
            using (var context = new EmployeesEFContext())
            {
                try
                {
                    var query = from e in context.Employees
                                where e.Name.Equals(searchTerm)
                                select e;
                    return query.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public Employee GetEmployeeByEmail(string email)
        {
            using (var context = new EmployeesEFContext())
            {
                try
                {
                    var query = from e in context.Employees
                                where e.Email.Equals(email)
                                select e;
                    if (query.Count() > 0)
                        return query.First();
                    else
                        return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

    }
}
