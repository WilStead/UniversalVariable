using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalTypes.Test
{
    [TestClass]
    public class UniversalVariable_GreaterThan
    {
        [TestMethod]
        public void Untyped_Null()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            Assert.IsTrue(a > null);
        }

        [TestMethod]
        public void Untyped()
        {
            let a = new object[,] { { "ChildProp", 5 } };
            Assert.IsFalse(a > true);
        }

        [TestMethod]
        public void Array_Array_T()
        {
            let a = new int[] { 0, 1 };
            Assert.IsTrue(a > new int[] { 1 });
        }

        [TestMethod]
        public void Array_Array_F()
        {
            let a = new int[] { 0, 1 };
            Assert.IsFalse(a > new int[] { 1, 2, 3 });
        }

        [TestMethod]
        public void Array_Bool_T()
        {
            let a = new int[] { 0, 1, 2 };
            Assert.IsTrue(a > true);
        }

        [TestMethod]
        public void Array_Bool_F()
        {
            let a = new int[] { 0 };
            Assert.IsFalse(a > true);
        }

        [TestMethod]
        public void Array_Number_T()
        {
            let a = new int[] { 0, 1 };
            Assert.IsTrue(a > 1);
        }

        [TestMethod]
        public void Array_Number_F()
        {
            let a = new int[] { 0 };
            Assert.IsFalse(a > 1);
        }

        [TestMethod]
        public void Array_String_T()
        {
            let a = new int[] { 0, 1, 2 };
            Assert.IsTrue(a > "Hi");
        }

        [TestMethod]
        public void Array_String_F()
        {
            let a = new int[] { 0, 1, 3 };
            Assert.IsFalse(a > "Hello");
        }

        [TestMethod]
        public void Bool_Array_T()
        {
            let a = true;
            Assert.IsTrue(a > new int[] { });
        }

        [TestMethod]
        public void Bool_Array_F()
        {
            let a = true;
            Assert.IsFalse(a > new int[] { 0 });
        }

        [TestMethod]
        public void Bool_Bool_T()
        {
            let a = true;
            Assert.IsTrue(a > false);
        }

        [TestMethod]
        public void Bool_Bool_F()
        {
            let a = false;
            Assert.IsFalse(a > true);
        }

        [TestMethod]
        public void Bool_Number_T()
        {
            let a = true;
            Assert.IsTrue(a > 0);
        }

        [TestMethod]
        public void Bool_Number_F()
        {
            let a = true;
            Assert.IsFalse(a > 2);
        }

        [TestMethod]
        public void Bool_String_Boolean()
        {
            let a = true;
            Assert.IsTrue(a > "false");
        }

        [TestMethod]
        public void Bool_String_Numeric()
        {
            let a = true;
            Assert.IsTrue(a > "0");
        }

        [TestMethod]
        public void Bool_String_T()
        {
            let a = true;
            Assert.IsTrue(a > "");
        }

        [TestMethod]
        public void Bool_String_F()
        {
            let a = true;
            Assert.IsFalse(a > "Hello");
        }

        [TestMethod]
        public void Number_Array_T()
        {
            let a = 4;
            Assert.IsTrue(a > new int[] { 0, 1, 2 });
        }

        [TestMethod]
        public void Number_Array_F()
        {
            let a = 2;
            Assert.IsFalse(a > new int[] { 0, 1, 2 });
        }

        [TestMethod]
        public void Number_Bool_T()
        {
            let a = 2;
            Assert.IsTrue(a > true);
        }

        [TestMethod]
        public void Number_Bool_F()
        {
            let a = 0;
            Assert.IsFalse(a > true);
        }

        [TestMethod]
        public void Number_Number_T()
        {
            let a = 3;
            Assert.IsTrue(a > 2);
        }

        [TestMethod]
        public void Number_Number_F()
        {
            let a = 1;
            Assert.IsFalse(a > 2);
        }

        [TestMethod]
        public void Number_String_Boolean()
        {
            let a = 0;
            Assert.IsFalse(a > "true");
        }

        [TestMethod]
        public void Number_String_Numeric()
        {
            let a = 2;
            Assert.IsTrue(a > "1");
        }

        [TestMethod]
        public void Number_String_T()
        {
            let a = 6;
            Assert.IsTrue(a > "Hello");
        }

        [TestMethod]
        public void Number_String_F()
        {
            let a = 2;
            Assert.IsFalse(a > "Hello");
        }

        [TestMethod]
        public void String_Array_T()
        {
            let a = "Hello";
            Assert.IsTrue(a > new int[] { 0, 1, 2 });
        }

        [TestMethod]
        public void String_Array_F()
        {
            let a = "Hi";
            Assert.IsFalse(a > new int[] { 0, 1, 2 });
        }

        [TestMethod]
        public void String_Boolean_Bool()
        {
            let a = "true";
            Assert.IsTrue(a > false);
        }

        [TestMethod]
        public void String_Numeric_Bool()
        {
            let a = "2";
            Assert.IsTrue(a > true);
        }

        [TestMethod]
        public void String_Bool_T()
        {
            let a = "Hello";
            Assert.IsTrue(a > true);
        }

        [TestMethod]
        public void String_Bool_F()
        {
            let a = "";
            Assert.IsFalse(a > true);
        }

        [TestMethod]
        public void String_Boolean_Number()
        {
            let a = "true";
            Assert.IsTrue(a > 0);
        }

        [TestMethod]
        public void String_Numeric_Number()
        {
            let a = "3";
            Assert.IsTrue(a > 2);
        }

        [TestMethod]
        public void String_Number_T()
        {
            let a = "Hello";
            Assert.IsTrue(a > 3);
        }

        [TestMethod]
        public void String_Number_F()
        {
            let a = "Hi";
            Assert.IsFalse(a > 3);
        }

        [TestMethod]
        public void String_Boolean_String_Boolean()
        {
            let a = "true";
            Assert.IsTrue(a > "false");
        }

        [TestMethod]
        public void String_Boolean_String_Numeric()
        {
            let a = "true";
            Assert.IsTrue(a > "0");
        }

        [TestMethod]
        public void String_Numeric_String_Boolean()
        {
            let a = "2";
            Assert.IsTrue(a > "true");
        }

        [TestMethod]
        public void String_Numeric_String_Numeric()
        {
            let a = "3";
            Assert.IsTrue(a > "1");
        }

        [TestMethod]
        public void String_String_T()
        {
            let a = "Hello";
            Assert.IsTrue(a > "Hi");
        }

        [TestMethod]
        public void String_String_F()
        {
            let a = "Hi";
            Assert.IsFalse(a > "Hello");
        }
    }
}
