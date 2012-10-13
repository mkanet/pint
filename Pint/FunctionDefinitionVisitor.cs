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
        public IDictionary<string, List<FunctionInfo>> FunctionTable { get; private set; }

        public FunctionDefinitionVisitor()
        {
            FunctionTable = new Dictionary<string, List<FunctionInfo>>();
        }
        

        public override AstVisitAction VisitFunctionDefinition(FunctionDefinitionAst functionDefinitionAst)
        {
            string name = functionDefinitionAst.Name;
            FunctionInfo info = new FunctionInfo(functionDefinitionAst);
            Add(name, info);
            return base.VisitFunctionDefinition(functionDefinitionAst);
        }

        public void Add(string name, FunctionInfo info)
        {
            List<FunctionInfo> current = null;
            if (FunctionTable.TryGetValue(name, out current))
            {
                current.Add(info);
            }
            else
            {
                current = new List<FunctionInfo>();
                current.Add(info);
                FunctionTable[name] = current;
            }
        }
    }
}
