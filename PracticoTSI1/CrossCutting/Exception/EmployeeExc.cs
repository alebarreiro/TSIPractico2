using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exception
{
    public class EmployeeExc : System.Exception
    {
        public EmployeeExc()
        {

        }
        public EmployeeExc(string message):base(message)
        {

        }
        public EmployeeExc(string message, System.Exception inner)
            : base(message, inner)
        {

        }

    }
}
