using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_TrueForAll
    {
        [TestMethod]
        public void Array_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            Assert.IsTrue(a.TrueForAll(i => i < 4));
        }

        [TestMethod]
        public void Array_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            Assert.IsFalse(a.TrueForAll(i => i < 2));
        }

        [TestMethod]
        public void Bool()
        {
            let a = true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.TrueForAll(i => i));
        }

        [TestMethod]
        public void Number()
        {
            let a = 1;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.TrueForAll(i => i < 2));
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            Assert.IsTrue(a.TrueForAll(i => i.Length == 5));
        }
    }
}
