using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_GetRange
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.GetRange(1, 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(2, b.Length);
            Assert.IsTrue(b[0] == 1);
            Assert.IsTrue(b[1] == 2);
        }
        [TestMethod]
        public void Array_Substring()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.Substring(1, 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(2, b.Length);
            Assert.IsTrue(b[0] == 1);
            Assert.IsTrue(b[1] == 2);
        }

        [TestMethod]
        public void Bool()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.GetRange(1, 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.Length);
            Assert.IsTrue(b[0]);
        }

        [TestMethod]
        public void Number()
        {
            let a = 3;
            Assert.IsNotNull(a);
            let b = a.GetRange(1, 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.Length);
            Assert.IsTrue(b[0] == 3);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.GetRange(1, 2);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == "el");
        }

        [TestMethod]
        public void String_Substring()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.Substring(1, 2);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == "el");
        }
    }
}
