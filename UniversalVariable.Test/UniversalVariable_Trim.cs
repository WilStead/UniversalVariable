using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Trim
    {
        [TestMethod]
        public void Array()
        {
            let a = new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);

            a.Insert(0, null);
            a[3] = null;
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == null);
            Assert.IsTrue(a[1] == 0);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == null);

            a = a.Trim();
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }

        [TestMethod]
        public void Array_Start()
        {
            let a = new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);

            a.Insert(0, null);
            a[3] = null;
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == null);
            Assert.IsTrue(a[1] == 0);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == null);

            a = a.TrimStart();
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == null);
        }

        [TestMethod]
        public void Array_End()
        {
            let a = new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);

            a.Insert(0, null);
            a[3] = null;
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == null);
            Assert.IsTrue(a[1] == 0);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == null);

            a = a.TrimEnd();
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == null);
            Assert.IsTrue(a[1] == 0);
            Assert.IsTrue(a[2] == 1);
        }

        [TestMethod]
        public void Array_Match()
        {
            let a = new int[] { 0, 1, 2, 0 };
            Assert.IsNotNull(a);

            a = a.Trim(0);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 1);
            Assert.IsTrue(a[1] == 2);
        }

        [TestMethod]
        public void Array_Match_Start()
        {
            let a = new int[] { 0, 1, 2, 0 };
            Assert.IsNotNull(a);

            a = a.TrimStart(0);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 1);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 0);
        }

        [TestMethod]
        public void Array_Match_End()
        {
            let a = new int[] { 0, 1, 2, 0 };
            Assert.IsNotNull(a);

            a = a.TrimEnd(0);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
        }

        [TestMethod]
        public void String()
        {
            let a = " Hello ";
            Assert.IsNotNull(a);
            a = a.Trim();
            Assert.IsTrue(a == "Hello");
        }

        [TestMethod]
        public void String_Start()
        {
            let a = " Hello ";
            Assert.IsNotNull(a);
            a = a.TrimStart();
            Assert.IsTrue(a == "Hello ");
        }

        [TestMethod]
        public void String_End()
        {
            let a = " Hello ";
            Assert.IsNotNull(a);
            a = a.TrimEnd();
            Assert.IsTrue(a == " Hello");
        }

        [TestMethod]
        public void String_Match()
        {
            let a = "xHellox";
            Assert.IsNotNull(a);
            a = a.Trim("x");
            Assert.IsTrue(a == "Hello");
        }

        [TestMethod]
        public void String_Match_Start()
        {
            let a = "xHellox";
            Assert.IsNotNull(a);
            a = a.TrimStart("x");
            Assert.IsTrue(a == "Hellox");
        }

        [TestMethod]
        public void String_Match_End()
        {
            let a = "xHellox";
            Assert.IsNotNull(a);
            a = a.TrimEnd("x");
            Assert.IsTrue(a == "xHello");
        }
    }
}
