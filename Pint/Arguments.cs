using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class Arguments
    {
        public Arguments(string[] args)
        { 
            FilesToProcess = new List<string>();

            for (int i = 0; i < args.Length; ++i)
            {
                string arg = args[i];
                if (String.IsNullOrEmpty(arg))
                {
                    // shouldn't really happen, but skip it 
                    continue;
                }

                if (arg[0] == '/' || arg[0] == '-')
                {
                    // a switch
                }
                else
                {
                    // a file to process
                    FilesToProcess.Add(arg);
                }
            }
            if (FilesToProcess.Count == 0)
            {
                throw new ArgumentException("No files specified");
            }
        }

        public List<string> FilesToProcess { get; private set; }
    }
}
