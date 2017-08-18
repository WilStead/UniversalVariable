namespace UniversalVariable.Math
{
    public static class Math
    {
#pragma warning disable IDE1006
        public static double e => System.Math.E;
#pragma warning restore IDE1006
        public static double E => System.Math.E;
        public static double Pi => System.Math.PI;
        public static double PI => System.Math.PI;

        /// <summary>
        /// Returns the absolute value of the specified variable. All values in an array are
        /// converted. Returns boolean and non-numeric string values unchanged.
        /// </summary>
        public static let Abs(let value)
        {
            if (value == null)
            {
                return new let();
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Abs(value._array[i]);
                    }
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Abs(value._number);
                    break;
                case let.InternalType.String:
                    if (double.TryParse(value._string, out var num))
                    {
                        result._type = let.InternalType.Number;
                        result._number = System.Math.Abs(num);
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the angle whose cosine is the value of the specified variable. All values in an
        /// array are converted. Treats booleans as their numeric equivalent. Returns NaN for
        /// non-numeric string values.
        /// </summary>
        public static let Acos(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Acos(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Acos(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Acos(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Acos(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the angle whose sine is the value of the specified variable. All values in an
        /// array are converted. Treats booleans as their numeric equivalent. Returns NaN for
        /// non-numeric string values.
        /// </summary>
        public static let Asin(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Asin(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Asin(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Asin(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Asin(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the angle whose tangent is the value of the specified variable. All values in an
        /// array are converted. Treats booleans as their numeric equivalent. Returns NaN for
        /// non-numeric string values.
        /// </summary>
        public static let Atan(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Atan(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Atan(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Atan(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Atan(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the angle whose tangent is the quotient of the two specified variable. All values
        /// in an array are converted. Treats booleans as their numeric equivalent. Returns NaN for
        /// non-numeric string values.
        /// </summary>
        public static let Atan2(let y, let x)
        {
            if (y == null || x == null)
            {
                return new let(double.NaN);
            }
            var result = y.DeepClone();
            if (y._type == let.InternalType.Array)
            {
                for (int i = 0; i < y._array.Count; i++)
                {
                    result._array[i] = Atan2(y._array[i], x);
                }
            }
            else if (x._type == let.InternalType.Array)
            {
                result.SetValue(x);
                for (int i = 0; i < x._array.Count; i++)
                {
                    result._array[i] = Atan2(y, x._array[i]);
                }
            }
            switch (y._type)
            {
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    switch (x._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Atan2(y.ToDouble(), x.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Atan2(y.ToDouble(), x._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(x._string, out var num2))
                            {
                                result._number = System.Math.Atan2(y.ToDouble(), num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.Number:
                    switch (x._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Atan2(y._number, x.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Atan2(y._number, x._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(x._string, out var num2))
                            {
                                result._number = System.Math.Atan2(y._number, num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(y._string, out var num))
                    {
                        switch (x._type)
                        {
                            case let.InternalType.Boolean:
                                result._number = System.Math.Atan2(num, x.ToDouble());
                                break;
                            case let.InternalType.Number:
                                result._number = System.Math.Atan2(num, x._number);
                                break;
                            case let.InternalType.String:
                                if (double.TryParse(x._string, out var num2))
                                {
                                    result._number = System.Math.Atan2(num, num2);
                                }
                                else
                                {
                                    result._number = double.NaN;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the smallest integral value that is greater than or equal to the numeric
        /// equivalent of the specified variable. All values in an array are converted.
        /// </summary>
        public static let Ceiling(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Ceiling(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = value.ToDouble();
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Ceiling(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Ceiling(num);
                    }
                    else
                    {
                        result._number = value._string.Length;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the cosine of the specified angle. All values in an array are converted. Treats
        /// booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Cos(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Cos(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Cos(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Cos(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Cos(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic cosine of the specified angle. All values in an array are
        /// converted. Treats booleans as their numeric equivalent. Returns NaN for non-numeric
        /// string values.
        /// </summary>
        public static let Cosh(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Cosh(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Cosh(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Cosh(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Cosh(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns e raised to the specified power. All values in an array are converted. Treats
        /// booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Exp(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Exp(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Exp(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Exp(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Exp(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the largest integral value less than or equal to the numeric equivalent of the
        /// specified variable. All values in an array are converted.
        /// </summary>
        public static let Floor(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Floor(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = value.ToDouble();
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Floor(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Floor(num);
                    }
                    else
                    {
                        result._number = value._string.Length;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the natural (base e) logarithm of the specified variable. All values in an array
        /// are converted. Treats booleans as their numeric equivalent. Returns NaN for non-numeric
        /// string values.
        /// </summary>
        public static let Log(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Log(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Log(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Log(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Log(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the logarithm of the first specified value in the base specified by the second.
        /// All values in an array are converted. Treats booleans as their numeric equivalent.
        /// Returns NaN for non-numeric string values.
        /// </summary>
        public static let Log(let a, let newBase)
        {
            if (a == null || newBase == null)
            {
                return new let(double.NaN);
            }
            var result = a.DeepClone();
            if (a._type == let.InternalType.Array)
            {
                for (int i = 0; i < a._array.Count; i++)
                {
                    result._array[i] = Log(a._array[i], newBase);
                }
            }
            else if (newBase._type == let.InternalType.Array)
            {
                result.SetValue(newBase);
                for (int i = 0; i < newBase._array.Count; i++)
                {
                    result._array[i] = Log(a, newBase._array[i]);
                }
            }
            switch (a._type)
            {
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    switch (newBase._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Log(a.ToDouble(), newBase.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Log(a.ToDouble(), newBase._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(newBase._string, out var num2))
                            {
                                result._number = System.Math.Log(a.ToDouble(), num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.Number:
                    switch (newBase._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Log(a._number, newBase.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Log(a._number, newBase._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(newBase._string, out var num2))
                            {
                                result._number = System.Math.Log(a._number, num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(a._string, out var num))
                    {
                        switch (newBase._type)
                        {
                            case let.InternalType.Boolean:
                                result._number = System.Math.Log(num, newBase.ToDouble());
                                break;
                            case let.InternalType.Number:
                                result._number = System.Math.Log(num, newBase._number);
                                break;
                            case let.InternalType.String:
                                if (double.TryParse(newBase._string, out var num2))
                                {
                                    result._number = System.Math.Log(num, num2);
                                }
                                else
                                {
                                    result._number = double.NaN;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the base 10 logarithm of the specified variable. All values in an array are
        /// converted. Treats booleans as their numeric equivalent. Returns NaN for non-numeric
        /// string values.
        /// </summary>
        public static let Log10(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Log10(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Log10(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Log10(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Log10(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the larger of two specified variables.
        /// </summary>
        public static let Max(let val1, let val2)
        {
            if (val1 == null)
            {
                return val2 ?? new let();
            }
            if (val2 == null)
            {
                return val1;
            }
            return val1.CompareTo(val2) >= 0 ? val1 : val2;
        }

        /// <summary>
        /// Returns the smaller of two specified variables.
        /// </summary>
        public static let Min(let val1, let val2)
        {
            if (val1 == null || val2 == null)
            {
                return new let();
            }
            return val1.CompareTo(val2) < 0 ? val1 : val2;
        }

        /// <summary>
        /// Returns the value of the first specified variable raised to the power specified by the
        /// second variable. All values in an array are converted. Treats booleans as their numeric
        /// equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Pow(let x, let y)
        {
            if (x == null || y == null)
            {
                return new let(double.NaN);
            }
            var result = x.DeepClone();
            if (x._type == let.InternalType.Array)
            {
                for (int i = 0; i < x._array.Count; i++)
                {
                    result._array[i] = Pow(x._array[i], y);
                }
            }
            else if (y._type == let.InternalType.Array)
            {
                result.SetValue(y);
                for (int i = 0; i < y._array.Count; i++)
                {
                    result._array[i] = Pow(x, y._array[i]);
                }
            }
            switch (x._type)
            {
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    switch (y._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Pow(x.ToDouble(), y.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Pow(x.ToDouble(), y._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(y._string, out var num2))
                            {
                                result._number = System.Math.Pow(x.ToDouble(), num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.Number:
                    switch (y._type)
                    {
                        case let.InternalType.Boolean:
                            result._number = System.Math.Pow(x._number, y.ToDouble());
                            break;
                        case let.InternalType.Number:
                            result._number = System.Math.Pow(x._number, y._number);
                            break;
                        case let.InternalType.String:
                            if (double.TryParse(y._string, out var num2))
                            {
                                result._number = System.Math.Pow(x._number, num2);
                            }
                            else
                            {
                                result._number = double.NaN;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(x._string, out var num))
                    {
                        switch (y._type)
                        {
                            case let.InternalType.Boolean:
                                result._number = System.Math.Pow(num, y.ToDouble());
                                break;
                            case let.InternalType.Number:
                                result._number = System.Math.Pow(num, y._number);
                                break;
                            case let.InternalType.String:
                                if (double.TryParse(y._string, out var num2))
                                {
                                    result._number = System.Math.Pow(num, num2);
                                }
                                else
                                {
                                    result._number = double.NaN;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the specified variable's numeric equivalent rounded to the nearest integral
        /// value. All values in an array are converted.
        /// </summary>
        public static let Round(let value)
        {
            if (value == null)
            {
                return new let();
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Round(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = value.ToDouble();
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Round(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Round(num);
                    }
                    else
                    {
                        result._number = value._string.Length;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns an integer that indicates the sign of the specified variable's numeric
        /// equivalent. All values in an array are converted.
        /// </summary>
        public static let Sign(let value)
        {
            if (value == null)
            {
                return new let(0);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Sign(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Sign(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Sign(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Sign(num);
                    }
                    else
                    {
                        result._number = System.Math.Sign(value._string.Length);
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the sine of the specified angle. All values in an array are converted. Treats
        /// booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Sin(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Sin(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Sin(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Sin(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Sin(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic sine of the specified angle. All values in an array are converted.
        /// Treats booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Sinh(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Sinh(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Sinh(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Sinh(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Sinh(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the square root of the specified variable. All values in an array are converted.
        /// Treats booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Sqrt(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Sqrt(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Sqrt(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Sqrt(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Sqrt(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the tangent of the specified angle. All values in an array are converted. Treats
        /// booleans as their numeric equivalent. Returns NaN for non-numeric string values.
        /// </summary>
        public static let Tan(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Tan(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Tan(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Tan(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Tan(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Returns the hyperbolic tangent of the specified angle. All values in an array are
        /// converted. Treats booleans as their numeric equivalent. Returns NaN for non-numeric
        /// string values.
        /// </summary>
        public static let Tanh(let value)
        {
            if (value == null)
            {
                return new let(double.NaN);
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Tanh(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = System.Math.Tanh(value.ToDouble());
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Tanh(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Tanh(num);
                    }
                    else
                    {
                        result._number = double.NaN;
                    }
                    break;
            }
            return result;
        }

        /// <summary>
        /// Calculates the integral part of the specified variable's numeric equivalent. All values
        /// in an array are converted.
        /// </summary>
        public static let Truncate(let value)
        {
            if (value == null)
            {
                return new let();
            }
            let result = value.DeepClone();
            switch (value._type)
            {
                case let.InternalType.Array:
                    for (int i = 0; i < value._array.Count; i++)
                    {
                        result._array[i] = Truncate(value._array[i]);
                    }
                    break;
                case let.InternalType.Boolean:
                    result._type = let.InternalType.Number;
                    result._number = value.ToDouble();
                    break;
                case let.InternalType.Number:
                    result._number = System.Math.Truncate(value._number);
                    break;
                case let.InternalType.String:
                    result._type = let.InternalType.Number;
                    if (double.TryParse(value._string, out var num))
                    {
                        result._number = System.Math.Truncate(num);
                    }
                    else
                    {
                        result._number = value._string.Length;
                    }
                    break;
            }
            return result;
        }
    }
}
