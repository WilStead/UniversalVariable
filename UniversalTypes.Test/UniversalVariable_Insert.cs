using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_Insert
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Insert(1, 4);
            Assert.AreEqual(5, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 4);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == 2);
            Assert.IsTrue(a[4] == 3);
        }

        [TestMethod]
        public void Array_OutOfBounds()
        {
            let a = new int[] { 0, 1 };
            Assert.IsNotNull(a);
            a.Insert(3, 4);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == null);
            Assert.IsTrue(a[3] == 4);
        }

        [TestMethod]
        public void Array_Range()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.InsertRange(1, new int[] { 4, 5 });
            Assert.AreEqual(6, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 4);
            Assert.IsTrue(a[2] == 5);
            Assert.IsTrue(a[3] == 1);
            Assert.IsTrue(a[4] == 2);
            Assert.IsTrue(a[5] == 3);
        }

        [TestMethod]
        public void NonArray()
        {
            let a = true;
            Assert.IsNotNull(a);
            a.Insert(1, 4);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == true);
            Assert.IsTrue(a[1] == 4);
        }

        [TestMethod]
        public void NonArray_OutOfBounds()
        {
            let a = true;
            Assert.IsNotNull(a);
            a.Insert(2, 4);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == true);
            Assert.IsTrue(a[1] == null);
            Assert.IsTrue(a[2] == 4);
        }
    }
}
