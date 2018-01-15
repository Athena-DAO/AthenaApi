using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataCenterManager.Test
{
    [TestClass]
    public class SimpleUnitTest
    {
        public readonly SimpleClass _simpleClass;

        public SimpleUnitTest()
        {
            _simpleClass = new SimpleClass();
        }

        [TestMethod]
        public void FirstTestMethod()
        {
            var result = _simpleClass.SimpleMethod();

            Assert.AreEqual(result, "Hello, World");
        }
    }
}
