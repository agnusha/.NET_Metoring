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
            var mappingGenerator = new MappingGenerator();
            _mapper = mappingGenerator.Generate<Foo, Bar>();
        }

        [TestMethod]
        public void TestMethodAllProperties()
        {
            var foo = new Foo(11, 22, "aaa","ccc");
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

        [TestMethod]
        public void TestMethodOneProperties()
        {
            var foo = new Foo()
            {
                PropertyCopy1 = 3333
            };
            var res = _mapper.Map(foo);
            Assert.IsTrue(foo.EqualsBar(res));
        }

        [TestMethod]
        public void TestMethodOneFromTwoPropertiesCopy()
        {
            var foo = new Foo()
            {
                PropertyCopy1 = 3333,
                PropertyNotCopyFoo1 = 4444
            };
            var res = _mapper.Map(foo);
            Assert.IsTrue(foo.EqualsBar(res));
            
        }
    }
}
