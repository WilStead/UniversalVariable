using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UniversalVariable.Test.Operators
{
    [TestClass]
    public class UniversalVariable_Math
    {
        [TestMethod]
        public void Math()
        {
            let a = 1;
            let b = 0;
            b["c"] = 2;
            let d = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            while (a < 10)
            {
                if (a == 1)
                {
                    b["c"] = System.Math.Pow(b["c"], 2);
                }
                else if (a % 2 == 0)
                {
                    b["c"] *= d[a];
                }
                else
                {
                    b["c"] -= d[a] * d[a - 2];
                }
                a++;
            }
            Assert.IsNotNull(b);
            Assert.IsTrue(b.Properties.Contains("c"));
            Assert.IsInstanceOfType(b["c"].Value, typeof(int));
            Assert.AreEqual(-103, b["c"].Value);
        }
    }
}
