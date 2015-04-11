using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Entities
{
    public class PartTimeEmployee : Employee
    {
        [Column("RATE")]
        public double HourlyDate { get; set; }

        public override string ToString()
        {
            return (base.Id + " " + base.Name + " " + base.StartDate.ToString() + " " + HourlyDate + " " +this.GetType().Name);
        }
    }
}
