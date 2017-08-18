using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Split
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2 };
            Assert.IsNotNull(a);

            a[1] = null;
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == null);
            Assert.IsTrue(a[2] == 2);

            a = a.Split(null, System.StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
        }

        [TestMethod]
        public void Array_Count()
        {
            let a = new int[] { 0, 1, 2 };
            Assert.IsNotNull(a);

            a[1] = null;
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == null);
            Assert.IsTrue(a[2] == 2);

            a = a.Split(null, 1, System.StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(1, a.Length);
            Assert.IsTrue(a[0] == 0);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello, World!";
            Assert.IsNotNull(a);
            a = a.Split(new string[] { ",", " " }, System.StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == "Hello");
            Assert.IsTrue(a[1] == "World!");
        }

        [TestMethod]
        public void String_Count()
        {
            let a = "Hello, Today's World!";
            Assert.IsNotNull(a);
            a = a.Split(new string[] { ",", " " }, 2, System.StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == "Hello");
            Assert.IsTrue(a[1] == "Today's World!");
        }
    }
}
