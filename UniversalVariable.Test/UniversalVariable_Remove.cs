using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Remove
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Remove(1);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 3);
        }

        [TestMethod]
        public void Array_At()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.RemoveAt(1);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 3);
        }

        [TestMethod]
        public void Array_Range()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.Remove(1, 2);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 3);
        }

        [TestMethod]
        public void Array_All_Index()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.RemoveAll(2);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }

        [TestMethod]
        public void Array_All()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            a.RemoveAll(i => i < 3);
            Assert.AreEqual(1, a.Length);
            Assert.IsTrue(a[0] == 3);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.Remove("l");
            Assert.IsTrue(a == "Heo");
        }

        [TestMethod]
        public void String_At()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.RemoveAt(1);
            Assert.IsTrue(a == "Hllo");
        }

        [TestMethod]
        public void String_Range()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.Remove(1, 2);
            Assert.IsTrue(a == "Hlo");
        }

        [TestMethod]
        public void String_All_Index()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            a.RemoveAll(2);
            Assert.IsTrue(a == "He");
        }
    }
}
