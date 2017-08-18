using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Indexing
    {
        [TestMethod]
        public void Access_Array_Bool()
        {
            let a = new bool[] { true, false };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0]);
            Assert.IsFalse(a[1]);
        }

        [TestMethod]
        public void Assignment_Array_Bool()
        {
            let a = new bool[] { true, false };
            a[0] = false;
            a[1] = true;
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsFalse(a[0]);
            Assert.IsTrue(a[1]);
        }

        [TestMethod]
        public void Access_Array_Int()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
        }

        [TestMethod]
        public void Access_Array_Int_let()
        {
            let a = new int[] { 0, 1, 2, 3 };
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[1] == 1);
            Assert.IsNotNull(a);
            let b = 1;
            Assert.IsNotNull(b);
            Assert.IsTrue(b == 1);
            let c = a[b];
            Assert.IsTrue(c == 1);
        }

        [TestMethod]
        public void Assignment_Array_Int()
        {
            let a = new int[] { 0, 1, 2, 3 };
            a[0] = 3;
            a[1] = 2;
            a[2] = 1;
            a[3] = 0;
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 3);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 1);
            Assert.IsTrue(a[3] == 0);
        }

        [TestMethod]
        public void Access_Array_String()
        {
            let a = new string[] { "Hello", ",", "World", "!" };
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == "Hello");
            Assert.IsTrue(a[1] == ",");
            Assert.IsTrue(a[2] == "World");
            Assert.IsTrue(a[3] == "!");
        }

        [TestMethod]
        public void Assignment_Array_String()
        {
            let a = new string[] { "Hello", ",", "World", "!" };
            a[0] = "!";
            a[1] = "World";
            a[2] = ",";
            a[3] = "Hello";
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == "!");
            Assert.IsTrue(a[1] == "World");
            Assert.IsTrue(a[2] == ",");
            Assert.IsTrue(a[3] == "Hello");
        }

        [TestMethod]
        public void Assignment_Array_OutOfBounds()
        {
            let a = new int[] { 0, 1, 2, 3 };
            a[5] = 5;
            Assert.IsNotNull(a);
            Assert.AreEqual(6, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
            Assert.IsTrue(a[4] == null);
            Assert.IsTrue(a[5] == 5);
        }

        [TestMethod]
        public void Assignment_Array_BelowZero()
        {
            let a = new int[] { 0, 1, 2, 3 };
            a[-1] = 4;
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
            Assert.IsTrue(a[3] == 3);
        }

        [TestMethod]
        public void Access_Children()
        {
            let a = new object[,]
            {
                { "Value", true },
                { "ArrayProp", new int[] { 0, 1, 2, 3 } },
                { "BoolProp", true },
                { "NumberProp", 5 },
                { "StringProp", "Hello" }
            };
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.PropertyCount);
            CollectionAssert.AreEquivalent(a.Properties.ToList(), new string[] { "ArrayProp", "BoolProp", "NumberProp", "StringProp" });
            Assert.IsTrue(a.ContainsProperty("ArrayProp"));
            Assert.AreEqual(4, a["ArrayProp"].Length);
            Assert.IsTrue(a["ArrayProp"][0] == 0);
            Assert.IsTrue(a["ArrayProp"][1] == 1);
            Assert.IsTrue(a["ArrayProp"][2] == 2);
            Assert.IsTrue(a["ArrayProp"][3] == 3);
            Assert.IsTrue(a["BoolProp"]);
            Assert.IsTrue(a["NumberProp"] == 5);
            Assert.IsTrue(a["StringProp"] == "Hello");
        }

        [TestMethod]
        public void Assignment_Children()
        {
            let a = new object[,]
            {
                { "Value", true },
                { "ArrayProp", new int[] { 0, 1, 2, 3 } },
                { "BoolProp", true },
                { "NumberProp", 5 },
                { "StringProp", "Hello" }
            };
            a["ArrayProp"][0] = true;
            a["ArrayProp"][1] = "Hello";
            a["ArrayProp"][2] = 1;
            a["ArrayProp"][3] = new int[] { 0, 1, 2, 3 };
            a["BoolProp"] = false;
            a["NumberProp"] = 1.5;
            a["StringProp"] = "Goodbye";
            Assert.IsNotNull(a);
            Assert.AreEqual(4, a.PropertyCount);
            CollectionAssert.AreEquivalent(a.Properties.ToList(), new string[] { "ArrayProp", "BoolProp", "NumberProp", "StringProp" });
            Assert.IsTrue(a.ContainsProperty("ArrayProp"));
            Assert.AreEqual(4, a["ArrayProp"].Length);
            Assert.IsTrue(a["ArrayProp"][0]);
            Assert.IsTrue(a["ArrayProp"][1] == "Hello");
            Assert.IsTrue(a["ArrayProp"][2] == 1);
            Assert.IsTrue(a["ArrayProp"][3][0] == 0);
            Assert.IsTrue(a["ArrayProp"][3][1] == 1);
            Assert.IsTrue(a["ArrayProp"][3][2] == 2);
            Assert.IsTrue(a["ArrayProp"][3][3] == 3);
            Assert.IsFalse(a["BoolProp"]);
            Assert.IsTrue(a["NumberProp"] == 1.5);
            Assert.IsTrue(a["StringProp"] == "Goodbye");
        }

        [TestMethod]
        public void Assignment_Children_New()
        {
            let a = new object[,]
            {
                { "Value", true },
                { "ArrayProp", new int[] { 0, 1, 2, 3 } },
                { "BoolProp", true },
                { "NumberProp", 5 },
                { "StringProp", "Hello" }
            };
            a["NewProp"] = 1.5;
            Assert.IsNotNull(a);
            Assert.AreEqual(5, a.PropertyCount);
            CollectionAssert.AreEquivalent(a.Properties.ToList(), new string[] { "ArrayProp", "BoolProp", "NumberProp", "StringProp", "NewProp" });
            Assert.IsTrue(a.ContainsProperty("ArrayProp"));
            Assert.AreEqual(4, a["ArrayProp"].Length);
            Assert.IsTrue(a["ArrayProp"][0] == 0);
            Assert.IsTrue(a["ArrayProp"][1] == 1);
            Assert.IsTrue(a["ArrayProp"][2] == 2);
            Assert.IsTrue(a["ArrayProp"][3] == 3);
            Assert.IsTrue(a["BoolProp"]);
            Assert.IsTrue(a["NumberProp"] == 5);
            Assert.IsTrue(a["StringProp"] == "Hello");
            Assert.IsTrue(a["NewProp"] == 1.5);
        }
    }
}
