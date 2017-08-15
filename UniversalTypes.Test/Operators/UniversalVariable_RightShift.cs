using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_RightShift
    {
        [TestMethod]
        public void Untyped()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
        }

        [TestMethod]
        public void Array()
        {
            let a = new int[] { 4, 2 };
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == null);
            Assert.IsTrue(a[1] == 4);
        }

        [TestMethod]
        public void Bool()
        {
            let a = true;
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Number_Number()
        {
            let a = 2;
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void String_Boolean()
        {
            let a = "true";
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void String_Numeric()
        {
            let a = "2";
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void String()
        {
            let a = "Hello";
            a >>= 1;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == " Hell");
        }
    }
}
