using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Add
    {
        [TestMethod]
        public void Array_Add()
        {
            let a = new int[] { 0, 1, 2, 3 };
            a.Add(4);
            Assert.IsNotNull(a);
            Assert.AreEqual(5, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
            Assert.IsTrue(a[4] == 4);
        }

        [TestMethod]
        public void NonArray_Add()
        {
            let a = 0;
            a.Add(1);
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }
    }
}
