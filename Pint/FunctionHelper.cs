using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;

namespace Pint
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

        public static IEnumerable<string> MandatoryParameters(this FunctionDefinitionAst def)
        {
            if (def.Body.ParamBlock == null) yield break;

            foreach(var param in def.Body.ParamBlock.Parameters.Where(IsMandatory))
            {
                yield return param.Name.VariablePath.UserPath;
            }
        }
    }
}
