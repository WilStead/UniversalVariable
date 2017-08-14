using System;
using System.Collections.Generic;

namespace UniversalTypes
{
    public static class TypeExtensions
    {
        private static HashSet<Type> numericTypes = new HashSet<Type>
        {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        };

        public static bool IsNumericType(this Type type) => numericTypes.Contains(type);
    }
}
