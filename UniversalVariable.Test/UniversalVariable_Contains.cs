using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Contains
    {
        [TestMethod]
        public void Array_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            Assert.IsTrue(a.Contains(0));
        }

        [TestMethod]
        public void Array_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            Assert.IsFalse(a.Contains(4));
        }

        [TestMethod]
        public void Bool_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.Contains("e"));
        }

        [TestMethod]
        public void Bool_F()
        {
            let a = true;
            Assert.IsNotNull(a);
            Assert.IsFalse(a.Contains(0));
        }

        [TestMethod]
        public void Number_T()
        {
            let a = 10;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.Contains(1));
        }

        [TestMethod]
        public void Number_F()
        {
            let a = 1;
            Assert.IsNotNull(a);
            Assert.IsFalse(a.Contains(0));
        }

        [TestMethod]
        public void String_T()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            Assert.IsTrue(a.Contains("e"));
        }

        [TestMethod]
        public void String_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            Assert.IsFalse(a.Contains("!"));
        }
    }
}
