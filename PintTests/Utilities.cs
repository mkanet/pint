using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PintTests
{
    public static class Utilities
    {
        public static ScriptBlockAst GetAst(string block)
        {
            Token[] tokens;
            ParseError[] errors;
            ScriptBlockAst ast = Parser.ParseInput(block, out tokens, out errors);
            Assert.Empty(errors);
            return ast;
        }

        public static FunctionDefinitionAst GetSingleFunctionAst(string definition)
        {
            ScriptBlockAst ast = Utilities.GetAst(definition);
            return  (FunctionDefinitionAst)(ast.EndBlock.Statements[0]);
        }
    }
}
