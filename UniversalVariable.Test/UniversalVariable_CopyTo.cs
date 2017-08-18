using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_CopyTo
    {
        [TestMethod]
        public void Array_CopyTo()
        {
            let a = new int[] { 0, 1, 2, 3 };
            let b = -1;
            a.CopyTo(b, 1);
            Assert.IsNotNull(b);
            Assert.AreEqual(5, b.Length);
            Assert.IsTrue(b[0] == -1);
            Assert.IsTrue(b[1] == 0);
            Assert.IsTrue(b[2] == 1);
            Assert.IsTrue(b[3] == 2);
            Assert.IsTrue(b[4] == 3);
        }

        [TestMethod]
        public void NonArray_CopyTo_Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            let b = 4;
            Assert.IsNotNull(b);
            b.CopyTo(a);
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 4);
            Assert.IsTrue(a[1] == 4);
            Assert.IsTrue(a[2] == 4);
            Assert.IsTrue(a[3] == 4);
        }

        [TestMethod]
        public void NonArray_CopyTo_NonArray()
        {
            let a = 0;
            let b = 4;
            Assert.IsNotNull(a);
            a.CopyTo(b);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void NonArray_CopyTo_NonArray_With_Index()
        {
            let a = 0;
            let b = 4;
            Assert.IsNotNull(a);
            a.CopyTo(b, 1);
            Assert.IsNotNull(b);
            Assert.AreEqual(2, b.Length);
            Assert.IsTrue(b[0] == 4);
            Assert.IsTrue(b[1] == 0);
        }
    }
}
