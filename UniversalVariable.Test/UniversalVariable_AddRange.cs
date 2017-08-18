using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_AddRange
    {
        [TestMethod]
        public void Array_AddRange()
        {
            let a = new int[] { 0, 1, 2, 3 };
            a.AddRange(new int[] { 4, 5 });
            Assert.IsNotNull(a);
            Assert.AreEqual(6, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
            Assert.IsTrue(a[4] == 4);
            Assert.IsTrue(a[5] == 5);
        }

        [TestMethod]
        public void NonArray_AddRange()
        {
            let a = 0;
            a.AddRange(new int[] { 1, 2 });
            Assert.IsNotNull(a);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
        }
    }
}
