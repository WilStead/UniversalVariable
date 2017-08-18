using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_FindIndex
    {
        [TestMethod]
        public void Array_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Array_Last_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindLastIndex(i => i < 3);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 2);
        }

        [TestMethod]
        public void Array_Index_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(1, i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Array_Last_Index_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindLastIndex(2, i => i < 3);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 2);
        }

        [TestMethod]
        public void Array_Index_Count_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(1, 2, i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
        }

        [TestMethod]
        public void Array_Last_Index_Count_T()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindLastIndex(2, 2, i => i < 3);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 2);
        }

        [TestMethod]
        public void Array_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == "Hello");
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Array_Index_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(2, i => i == 1);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Array_Index_Count_F()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            let b = a.FindIndex(1, 2, i => i == 3);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void NonArray_Index_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.FindIndex(1, i => i);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Bool_T()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void Bool_F()
        {
            let a = true;
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == "Hello");
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void Number_T()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == 1);
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void Number_F()
        {
            let a = 1;
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == 2);
            Assert.IsTrue(b == -1);
        }

        [TestMethod]
        public void String_T()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == "Hello");
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 0);
        }

        [TestMethod]
        public void String_F()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
            let b = a.FindIndex(i => i == 2);
            Assert.IsTrue(b == -1);
        }
    }
}
