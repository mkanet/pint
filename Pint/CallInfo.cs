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
        }

        public string Target
        {
            get
            {
                return ((StringConstantExpressionAst)ast.CommandElements[0]).Value;
            }
        }

        public IEnumerable<string> NamedParameters
        {
            get
            {
                foreach(CommandElementAst ce in ast.CommandElements.Skip(1))
                {
                    CommandParameterAst cp = ce as CommandParameterAst;
                    if (null == cp) continue;
                    yield return cp.ParameterName;
                }
                yield break;
            }
        }
    }
}
