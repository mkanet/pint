using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;
using System.Management.Automation;

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
            ParseError[] localErrors=null;

            try
            {
                var ast = Parser.ParseFile(fileName, out tokens, out localErrors);
                errors.AddRange(localErrors); 
            }
            catch (ParseException ex)
            {
                var pos = new ScriptPosition(fileName,0,0,"");
                var err = new ParseError(new ScriptExtent(pos, pos), "FatalParserError", GetNestedMessage(ex));
                errors.Add(err);
            }             
        }

        private string GetNestedMessage(Exception ex)
        {
            StringBuilder message = new StringBuilder();
            message.Append(ex.Message);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                message.AppendFormat("--> {0}", ex.Message);
            }
            return message.ToString();
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
