using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Reverse
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Reverse();
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 3);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == 0);
        }

        [TestMethod]
        public void Array_Range()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Reverse(1, 2);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == 3);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.Reverse();
            Assert.IsTrue(a == "olleH");
        }

        [TestMethod]
        public void String_Range()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.Reverse(1, 2);
            Assert.IsTrue(a == "Hlelo");
        }
    }
}
