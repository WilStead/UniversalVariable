using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_IndexOf
    {
        [TestMethod]
        public void Array_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.IndexOf(1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Array_Index_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.IndexOf(1, 2);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Array_Index_Count_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.IndexOf(3, 1, 2);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void NonArray_Index_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.IndexOf(true, 1);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Bool_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.IndexOf(true);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void Number_T()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.IndexOf(1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void String_T()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.IndexOf("e");
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void String_Index_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.IndexOf("e", 2);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void String_Index_Count_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.IndexOf("o", 1, 2);
            Assert.IsTrue(b == -1);
        }
    }
}
