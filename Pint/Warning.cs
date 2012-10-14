using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pint
{
    public class Warning
    {
        public string Message { get; private set; }

        public Warning(string message)
        {
            Message = message;
        }
    }
}
