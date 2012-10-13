using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;

namespace Pint
{
    public class FunctionDefinitionVisitor : AstVisitor
    {
        public FunctionTable FunctionTable { get; private set; }

        public FunctionDefinitionVisitor()
        {
            FunctionTable = new FunctionTable();
        }
        
        public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst functionDefinitionAst)
        {
            string name = functionDefinitionAst.Name;
            FunctionInfo info = new FunctionInfo(functionDefinitionAst);
            FunctionTable.Add(name, info);
            return base.VisitFunctionDefinition(functionDefinitionAst);
        }

    }
}
