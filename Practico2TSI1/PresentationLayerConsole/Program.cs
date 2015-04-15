using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using log4net;
using Shared.Exception;

namespace PresentationLayerConsole
{
    class Program
    {
        static string [] Commands = {
                                    "exit",
                                    "help", "AddFullTimeEmployee", "AddPartTimeEmployee",
                                    "DeleteEmployee", "UpdateEmployee", "GetAllEmployees",
                                    "GetEmployee", "SearchEmployees", "CalcPartTime"
                                    };
        
        static string[] Usage = {
                                 "","","<Name> <StartDate> <Salary>", "<Name> <StartDate> <HourlyDate>",
                                 "<Id>", "<Id> <Name> <StartDate> <Salary|HourlyDate>", "",
                                 "<Id>", "<Sentence>", "<Id> <Hours>"
                                 };
        static void PrintHelp()
        {
            System.Console.WriteLine("Available Commands:");
            for(int i=2; i<Commands.Length; i++)
            {
                System.Console.WriteLine(Commands[i] + " " + Usage[i]);
            }
 
        }

        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger("Logger");  

            using (UnityContainer container = new UnityContainer())
            {
                container.LoadConfiguration();
                IBLEmployees blHandler = container.Resolve<IBLEmployees>();

                Employee e;
                FullTimeEmployee fte;
                PartTimeEmployee p;

                PrintHelp();
                System.Console.Write(">");
                string line = Console.ReadLine();

                log4net.Config.XmlConfigurator.Configure();

                do
                {
                    string[] parameters = line.Split(new string[] { " " }, StringSplitOptions.None);                   

                    try
                    {
                        switch (parameters[0])
                        {
                            case "help":
                                PrintHelp();
                                break;

                            case "AddFullTimeEmployee":
                                fte = new FullTimeEmployee();
                                foreach (string s in parameters)
                                {
                                    System.Console.WriteLine(s);
                                }
                                fte.Name = parameters[1];
                                fte.StartDate = Convert.ToDateTime(parameters[2]);
                                fte.Salary = Int32.Parse(parameters[3]);
                                blHandler.AddEmployee(fte);
                                break;

                            case "AddPartTimeEmployee":
                                p = new PartTimeEmployee();
                                p.Name = parameters[1];
                                p.StartDate = Convert.ToDateTime(parameters[2]);
                                p.HourlyDate = Int32.Parse(parameters[3]);
                                blHandler.AddEmployee(p);
                                break;

                            case "DeleteEmployee":
                                blHandler.DeleteEmployee(Int32.Parse(parameters[1]));
                                break;

                            case "UpdateEmployee":
                                Employee emp = blHandler.GetEmployee(Int32.Parse(parameters[1]));
                                if (emp != null)
                                {
                                    //emp.Id = Int32.Parse(parameters[1]);
                                    emp.Name = parameters[2];
                                    emp.StartDate = Convert.ToDateTime(parameters[3]);
                                    int salary = Int32.Parse(parameters[4]);
                                    if (emp is FullTimeEmployee)
                                        ((FullTimeEmployee)emp).Salary = salary;
                                    else
                                        ((PartTimeEmployee)emp).HourlyDate = salary;
                                    blHandler.UpdateEmployee(emp);
                                }
                                else
                                {
                                    throw new Exception("Error: ID empleado no existe.");
                                }
                                break;

                            case "GetAllEmployees":
                                List<Employee> list = blHandler.GetAllEmployees();
                                foreach (Employee l in list)
                                {
                                    System.Console.WriteLine(l.ToString());
                                }
                                break;

                            case "GetEmployee":
                                e = blHandler.GetEmployee(Int32.Parse(parameters[1]));
                                if (e != null)
                                {
                                    System.Console.WriteLine(e.ToString());
                                }
                                else
                                {
                                    System.Console.WriteLine("No existe empleado.");
                                }
                                break;

                            case "SearchEmployees":
                                list = blHandler.SearchEmployees(parameters[1]);
                                foreach (Employee l in list)
                                {
                                    System.Console.WriteLine(l.ToString());
                                }
                                break;

                            case "CalcPartTime":
                                int id = Int32.Parse(parameters[1]);
                                int hours = Int32.Parse(parameters[2]);
                                double mount = blHandler.CalcPartTimeEmployeeSalary(id, hours);
                                System.Console.WriteLine(mount);
                                break;

                            default:
                                Console.WriteLine("Invalid Command!");
                                break;
                        }

                    }
                    catch (EmployeeExc exc)
                    {
                        log.Warn(exc.Message,exc);
                        
                        System.Console.WriteLine(exc.Message);
                    }
                    catch (Exception E)
                    {
                        log.Error("Error no controlado:", E);

                    }
                   


                    System.Console.Write(">");
                    line = System.Console.ReadLine();

                } while (!line.Equals("exit"));


            }
            
        }
    }
}

