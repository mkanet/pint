using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pint;

namespace PintTests
{
    public class MockProgram : Program
    {
        public List<string> LoggedErrors = new List<string>();

        public override void WriteError(string message)
        {
            LoggedErrors.Add(message);
        }
    }
}
