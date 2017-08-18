using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Assignment
    {
        [TestMethod]
        public void Assignment_Null()
        {
            let a = null;
            Assert.IsNull(a);
        }

        [TestMethod]
        public void Assignment_Array_Bool()
        {
            let a = new bool[] { true, false };
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_Array_Bool()
        {
            let a = new bool[] { true, false };
            bool[] b = a;
            Assert.IsNotNull(b);
            Assert.AreEqual(2, b.Length);
            Assert.IsTrue(b[0]);
            Assert.IsFalse(b[1]);
        }

        [TestMethod]
        public void Assignment_Array_Int()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_Array_To_Int()
        {
            let a = new int[] { 0, 1, 2, 3 };
            int[] b = a;
            Assert.IsNotNull(a);
            Assert.AreEqual(4, b.Length);
            Assert.IsTrue(b[0] == 0);
            Assert.IsTrue(b[1] == 1);
            Assert.IsTrue(b[2] == 2);
            Assert.IsTrue(b[3] == 3);
        }

        [TestMethod]
        public void Assignment_Array_String()
        {
            let a = new string[] { "Hello", ",", "World", "!" };
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_Array_To_String()
        {
            let a = new string[] { "Hello", ",", "World", "!" };
            string[] b = a;
            Assert.IsNotNull(a);
            Assert.AreEqual(4, b.Length);
            Assert.IsTrue(b[0] == "Hello");
            Assert.IsTrue(b[1] == ",");
            Assert.IsTrue(b[2] == "World");
            Assert.IsTrue(b[3] == "!");
        }

        [TestMethod]
        public void Assignment_Boolean()
        {
            let a = true;
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_Boolean()
        {
            let a = true;
            bool b = a;
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void Assignment_Children()
        {
            let a = new object[,] { { "Value", true }, { "ChildProp", 5 } };
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_Jagged_Object_Array()
        {
            let a = new object[,] { { "Value", true }, { "ChildProp", 5 } };
            object[,] b = a;
            Assert.AreEqual(2, b.GetLength(0));
            Assert.AreEqual(2, b.GetLength(1));
            Assert.AreEqual("Value", b[0, 0]);
            Assert.IsTrue((bool)b[0, 1]);
            Assert.AreEqual("ChildProp", b[1, 0]);
            Assert.AreEqual(5, b[1, 1]);
        }

        [TestMethod]
        public void Assignment_Integer()
        {
            let a = 1;
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_Integer()
        {
            let a = 1;
            int b = a;
            Assert.AreEqual(1, b);
        }

        [TestMethod]
        public void Assignment_Real()
        {
            let a = 1.5;
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_Double()
        {
            let a = 1.5;
            double b = a;
            Assert.AreEqual(1.5, b);
        }

        [TestMethod]
        public void Assignment_String()
        {
            let a = "Hello";
            Assert.IsNotNull(a);
        }

        [TestMethod]
        public void Assignment_To_String()
        {
            let a = "Hello";
            string b = a;
            Assert.AreEqual("Hello", b);
        }
    }
}
