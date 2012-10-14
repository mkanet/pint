using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class FunctionCallVisitor : AstVisitor
    {
        public IList<CallInfo> Calls { get; private set; }

        public FunctionCallVisitor()
        {
            Calls = new List<CallInfo>();
        }

        public override AstVisitAction VisitCommand(CommandAst commandAst)
        {
                Calls.Add(new CallInfo(commandAst));
 	            return base.VisitCommand(commandAst);
        }
        
    }
}
