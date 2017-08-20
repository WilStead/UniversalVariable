using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

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

        public class TestChildArgClass
        {
            public bool B { get; set; }
        }

        private class TestArgClass
        {
            public int X;
            public string Y { get; set; }
            public ICollection<TestChildArgClass> Children { get; set; }
        }

        [TestMethod]
        public void Assignment_Children_Class()
        {
            var tac = new TestArgClass
            {
                X = 1,
                Y = "Hello",
                Children = new List<TestChildArgClass> { new TestChildArgClass { B = true }, new TestChildArgClass { B = true }, new TestChildArgClass { B = false } }
            };

            let a = new let(tac);
            Assert.IsNotNull(a);
            Assert.AreEqual(3, a.PropertyCount);
            Assert.IsTrue(a.ContainsProperty("X"));
            Assert.IsTrue(a["X"] == 1);
            Assert.IsTrue(a.ContainsProperty("Y"));
            Assert.IsTrue(a["Y"] == "Hello");
            Assert.IsTrue(a.ContainsProperty("Children"));
            Assert.AreEqual(3, a["Children"].Length);
            Assert.IsTrue(a["Children"][0].ContainsProperty("B"));
            Assert.IsTrue(a["Children"][0]["B"]);

            a["Children"].ForEach(c => c["B"] = false);
            Assert.IsTrue(a["Children"].TrueForAll(c => !c["B"]));

            var d = a.ToDynamic();
            Assert.IsNotNull(d);
            Assert.IsTrue(d.X == 1);
            Assert.IsTrue(d.Y == "Hello");
            Assert.IsFalse(d.Children.Value[0].B);
            Assert.IsFalse(d.Children.Value[1].B);
            Assert.IsFalse(d.Children.Value[2].B);
        }

        private void SetValue(object original, Type type, dynamic updated)
        {
            if ((updated as IDictionary<string, object>).TryGetValue("Value", out var value))
            {
                if (type.IsArray)
                {
                    var list = original as IList<object>;
                    list.Clear();
                    for (int i = 0; i < (int)typeof(object[]).GetProperty("Length").GetValue(value); i++)
                    {
                        list.Add((value as object[])[i]);
                    }
                }
                original = value;
            }
            else
            {
                foreach (var prop in updated as IDictionary<string, object>)
                {
                    var argField = type.GetRuntimeField(prop.Key);
                    if (argField == null)
                    {
                        var argProp = type.GetRuntimeProperty(prop.Key);
                        if (prop.Value.GetType() == typeof(ExpandoObject))
                        {
                            SetValue(argProp.GetValue(original), argProp.PropertyType, prop.Value);
                        }
                        else
                        {
                            argProp.SetValue(original, prop.Value);
                        }
                    }
                    else
                    {
                        if (prop.Value.GetType() == typeof(ExpandoObject))
                        {
                            SetValue(argField.GetValue(original), argField.FieldType, prop.Value);
                        }
                        else
                        {
                            argField.SetValue(original, prop.Value);
                        }
                    }
                }
            }
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
