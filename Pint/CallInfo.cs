using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;

namespace Pint
{
    public class CallInfo
    {
        private CommandAst ast;

        public CallInfo(CommandAst ast)
        {
            this.ast = ast;
            this.NamedParameters = new List<string>();
            this.Splatted = false;
            ProcessAst();
        }

        public string Target
        {
            get
            {
                return ((StringConstantExpressionAst)ast.CommandElements[0]).Value;
            }
        }

        public bool Splatted { get; private set; }
        public IList<string> NamedParameters { get; private set; }

        private void ProcessAst()
        {
            foreach(CommandElementAst ce in ast.CommandElements.Skip(1))
            {
                CommandParameterAst cp = ce as CommandParameterAst;
                if (null != cp)
                {
                    NamedParameters.Add(cp.ParameterName);
                }
                else
                {
                    VariableExpressionAst ve = ce as VariableExpressionAst;
                    if (null != ve && ve.Splatted)
                    {
                        Splatted = true;
                    }
                }
            }            
        }              
    }
}
