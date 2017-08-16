using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_ToLowerUpper
    {
        [TestMethod]
        public void Array_Lower()
        {
            let a = new string[] { "Hello", "World" };
            Assert.IsNotNull(a);
            a = a.ToLower();
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == "hello");
            Assert.IsTrue(a[1] == "world");
        }

        [TestMethod]
        public void Array_Upper()
        {
            let a = new string[] { "Hello", "World" };
            Assert.IsNotNull(a);
            a = a.ToUpper();
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == "HELLO");
            Assert.IsTrue(a[1] == "WORLD");
        }

        [TestMethod]
        public void String_Lower()
        {
            let a = "Hello, World!";
            Assert.IsNotNull(a);
            a = a.ToLower();
            Assert.IsTrue(a == "hello, world!");
        }

        [TestMethod]
        public void String_Upper()
        {
            let a = "Hello, World!";
            Assert.IsNotNull(a);
            a = a.ToUpper();
            Assert.IsTrue(a == "HELLO, WORLD!");
        }
    }
}
