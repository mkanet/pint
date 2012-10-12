using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation.Language;

namespace Pint
{
    public class AnalyzerException : Exception
    {
    }

    public class AnalyzerParseException : AnalyzerException
    {
        public ParseError[] Errors { get; private set; }

        public AnalyzerParseException(ParseError[] errors)
        {
            Errors = errors;
        }
    }
}
