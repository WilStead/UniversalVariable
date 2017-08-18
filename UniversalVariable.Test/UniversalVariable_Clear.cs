using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Clear
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Clear();
            Assert.AreEqual(0, a.Length);
        }

        [TestMethod]
        public void Bool()
        {
            let a = true;
            Assert.IsNotNull(a);
            a.Clear();
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Number()
        {
            let a = 1;
            Assert.IsNotNull(a);
            a.Clear();
            Assert.IsTrue(a == double.NaN);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.Clear();
            Assert.IsTrue(a == string.Empty);
        }
    }
}
