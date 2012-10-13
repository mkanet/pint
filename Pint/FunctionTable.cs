using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class FunctionTable : Dictionary<string, List<FunctionInfo>> 
    {
        public void Add(string key, FunctionInfo value)
        {
            List<FunctionInfo> current = null;
            if (this.TryGetValue(key, out current))
            {
                current.Add(value);
            }
            else
            {
                current = new List<FunctionInfo>();
                current.Add(value);
                this[key] = current;
            }
        }
    }
}
