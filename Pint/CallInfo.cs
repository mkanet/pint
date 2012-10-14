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
    }
}
