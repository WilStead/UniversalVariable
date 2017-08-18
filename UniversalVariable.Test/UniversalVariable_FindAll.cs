using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_FindAll
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i < 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(2, b.Length);
            Assert.IsTrue(b[0] == 0);
            Assert.IsTrue(b[1] == 1);
        }

        [TestMethod]
        public void Bool_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i);
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.Length);
            Assert.IsTrue(b[0]);
        }

        [TestMethod]
        public void Bool_F()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i == "Hello");
            Assert.IsNotNull(b);
            Assert.AreEqual(0, b.Length);
        }

        [TestMethod]
        public void Number_T()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i == 1);
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.Length);
            Assert.IsTrue(b[0] == 1);
        }

        [TestMethod]
        public void Number_F()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i == 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(0, b.Length);
        }

        [TestMethod]
        public void String_T()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i == "Hello");
            Assert.IsNotNull(b);
            Assert.AreEqual(1, b.Length);
            Assert.IsTrue(b[0] == "Hello");
        }

        [TestMethod]
        public void String_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.FindAll(i => i == 2);
            Assert.IsNotNull(b);
            Assert.AreEqual(0, b.Length);
        }
    }
}
