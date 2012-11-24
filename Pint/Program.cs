using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class Program
    {
        public const int BadArguments = 1;
        public const int ErrorsEncountered = 2;
        public Analyzer Analyzer { get; private set; }

        public static void Main(string[] args)
        {
            Program p = new Program();
            System.Environment.ExitCode = p.Run(args);
        }

        public Program()
        {
            Analyzer = new Analyzer();
        }

        public int Run(string[] args)
        {
            try
            {
                Arguments arguments = new Arguments(args);
                CheckFiles(arguments.FilesToProcess);
                LogErrors();
            }
            catch(ArgumentException ex)
            {
                WriteError(ex.Message);
                WriteUsage();
                return BadArguments;
            }
            
            return 0;
        }

        public void LogErrors()
        {
            foreach (ParseError err in Analyzer.Errors)
            {
                WriteError(Analyzer.FormatError(err));
            }
        }

        private void CheckFiles(List<string> filesToProcess)
        {
            foreach (var file in filesToProcess)
            {
                CheckFile(file);
            }
        }

        public void CheckFile(string fileName)
        {
            Analyzer.LoadFile(fileName);
        }

        public void Check(string fileContents)
        {            
            Analyzer.Load(fileContents);
        }

        public virtual void WriteError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public virtual void WriteUsage()
        {
            
        }
    }
}
