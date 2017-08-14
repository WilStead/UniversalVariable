using System;

namespace UniversalTypes
{
    public static class DoubleExtensions
    {
        public static bool IsNearlyEqual(this double d, double value)
        {
            if (d == value)
            {
                return true;
            }

            var diff = Math.Abs(d - value);
            var epsilon = Math.Max(d, value) * 1e-15;
            if ((d == 0 || d.IsNearlyZero())
                && (value == 0 || value.IsNearlyZero()))
            {
                return true;
            }
            if (d == 0 || value == 0 || diff < double.Epsilon)
            {
                return diff < epsilon;
            }

            return diff / (Math.Abs(d) + Math.Abs(value)) < epsilon;
        }

        public static bool IsNearlyZero(this double d)
        {
            if (d == 0)
            {
                return true;
            }

            return Math.Abs(d) < 1e-15;
        }
    }
}
