using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;

namespace PintTests
{
    public class FunctionCallVisitorTests
    {

        [Fact]
        public void FunctionCall_Found()
        {
            Ast ast = Utilities.GetAst(@"foo -A 1 -B");
            FunctionCallVisitor visitor = new FunctionCallVisitor();

            ast.Visit(visitor);
            Assert.Equal(1, visitor.Calls.Count);            
        }

        [Fact]
        public void FunctionCall_NameFound()
        {
            Ast ast = Utilities.GetAst(@"foo -A 1 -B");
            FunctionCallVisitor visitor = new FunctionCallVisitor();

            ast.Visit(visitor);
            Assert.Equal(1, visitor.Calls.Count);
            Assert.Equal("foo", visitor.Calls[0].Target);
        }

        [Fact]
        public void FunctionCall_BoundParameters()
        {
            Ast ast = Utilities.GetAst(@"foo -A 1 -B");
            FunctionCallVisitor visitor = new FunctionCallVisitor();

            ast.Visit(visitor);
            Assert.Equal(1, visitor.Calls.Count);
            CallInfo info = visitor.Calls[0];
            Assert.Equal(new[] { "A", "B" }, info.NamedParameters);
        }
    }
}
