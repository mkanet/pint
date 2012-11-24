using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;

namespace Pint
{
    public class Analyzer
    {
        public IEnumerable<object> Warnings {get; private set; }

        private List<ParseError> errors;
        public IEnumerable<ParseError> Errors {
            get { return errors; }
        }

        public Analyzer()
        {
            Warnings = new List<object>();
            errors = new List<ParseError>();
        }

        public void Load(string p)
        {
            Token[] tokens;
            ParseError[] localErrors;
            var ast =  Parser.ParseInput(p, out tokens, out localErrors);

            errors.AddRange(localErrors);            
        }

        public void LoadFile(string fileName)
        {
            Token[] tokens;
            ParseError[] localErrors;
            var ast = Parser.ParseFile(fileName, out tokens, out localErrors);

            errors.AddRange(localErrors);  
        }

        public string FormatError(ParseError err)
        {
            return String.Format(
                "{0}({1},{2},{3},{4}): error {5}: {6}",
                err.Extent.File,
                err.Extent.StartLineNumber,
                err.Extent.StartColumnNumber,
                err.Extent.EndLineNumber,
                err.Extent.EndColumnNumber,
                err.ErrorId,
                err.Message);
        }
    }


}
