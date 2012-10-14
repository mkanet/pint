using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class MandatoryParametersPassedAnalyzer
    {
        
        public AnalysisResults Analyze(Ast ast)
        {
            AnalysisResults results = new AnalysisResults();
            FunctionDefinitionVisitor definitionVisitor = new FunctionDefinitionVisitor();
            FunctionCallVisitor callVisitor = new FunctionCallVisitor();


            ast.Visit(definitionVisitor);
            ast.Visit(callVisitor);

            foreach(CallInfo call in callVisitor.Calls)
            {
                ValidateMandatoryParameters(results, call, definitionVisitor.FunctionTable);                     
            }
            return results;
        }

        private void ValidateMandatoryParameters(AnalysisResults results, CallInfo call, FunctionTable functionTable)
        {
            if (call.Splatted)
            {
                // can't analyze, skip it
                return;
            }
            string function = call.Target;
            List<FunctionInfo> candidates;
            if (!functionTable.TryGetValue(function, out candidates))
            {
                // unknown function, can't validate
                return;
            }
            if (candidates.Count > 1)
            {
                // multiple definitions with same name, can't disambiguate
                return;
            }
            var expectedParameters = candidates[0].MandatoryParameters;
            var passedParameters = call.NamedParameters.ToList();

            foreach (var p in expectedParameters)
            {
                if(! passedParameters.Contains(p,StringComparer.OrdinalIgnoreCase))
                {
                    // we don't appear to be passing p, warn
                    results.Warnings.Add(new Warning(p));
                }
            }
        }
    }
}
