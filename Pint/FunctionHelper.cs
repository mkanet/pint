using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;

namespace PintTests
{
    public static class FunctionHelper
    {
        public static bool IsMandatory(this ParameterAst ast)
        {
            return ast.Attributes.Any(IsMandatoryAttribute);
        }

        public static bool IsMandatoryAttribute(AttributeBaseAst bast)
        {
            AttributeAst ast = bast as AttributeAst;
            if (null == ast) return false;
            if (ast.TypeName.FullName != "Parameter") return false;
            return ast.NamedArguments.Any(arg => 
                arg.ArgumentName == "Mandatory" && 
                ((VariableExpressionAst)(arg.Argument)).VariablePath.UserPath == "true");
        }
    }
}
