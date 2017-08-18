using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_ForEach
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = 0;
            Assert.IsNotNull(b);
            a.ForEach(i => b += i);
            Assert.IsTrue(b == 6);
        }

        [TestMethod]
        public void Bool()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = 0;
            Assert.IsNotNull(b);
            a.ForEach(i => b = ++i);
            Assert.IsFalse(b);
        }

        [TestMethod]
        public void Number()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = 0;
            Assert.IsNotNull(b);
            a.ForEach(i => b = ++i);
            Assert.IsTrue(b == 2);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = 0;
            Assert.IsNotNull(b);
            a.ForEach(i => b = ++i);
            Assert.IsTrue(b == "Hello ");
        }
    }
}
