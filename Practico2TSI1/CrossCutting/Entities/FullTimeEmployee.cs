using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class FullTimeEmployee : Employee
    {
        [Column("SALARY")]
        public int Salary { get; set; }

        public override string ToString()
        {
            return (base.Id + " " + base.Name + " " + base.StartDate.ToString() + " " + Salary + " "+ this.GetType().Name);
        }
    }
}
