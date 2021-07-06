using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        private static Mapper<Foo, Bar> _mapper;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            var mapGenerator = new MappingGenerator();
            _mapper = mapGenerator.Generate<Foo, Bar>();
        }

        [TestMethod]
        public void TestMethodAllProperties()
        {
            var foo = new Foo(11, 22, "aaa copy", 333, "ccc");
            var res = _mapper.Map(foo);
            Assert.IsTrue(foo.EqualsBar(res));
        }

        [TestMethod]
        public void TestMethodTwoProperties()
        {
            var foo = new Foo()
            {
                PropertyCopy1 = 11,
                PropertyCopy2 = "aaaaaa"
            };
            var res = _mapper.Map(foo);
            Assert.IsTrue(foo.EqualsBar(res));
        }
    }
}
