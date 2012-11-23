using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class Program
    {
        public const int BadArguments = 1;
        public const int ErrorsEncountered = 2;

        public static void Main(string[] args)
        {
            Program p = new Program();
            System.Environment.ExitCode = p.Run(args);
        }

        public int Run(string[] args)
        {
            try
            {
                Arguments arguments = new Arguments(args);
                CheckFiles(arguments.FilesToProcess);
            }
            catch(ArgumentException ex)
            {
                WriteError(ex.Message);
                WriteUsage();
                return BadArguments;
            }
            
            return 0;
        }

        private void CheckFiles(List<string> filesToProcess)
        {
            throw new NotImplementedException();
        }

        public void Check(string fileContents)
        {
            Analyzer analyzer = new Analyzer();
            analyzer.Load(fileContents);
        }

        public virtual void WriteError(string p)
        {
            
        }

        public virtual void WriteUsage()
        {
            
        }
    }
}
