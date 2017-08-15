﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_Minus
    {
        [TestMethod]
        public void Unary()
        {
            let a = 5;
            a = -a;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -5);
        }

        [TestMethod]
        public void Untyped_Null()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a -= null;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
        }

        [TestMethod]
        public void Untyped_Array()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a -= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 1);
            Assert.IsTrue(a[1] == 0);
        }

        [TestMethod]
        public void Untyped_Bool()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Untyped_Number()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a -= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
            Assert.IsTrue(a == -2);
        }

        [TestMethod]
        public void Untyped_String()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a -= "Hello";
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
            Assert.IsTrue(a == "olleH");
        }

        [TestMethod]
        public void Array_Array()
        {
            let a = new int[] { 0, 1 };
            a -= new int[] { 1, 2 };
            Assert.IsNotNull(a);
            Assert.AreEqual(1, a.Length);
            Assert.IsTrue(a[0] == 0);
        }

        [TestMethod]
        public void Array_Included()
        {
            let a = new int[] { 0, 1 };
            a -= 0;
            Assert.IsNotNull(a);
            Assert.AreEqual(1, a.Length);
            Assert.IsTrue(a[0] == 1);
        }

        [TestMethod]
        public void Array_Not_Included()
        {
            let a = new int[] { 0, 1 };
            a -= true;
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }

        [TestMethod]
        public void Bool_Array()
        {
            let a = true;
            a -= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Bool_TT()
        {
            let a = true;
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Bool_FT()
        {
            let a = false;
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Bool_TF()
        {
            let a = true;
            a -= false;
            Assert.IsNotNull(a);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void Bool_Bool_FF()
        {
            let a = false;
            a -= false;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Number()
        {
            let a = true;
            a -= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void Bool_String_Boolean()
        {
            let a = true;
            a -= "true";
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_String_Numeric()
        {
            let a = true;
            a -= "2";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void Bool_String()
        {
            let a = true;
            a -= "ru";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Te");
        }

        [TestMethod]
        public void Number_Array()
        {
            let a = 2;
            a -= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 0);
        }

        [TestMethod]
        public void Number_Bool()
        {
            let a = 2;
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void Number_Number()
        {
            let a = 2;
            a -= 3;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void Number_String_Boolean()
        {
            let a = 2;
            a -= "true";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void Number_String_Numeric()
        {
            let a = 2;
            a -= "3";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void Number_String()
        {
            let a = 1.5;
            a -= ".";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 15);
        }

        [TestMethod]
        public void String_Array()
        {
            let a = "Hello";
            a -= new string[] { "e", "l" };
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Ho");
        }

        [TestMethod]
        public void String_Boolean_Bool()
        {
            let a = "true";
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void String_Numeric_Bool()
        {
            let a = "2";
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void String_Bool()
        {
            let a = "UnTrue";
            a -= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Un");
        }

        [TestMethod]
        public void String_Boolean_Number()
        {
            let a = "true";
            a -= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void String_Numeric_Number()
        {
            let a = "2";
            a -= 3;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void String_Number()
        {
            let a = "One2Three";
            a -= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "OneThree");
        }

        [TestMethod]
        public void String_Boolean_String_Boolean()
        {
            let a = "false";
            a -= "true";
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void String_Boolean_String_Numeric()
        {
            let a = "true";
            a -= "2";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void String_Numeric_String_Boolean()
        {
            let a = "2";
            a -= "true";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 1);
        }

        [TestMethod]
        public void String_Numeric_String_Numeric()
        {
            let a = "2";
            a -= "3";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == -1);
        }

        [TestMethod]
        public void String_String()
        {
            let a = "Hello";
            a -= "l";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Heo");
        }
    }
}
