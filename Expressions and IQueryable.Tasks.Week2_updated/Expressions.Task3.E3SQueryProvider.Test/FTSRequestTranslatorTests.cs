using System;
using System.Linq;
using System.Linq.Expressions;
using Expressions.Task3.E3SQueryProvider.Models.Entities;
using Xunit;

namespace Expressions.Task3.E3SQueryProvider.Test
{
    public class FtsRequestTranslatorTests
    {
        #region SubTask 1 : operands order

        [Fact]
        public void TestBinaryBackOrder()
        {
            var translator1 = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression1
                = employee => "EPRUIZHW006" == employee.Workstation;

            var translator2 = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression2
                = employee => employee.Workstation == "EPRUIZHW006";

            string translated1 = translator1.Translate(expression1);
            Assert.Equal("Workstation:(EPRUIZHW006)", translated1);

            string translated2 = translator2.Translate(expression2);
            Assert.Equal("Workstation:(EPRUIZHW006)", translated2);
        }

        #endregion

        #region SubTask 2: inclusion operations

        [Fact]
        public void TestBinaryEqualsQueryable()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<IQueryable<EmployeeEntity>, IQueryable<EmployeeEntity>>> expression
                = query => query.Where(e => e.Workstation == "EPRUIZHW006");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPRUIZHW006)", translated);
        }

        [Fact]
        public void TestBinaryEquals()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation == "EPRUIZHW006";

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPRUIZHW006)", translated);
        }

        [Fact]
        public void TestMethodEquals()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.Equals("EPRUIZHW006");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPRUIZHW006)", translated);
        }

        [Fact]
        public void TestStartsWith()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.StartsWith("EPRUIZHW006");
            
            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(EPRUIZHW006*)", translated);
        }

        [Fact]
        public void TestEndsWith()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.EndsWith("IZHW0060");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(*IZHW0060)", translated);
        }

        [Fact]
        public void TestContains()
        {
            var translator = new ExpressionToFtsRequestTranslator();
            Expression<Func<EmployeeEntity, bool>> expression
                = employee => employee.Workstation.Contains("IZHW006");

            string translated = translator.Translate(expression);
            Assert.Equal("Workstation:(*IZHW006*)", translated);
        }

        #endregion
    }
}
