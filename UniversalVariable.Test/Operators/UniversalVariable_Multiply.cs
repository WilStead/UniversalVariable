﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalVariable.Test
{
    [TestClass]
    public class UniversalVariable_Multiply
    {
        [TestMethod]
        public void Untyped_Null()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a *= null;
            Assert.IsTrue(a == null);
        }

        [TestMethod]
        public void Untyped_Array()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a *= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0]["ChildProp"] == 5);
            Assert.IsTrue(a[1]["ChildProp"] == 5);
        }

        [TestMethod]
        public void Untyped_NonArray()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a.ContainsProperty("ChildProp"));
            Assert.IsTrue(a["ChildProp"] == 5);
        }

        [TestMethod]
        public void Array_Array()
        {
            let a = new int[] { 0, 1 };
            a *= new int[] { 1, 2 };
            Assert.IsNotNull(a);
            Assert.AreEqual(3, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
            Assert.IsTrue(a[2] == 2);
        }

        [TestMethod]
        public void Array_Bool()
        {
            let a = new int[] { 0, 1 };
            a *= true;
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }

        [TestMethod]
        public void Array_Number()
        {
            let a = new int[] { 0, 1 };
            a *= 2;
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
        }

        [TestMethod]
        public void Array_String()
        {
            let a = new int[] { 0, 1 };
            a *= "Hello";
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == string.Empty);
            Assert.IsTrue(a[1] == "Hello");
        }

        [TestMethod]
        public void Bool_Array()
        {
            let a = true;
            a *= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 1);
        }

        [TestMethod]
        public void Bool_Bool_TT()
        {
            let a = true;
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void Bool_Bool_FT()
        {
            let a = false;
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Bool_TF()
        {
            let a = true;
            a *= false;
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void Bool_Bool_FF()
        {
            let a = false;
            a *= false;
            Assert.IsNotNull(a);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void Bool_Number()
        {
            let a = true;
            a *= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void Bool_String_Boolean()
        {
            let a = true;
            a *= "true";
            Assert.IsNotNull(a);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void Bool_String_Numeric()
        {
            let a = true;
            a *= "2";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void Bool_String()
        {
            let a = true;
            a *= "Hello";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Hello");
        }

        [TestMethod]
        public void Number_Array()
        {
            let a = 2;
            a *= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == 0);
            Assert.IsTrue(a[1] == 2);
        }

        [TestMethod]
        public void Number_Bool()
        {
            let a = 2;
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void Number_Number()
        {
            let a = 2;
            a *= 3;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 6);
        }

        [TestMethod]
        public void Number_String_Boolean()
        {
            let a = 2;
            a *= "true";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void Number_String_Numeric()
        {
            let a = 2;
            a *= "3";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 6);
        }

        [TestMethod]
        public void Number_String()
        {
            let a = 2;
            a *= "Hello";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "HelloHello");
        }

        [TestMethod]
        public void String_Array()
        {
            let a = "Hello";
            a *= new int[] { 0, 1 };
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.Length);
            Assert.IsTrue(a[0] == string.Empty);
            Assert.IsTrue(a[1] == "Hello");
        }

        [TestMethod]
        public void String_Boolean_Bool()
        {
            let a = "true";
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a);
        }

        [TestMethod]
        public void String_Numeric_Bool()
        {
            let a = "2";
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void String_Bool()
        {
            let a = "Hello";
            a *= true;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Hello");
        }

        [TestMethod]
        public void String_Boolean_Number()
        {
            let a = "true";
            a *= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void String_Numeric_Number()
        {
            let a = "2";
            a *= 3;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 6);
        }

        [TestMethod]
        public void String_Number()
        {
            let a = "Hello";
            a *= 2;
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "HelloHello");
        }

        [TestMethod]
        public void String_Boolean_String_Boolean()
        {
            let a = "false";
            a *= "true";
            Assert.IsNotNull(a);
            Assert.IsFalse(a);
        }

        [TestMethod]
        public void String_Boolean_String_Numeric()
        {
            let a = "true";
            a *= "2";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void String_Numeric_String_Boolean()
        {
            let a = "2";
            a *= "true";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 2);
        }

        [TestMethod]
        public void String_Numeric_String_Numeric()
        {
            let a = "2";
            a *= "3";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == 6);
        }

        [TestMethod]
        public void String_String()
        {
            let a = "Hello";
            a *= "World";
            Assert.IsNotNull(a);
            Assert.IsTrue(a == "Hello*World");
        }
    }
}
