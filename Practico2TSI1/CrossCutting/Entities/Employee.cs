using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{

    public abstract class Employee
    {
        //[Column("ID"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column("NAME"), MaxLength(255)]
        public string Name { get; set; }

        [Column("START_DATE")]
        public DateTime StartDate { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("FIRST_LOGIN")]
        public bool FirstLogin { get; set; }

        public override string ToString()
        {
            return "";
        }
    }
}
