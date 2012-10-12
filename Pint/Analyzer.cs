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
        public void Load(string p)
        {
            Token[] tokens;
            ParseError[] errors;
            Parser.ParseInput(p, out tokens, out errors);

            if (errors.Length != 0)
            {
                throw new AnalyzerParseException(errors);
            }
        }
    }
}
