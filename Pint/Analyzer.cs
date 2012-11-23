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
        public ParseError[] Errors { get; private set; }
        public Analyzer()
        {
            Warnings = new List<object>();
        }

        public void Load(string p)
        {
            Token[] tokens;
            ParseError[] errors;
            var ast =  Parser.ParseInput(p, out tokens, out errors);

            Errors = errors;            
        }
    }


}
