using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_Find
    {
        [TestMethod]
        public void Array_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.Find(i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Array_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.Find(i => i == "Hello");
            Assert.IsTrue(b == null);
        }

        [TestMethod]
        public void Bool_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.Find(i => i);
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void Bool_F()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.Find(i => i == "Hello");
            Assert.IsTrue(b == null);
        }

        [TestMethod]
        public void Number_T()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.Find(i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Number_F()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.Find(i => i == 2);
            Assert.IsTrue(b == null);
        }

        [TestMethod]
        public void String_T()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.Find(i => i == "Hello");
            Assert.IsNotNull(b);
            Assert.IsTrue(b == "Hello");
        }

        [TestMethod]
        public void String_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.Find(i => i == 2);
            Assert.IsTrue(b == null);
        }
    }
}
