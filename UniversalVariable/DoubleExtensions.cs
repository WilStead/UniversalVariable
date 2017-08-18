using System;

namespace UniversalVariable
{
    public static class DoubleExtensions
    {
        public static bool IsIntegral(this double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d))
            {
                return false;
            }
            return (d % 1).IsNearlyZero();
        }

        public static bool IsNearlyEqual(this double d, double value)
        {
            if (double.IsNaN(d))
            {
                return double.IsNaN(value);
            }
            if (double.IsPositiveInfinity(d))
            {
                return double.IsPositiveInfinity(value);
            }
            if (double.IsNegativeInfinity(d))
            {
                return double.IsNegativeInfinity(value);
            }
            if (d == value)
            {
                return true;
            }

            var diff = System.Math.Abs(d - value);
            var epsilon = System.Math.Max(d, value) * 1e-15;
            if ((d == 0 || d.IsNearlyZero())
                && (value == 0 || value.IsNearlyZero()))
            {
                return true;
            }
            if (d == 0 || value == 0 || diff < double.Epsilon)
            {
                return diff < epsilon;
            }

            return diff / (System.Math.Abs(d) + System.Math.Abs(value)) < epsilon;
        }

        public static bool IsNearlyZero(this double d)
        {
            if (double.IsNaN(d) || double.IsInfinity(d))
            {
                return false;
            }
            if (d == 0)
            {
                return true;
            }

            return System.Math.Abs(d) < 1e-15;
        }
    }
}
