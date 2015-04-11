using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    [Table("Log")]
    public class Log
    {
        [Key, Column("DATE")]
        public DateTime Date { get; set; }
        [Column("LEVEL")]
        public string LogLevel { get; set; }
        [Column("Message")]
        public string Message { get; set; }
        [Column("EXCEPTION")]
        public string Exception { get; set; }
    }
}
