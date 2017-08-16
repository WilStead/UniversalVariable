using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_Sort
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 3, 1, 2 };
            Assert.IsNotNull(a);
            a.Sort();
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
        }

        [TestMethod]
        public void Array_Range()
        {
            let a = new int[] { 0, 3, 1, 2 };
            Assert.IsNotNull(a);
            a.Sort(1, 2);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 3);
            Assert.IsTrue(a[3] == 2);
        }

        [TestMethod]
        public void String()
        {
            let a = "elolH";
            Assert.IsNotNull(a);
            a.Sort();
            Assert.IsTrue(a == "Hello");
        }

        [TestMethod]
        public void String_Range()
        {
            let a = "eolHl";
            Assert.IsNotNull(a);
            a.Sort(1, 3);
            Assert.IsTrue(a == "eHlol");
        }
    }
}
