using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class FunctionInfo
    {
        private FunctionDefinitionAst ast;

        private List<string> mandatoryParameters;
        public List<string> MandatoryParameters {
            get
            {
                if (mandatoryParameters == null)
                {
                    mandatoryParameters = ast.MandatoryParameters().ToList();
                }
                return mandatoryParameters;
            }            
        }

        public FunctionInfo(FunctionDefinitionAst ast)
        {
            this.ast = ast;
        }

        public bool IsWorkflow {
            get
            {
                return ast.IsWorkflow;
            }
        }
    }
}
