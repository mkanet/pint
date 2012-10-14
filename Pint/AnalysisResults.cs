using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pint
{
    public class AnalysisResults
    {
        public IList<Warning> Warnings { get; private set; }

        public AnalysisResults()
        {
            Warnings = new List<Warning>();
        }
    }
}
