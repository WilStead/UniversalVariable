using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace UniversalTypes
{
#pragma warning disable IDE1006
    public class let : IEquatable<let>, IComparable
#pragma warning restore IDE1006
    {
        internal enum InternalType { None, Array, Boolean, Number, String }

        internal List<let> _array = new List<let>();
        internal bool _boolean;
        internal Dictionary<string, let> _children = new Dictionary<string, let>();
        internal double _number = double.NaN;
        internal string _string;
        internal InternalType _type = InternalType.None;

        public let this[int index]
        {
            get
            {
                if (index < 0)
                {
                    return new let();
                }
                PadRight(index + 1);
                return _array[index];
            }
            set
            {
                if (index < 0)
                {
                    return;
                }
                PadRight(index + 1);
                _array[index] = value ?? new let();
            }
        }

        /// <summary>
        /// Gets the length of an array or string, or the integer equivalent of other variable types.
        /// Gets 0 for an empty variable.
        /// </summary>
        public int Length
        {
            get
            {
                switch (_type)
                {
                    case InternalType.Array:
                        return _array.Count;
                    case InternalType.Boolean:
                        return _boolean ? 1 : 0;
                    case InternalType.Number:
                        return (int)Math.Round(_number);
                    case InternalType.String:
                        return _string.Length;
                    default:
                        return 0;
                }
            }
        }

        public let this[string index]
        {
            get
            {
                if (int.TryParse(index, out var intIndex))
                {
                    return this[intIndex];
                }
                if (_children.TryGetValue(index, out var value))
                {
                    return value;
                }
                else
                {
                    let newValue = new let();
                    _children.Add(index, newValue);
                    return newValue;
                }
            }

            set
            {
                if (int.TryParse(index, out var intIndex))
                {
                    this[intIndex] = value ?? new let();
                }
                else
                {
                    _children[index] = value ?? new let();
                }
            }
        }

        /// <summary>
        /// An enumeration of all the child properties of this variable.
        /// Does not include built-in properties such as <see cref="Length"/>.
        /// </summary>
        public IEnumerable<string> Properties => _children.Keys.AsEnumerable();

        /// <summary>
        /// The number of child properties of this variable.
        /// Does not include built-in properties such as <see cref="Length"/>.
        /// </summary>
        public int PropertyCount => _children.Count;

        /// <summary>
        /// Gets this variable's value as an object. Child properties are not represented. To get an
        /// object which contains all child properties (but not the value), use <see cref="ToDynamic"/>.
        /// </summary>
        public object Value
        {
            get => ToObject();
            set => SetValue(value);
        }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="let"/>.
        /// </summary>
        public let() { }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a deep copy of the given variable.
        /// </summary>
        public let(let other) => Copy(other);

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as an array with the given values.
        /// </summary>
        public let(IEnumerable<let> collection)
        {
            _type = InternalType.Array;
            _array.AddRange(collection);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> with the given values as children.
        /// </summary>
        public let(IDictionary<string, object> collection)
        {
            foreach (var item in collection)
            {
                if (item.Key == "Value")
                {
                    SetValue(item.Value);
                }
                else
                {
                    _children.Add(item.Key, new let(item.Value));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a boolean with the given value.
        /// </summary>
        public let(bool value)
        {
            _type = InternalType.Boolean;
            _boolean = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(byte value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(decimal value)
        {
            _type = InternalType.Number;
            _number = (double)value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(double value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(float value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(int value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(long value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> with the given value.
        /// </summary>
        public let(object value) => SetValue(value);

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as an array with the given collection.
        /// </summary>
        public let(object[] collection)
        {
            _type = InternalType.Array;
            foreach (var item in collection)
            {
                _array.Add(new let(item));
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> with the given children.
        /// </summary>
        public let(object[][] collection)
        {
            foreach (var item in collection)
            {
                if (item.Length == 0)
                {
                    continue;
                }
                else
                {
                    if (item[0].ToString() == "Value")
                    {
                        if (item.Length > 1)
                        {
                            SetValue(item[1]);
                        }
                    }
                    else if (item.Length == 1)
                    {
                        _children.Add(item[0].ToString(), new let());
                    }
                    else
                    {
                        _children.Add(item[0].ToString(), new let(item[1]));
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> with the given children.
        /// </summary>
        public let(object[,] collection)
        {
            if (collection.GetLength(1) == 0)
            {
                return;
            }
            for (int i = 0; i < collection.GetLength(0); i++)
            {
                if (collection.GetLength(1) == 1)
                {
                    if (collection[i, 0].ToString() != "Value")
                    {
                        _children.Add(collection[i, 0].ToString(), new let());
                    }
                }
                else if (collection[i, 0].ToString() == "Value")
                {
                    SetValue(collection[i, 1]);
                }
                else
                {
                    _children.Add(collection[i, 0].ToString(), new let(collection[i, 1]));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(sbyte value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(short value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a string with the given value.
        /// </summary>
        public let(string value)
        {
            _type = InternalType.String;
            _string = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(uint value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(ulong value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="let"/> as a number with the given value.
        /// </summary>
        public let(ushort value)
        {
            _type = InternalType.Number;
            _number = value;
        }

        #endregion Constructors

        #region Operators

        public static let operator +(let a) => a ?? new let();

        public static let operator -(let a)
        {
            if (a == null)
            {
                return new let(true);
            }
            var result = a.ShallowCopy();
            switch (a._type)
            {
                case InternalType.Boolean:
                    result._boolean = !a._boolean;
                    break;
                case InternalType.Number:
                    result._number = -a._number;
                    break;
                case InternalType.Array:
                case InternalType.String:
                    result.Reverse();
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator !(let a)
        {
            if (a == null)
            {
                return new let(true);
            }
            var result = a.ShallowCopy();
            switch (a._type)
            {
                case InternalType.Boolean:
                    result._boolean = !a._boolean;
                    break;
                case InternalType.Number:
                    result._number = -a._number;
                    break;
                case InternalType.Array:
                case InternalType.String:
                    result.Reverse();
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator ~(let a)
        {
            if (a == null)
            {
                return new let();
            }
            var result = a.ShallowCopy();
            switch (a._type)
            {
                case InternalType.Array:
                    break;
                case InternalType.Boolean:
                    result._boolean = !a._boolean;
                    break;
                case InternalType.Number:
                    if ((a._number % 1).IsNearlyZero()
                        && Math.Abs(a._number) < ulong.MaxValue)
                    {
                        var sign = Math.Sign(a._number);
                        var ul = Convert.ToUInt64(Math.Abs(a._number));
                        result._number = sign * (double)~ul;
                    }
                    break;
                case InternalType.String:
                    var upper = a._string.ToUpper();
                    if (a._string == upper)
                    {
                        result._string = a._string.ToLower();
                    }
                    else
                    {
                        result._string = upper;
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator ++(let a)
        {
            if (a == null)
            {
                return new let(1);
            }
            var result = a.ShallowCopy();
            switch (a._type)
            {
                case InternalType.None:
                    result._type = InternalType.Number;
                    result._number = 1;
                    break;
                case InternalType.Array:
                    result._array.Add(new let());
                    break;
                case InternalType.Boolean:
                    result._boolean = !a._boolean;
                    break;
                case InternalType.Number:
                    result._number++;
                    break;
                case InternalType.String:
                    if (double.TryParse(a._string, out var value))
                    {
                        result._type = InternalType.Number;
                        result._number = ++value;
                    }
                    else if (bool.TryParse(a._string, out var bValue))
                    {
                        result._type = InternalType.Boolean;
                        result._boolean = !bValue;
                    }
                    else
                    {
                        result._string += " ";
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator --(let a)
        {
            if (a == null)
            {
                return new let(-1);
            }
            var result = a.ShallowCopy();
            switch (a._type)
            {
                case InternalType.None:
                    result._type = InternalType.Number;
                    result._number = -1;
                    break;
                case InternalType.Array:
                    result._array.RemoveAt(a._array.Count - 1);
                    break;
                case InternalType.Boolean:
                    result._boolean = !a._boolean;
                    break;
                case InternalType.Number:
                    result._number--;
                    break;
                case InternalType.String:
                    if (double.TryParse(a._string, out var value))
                    {
                        result._type = InternalType.Number;
                        result._number = --value;
                    }
                    else if (bool.TryParse(a._string, out var bValue))
                    {
                        result._type = InternalType.Boolean;
                        result._boolean = !bValue;
                    }
                    else
                    {
                        result._string = a._string.Substring(0, a._string.Length - 1);
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator +(let a, let b)
        {
            if (a == null)
            {
                return b?.ShallowCopy() ?? new let();
            }
            var result = a.ShallowCopy();
            if (b == null)
            {
                return result;
            }
            if (a._type == InternalType.Array)
            {
                if (b._type == InternalType.Array)
                {
                    result._array.AddRange(b._array);
                }
                else
                {
                    result._array.Add(b);
                }
            }
            else if (b._type == InternalType.Array)
            {
                result._type = InternalType.Array;
                result._array.AddRange(b._array);
                if (a._type != InternalType.None)
                {
                    result.Insert(0, a);
                }
            }
            else
            {
                switch (a._type)
                {
                    case InternalType.Boolean:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._boolean |= b._boolean;
                                break;
                            case InternalType.Number:
                                result._type = InternalType.Number;
                                result._number = (a._boolean ? 1 : 0) + b._number;
                                break;
                            case InternalType.String:
                                if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._boolean &= bValue;
                                }
                                else if (double.TryParse(b._string, out var value))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (a._boolean ? 1 : 0) + value;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = a._boolean.ToString() + b._string;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.Number:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._number += b._boolean ? 1 : 0;
                                break;
                            case InternalType.Number:
                                result._number += b._number;
                                break;
                            case InternalType.String:
                                if (double.TryParse(b._string, out var value))
                                {
                                    result._number += value;
                                }
                                else if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._number += bValue ? 1 : 0;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = a._number.ToString() + b._string;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.String:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                if (bool.TryParse(a._string, out var bValue))
                                {
                                    result._type = InternalType.Boolean;
                                    result._boolean = bValue || b._boolean;
                                }
                                else if (double.TryParse(a._string, out var nValue))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue + (b._boolean ? 1 : 0);
                                }
                                else
                                {
                                    result._string += b._boolean.ToString();
                                }
                                break;
                            case InternalType.Number:
                                if (double.TryParse(a._string, out var nValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue2 + b._number;
                                }
                                else if (bool.TryParse(a._string, out var bValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (bValue2 ? 1 : 0) + b._number;
                                }
                                else
                                {
                                    result._string += b._number.ToString();
                                }
                                break;
                            case InternalType.String:
                                if (double.TryParse(a._string, out var value1))
                                {
                                    if (double.TryParse(b._string, out var value2))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 + value2;
                                    }
                                    else if (bool.TryParse(b._string, out var bValue3))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 + (bValue3 ? 1 : 0);
                                    }
                                    else
                                    {
                                        result._string += b._string;
                                    }
                                }
                                else if (bool.TryParse(a._string, out var bValue3))
                                {
                                    if (bool.TryParse(b._string, out var bValue4))
                                    {
                                        result._type = InternalType.Boolean;
                                        result._boolean = bValue3 || bValue4;
                                    }
                                    else if (double.TryParse(b._string, out var value))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = (bValue3 ? 1 : 0) + value;
                                    }
                                    else
                                    {
                                        result._string += b._string;
                                    }
                                }
                                else
                                {
                                    result._string += b._string;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        result.CopyValue(b);
                        break;
                }
            }
            return result;
        }

        public static let operator -(let a, let b)
        {
            if (a == null)
            {
                return b == null ? new let() : -b;
            }
            var result = a.ShallowCopy();
            if (b == null)
            {
                return result;
            }
            switch (a._type)
            {
                case InternalType.Array:
                    if (b._type == InternalType.Array)
                    {
                        result._array.RemoveAll(i => b._array.Any(j => i == j));
                    }
                    else
                    {
                        result._array.RemoveAll(i => i == b);
                    }
                    break;
                case InternalType.Boolean:
                    switch (b._type)
                    {
                        case InternalType.Array:
                            result._boolean &= !b.ToBoolean();
                            break;
                        case InternalType.Boolean:
                            result._boolean &= !b._boolean;
                            break;
                        case InternalType.Number:
                            result._type = InternalType.Number;
                            result._number = (a._boolean ? 1 : 0) - b._number;
                            break;
                        case InternalType.String:
                            if (bool.TryParse(b._string, out var bValue))
                            {
                                result._boolean &= !bValue;
                            }
                            else if (double.TryParse(b._string, out var value))
                            {
                                result._type = InternalType.Number;
                                result._number = (a._boolean ? 1 : 0) - value;
                            }
                            else
                            {
                                result._type = InternalType.String;
                                result._string = a._boolean.ToString().Replace(b._string, string.Empty);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case InternalType.Number:
                    switch (b._type)
                    {
                        case InternalType.Array:
                            result._number -= b.Length;
                            break;
                        case InternalType.Boolean:
                            result._number -= b._boolean ? 1 : 0;
                            break;
                        case InternalType.Number:
                            result._number -= b._number;
                            break;
                        case InternalType.String:
                            if (double.TryParse(b._string, out var value))
                            {
                                result._number -= value;
                            }
                            else if (bool.TryParse(b._string, out var bValue))
                            {
                                result._number -= bValue ? 1 : 0;
                            }
                            else
                            {
                                var oStr = a._number.ToString();
                                var nStr = oStr.Replace(b._string, string.Empty);
                                if (nStr != oStr)
                                {
                                    if (double.TryParse(nStr, out var nVal))
                                    {
                                        result._number = nVal;
                                    }
                                    else
                                    {
                                        result._type = InternalType.String;
                                        result._string = nStr;
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case InternalType.String:
                    switch (b._type)
                    {
                        case InternalType.Array:
                            foreach (var item in b._array)
                            {
                                result.Remove(item);
                            }
                            break;
                        case InternalType.Boolean:
                            if (bool.TryParse(a._string, out var bValue))
                            {
                                result._type = InternalType.Boolean;
                                result._boolean = bValue && !b._boolean;
                            }
                            else if (double.TryParse(a._string, out var nValue))
                            {
                                result._type = InternalType.Number;
                                result._number = nValue - (b._boolean ? 1 : 0);
                            }
                            else
                            {
                                result._string = a._string.Replace(b._boolean.ToString(), string.Empty);
                            }
                            break;
                        case InternalType.Number:
                            if (double.TryParse(a._string, out var nValue2))
                            {
                                result._type = InternalType.Number;
                                result._number = nValue2 - b._number;
                            }
                            else if (bool.TryParse(a._string, out var bValue2))
                            {
                                result._type = InternalType.Number;
                                result._number = (bValue2 ? 1 : 0) - b._number;
                            }
                            else
                            {
                                result._string = a._string.Replace(b._number.ToString(), string.Empty);
                            }
                            break;
                        case InternalType.String:
                            if (double.TryParse(a._string, out var value1))
                            {
                                if (double.TryParse(b._string, out var value2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = value1 - value2;
                                }
                                else if (bool.TryParse(b._string, out var bValue3))
                                {
                                    result._type = InternalType.Number;
                                    result._number = value1 - (bValue3 ? 1 : 0);
                                }
                                else
                                {
                                    result._string = a._string.Replace(b._string, string.Empty);
                                }
                            }
                            else if (bool.TryParse(a._string, out var bValue3))
                            {
                                if (bool.TryParse(b._string, out var bValue4))
                                {
                                    result._type = InternalType.Boolean;
                                    result._boolean = bValue3 && !bValue4;
                                }
                                else if (double.TryParse(b._string, out var value))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (bValue3 ? 1 : 0) - value;
                                }
                                else
                                {
                                    result._string = a._string.Replace(b._string, string.Empty);
                                }
                            }
                            else
                            {
                                result._string = a._string.Replace(b._string, string.Empty);
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    result.CopyValue(-b);
                    break;
            }
            return result;
        }

        public static let operator *(let a, let b)
        {
            if (a == null)
            {
                return new let();
            }
            var result = a.ShallowCopy();
            if (b == null)
            {
                result._type = InternalType.None;
                result._children.Clear();
                return result;
            }
            if (a._type == InternalType.Array)
            {
                if (b._type == InternalType.Array)
                {
                    result._array = a._array.Union(b._array).ToList();
                }
                else
                {
                    for (int i = 0; i < a._array.Count; i++)
                    {
                        result._array[i] = a._array[i] * b;
                    }
                }
            }
            else if (b._type == InternalType.Array)
            {
                result._type = InternalType.Array;
                result._array.AddRange(b._array);
                for (int i = 0; i < b._array.Count; i++)
                {
                    result._array[i] = a * b._array[i];
                }
            }
            else
            {
                switch (a._type)
                {
                    case InternalType.Boolean:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._boolean = a._boolean == b._boolean;
                                break;
                            case InternalType.Number:
                                result._type = InternalType.Number;
                                result._number = (a._boolean ? 1 : 0) * b._number;
                                break;
                            case InternalType.String:
                                if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._boolean = a._boolean == bValue;
                                }
                                else if (double.TryParse(b._string, out var value))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (a._boolean ? 1 : 0) * value;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = a._boolean ? b._string : string.Empty;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.Number:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._number *= b._boolean ? 1 : 0;
                                break;
                            case InternalType.Number:
                                result._number *= b._number;
                                break;
                            case InternalType.String:
                                if (double.TryParse(b._string, out var value))
                                {
                                    result._number *= value;
                                }
                                else if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._number *= bValue ? 1 : 0;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    var sb = new StringBuilder();
                                    for (int i = 0; i < a._number; i++)
                                    {
                                        sb.Append(b._string);
                                    }
                                    result._string = sb.ToString();
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.String:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                if (bool.TryParse(a._string, out var bValue))
                                {
                                    result._type = InternalType.Boolean;
                                    result._boolean = bValue == b._boolean;
                                }
                                else if (double.TryParse(a._string, out var nValue))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue * (b._boolean ? 1 : 0);
                                }
                                else
                                {
                                    result._string = b._boolean ? a._string : string.Empty;
                                }
                                break;
                            case InternalType.Number:
                                if (double.TryParse(a._string, out var nValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue2 * b._number;
                                }
                                else if (bool.TryParse(a._string, out var bValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (bValue2 ? 1 : 0) * b._number;
                                }
                                else
                                {
                                    var sb = new StringBuilder();
                                    for (int i = 0; i < b._number; i++)
                                    {
                                        sb.Append(a._string);
                                    }
                                    result._string = sb.ToString();
                                }
                                break;
                            case InternalType.String:
                                if (double.TryParse(a._string, out var value1))
                                {
                                    if (double.TryParse(b._string, out var value2))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 * value2;
                                    }
                                    else if (bool.TryParse(b._string, out var bValue3))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 * (bValue3 ? 1 : 0);
                                    }
                                    else
                                    {
                                        result._string = $"{a._string}*{b._string}";
                                    }
                                }
                                else if (bool.TryParse(a._string, out var bValue3))
                                {
                                    if (bool.TryParse(b._string, out var bValue4))
                                    {
                                        result._type = InternalType.Boolean;
                                        result._boolean = bValue3 == bValue4;
                                    }
                                    else if (double.TryParse(b._string, out var value))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = (bValue3 ? 1 : 0) * value;
                                    }
                                    else
                                    {
                                        result._string = $"{a._string}*{b._string}";
                                    }
                                }
                                else
                                {
                                    result._string = $"{a._string}*{b._string}";
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public static let operator /(let a, let b)
        {
            if (a == null)
            {
                return new let();
            }
            var result = a.ShallowCopy();
            if (b == null)
            {
                result._type = InternalType.None;
                result._children.Clear();
                return result;
            }
            if (a._type == InternalType.Array)
            {
                if (b._type == InternalType.Array)
                {
                    result._array = a._array.Except(b._array).ToList();
                }
                else
                {
                    for (int i = 0; i < a._array.Count; i++)
                    {
                        result._array[i] = a._array[i] / b;
                    }
                }
            }
            else if (b._type == InternalType.Array)
            {
                result._type = InternalType.Array;
                result._array.AddRange(b._array);
                for (int i = 0; i < b._array.Count; i++)
                {
                    result._array[i] = a / b._array[i];
                }
            }
            else
            {
                switch (a._type)
                {
                    case InternalType.Boolean:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._boolean = a._boolean == b._boolean;
                                break;
                            case InternalType.Number:
                                result._type = InternalType.Number;
                                result._number = (a._boolean ? 1 : 0) / b._number;
                                break;
                            case InternalType.String:
                                if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._boolean = result._boolean == bValue;
                                }
                                else if (double.TryParse(b._string, out var value))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (a._boolean ? 1 : 0) / value;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = a._boolean ? b._string : string.Empty;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.Number:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._number /= b._boolean ? 1 : 0;
                                break;
                            case InternalType.Number:
                                result._number /= b._number;
                                break;
                            case InternalType.String:
                                if (double.TryParse(b._string, out var value))
                                {
                                    result._number /= value;
                                }
                                else if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._number /= bValue ? 1 : 0;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = $"{a._number}/{b._string}";
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.String:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                if (bool.TryParse(a._string, out var bValue))
                                {
                                    result._type = InternalType.Boolean;
                                    result._boolean = bValue == b._boolean;
                                }
                                else if (double.TryParse(a._string, out var nValue))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue / (b._boolean ? 1 : 0);
                                }
                                else
                                {
                                    result._string = b._boolean ? a._string : string.Empty;
                                }
                                break;
                            case InternalType.Number:
                                if (double.TryParse(a._string, out var nValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue2 / b._number;
                                }
                                else if (bool.TryParse(a._string, out var bValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (bValue2 ? 1 : 0) / b._number;
                                }
                                else
                                {
                                    if (double.IsNaN(b._number) || double.IsInfinity(b._number) || b._number == 0)
                                    {
                                        result._string = string.Empty;
                                    }
                                    else
                                    {
                                        result._string = result.Substring(0,
                                            (int)Math.Max(0, Math.Round(a._string.Length / b._number)));
                                    }
                                }
                                break;
                            case InternalType.String:
                                if (double.TryParse(a._string, out var value1))
                                {
                                    if (double.TryParse(b._string, out var value2))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 / value2;
                                    }
                                    else if (bool.TryParse(b._string, out var bValue3))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 / (bValue3 ? 1 : 0);
                                    }
                                    else
                                    {
                                        result._string = a._string.Replace(b._string, string.Empty);
                                    }
                                }
                                else if (bool.TryParse(a._string, out var bValue3))
                                {
                                    if (bool.TryParse(b._string, out var bValue4))
                                    {
                                        result._type = InternalType.Boolean;
                                        result._boolean = bValue3 == bValue4;
                                    }
                                    else if (double.TryParse(b._string, out var value))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = (bValue3 ? 1 : 0) / value;
                                    }
                                    else
                                    {
                                        result._string = a._string.Replace(b._string, string.Empty);
                                    }
                                }
                                else
                                {
                                    result._string = a._string.Replace(b._string, string.Empty);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public static let operator %(let a, let b)
        {
            if (a == null)
            {
                return b?.ShallowCopy() ?? new let();
            }
            var result = a.ShallowCopy();
            if (b == null)
            {
                return result;
            }
            if (a._type == InternalType.Array)
            {
                if (b._type == InternalType.Array)
                {
                    result._array = a._array.Except(b._array)
                        .Concat(b._array.Except(a._array)).ToList();
                }
                else
                {
                    for (int i = 0; i < a._array.Count; i++)
                    {
                        result._array[i] = a._array[i] % b;
                    }
                }
            }
            else if (b._type == InternalType.Array)
            {
                result._type = InternalType.Array;
                result._array.AddRange(b._array);
                for (int i = 0; i < b._array.Count; i++)
                {
                    result._array[i] = a % b._array[i];
                }
            }
            else
            {
                switch (a._type)
                {
                    case InternalType.Boolean:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._boolean ^= b._boolean;
                                break;
                            case InternalType.Number:
                                result._type = InternalType.Number;
                                result._number = (a._boolean ? 1 : 0) % b._number;
                                break;
                            case InternalType.String:
                                if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._boolean ^= bValue;
                                }
                                else if (double.TryParse(b._string, out var value))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (a._boolean ? 1 : 0) % value;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = a._boolean ? b._string : string.Empty;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.Number:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                result._number %= b._boolean ? 1 : 0;
                                break;
                            case InternalType.Number:
                                result._number %= b._number;
                                break;
                            case InternalType.String:
                                if (double.TryParse(b._string, out var value))
                                {
                                    result._number %= value;
                                }
                                else if (bool.TryParse(b._string, out var bValue))
                                {
                                    result._number %= bValue ? 1 : 0;
                                }
                                else
                                {
                                    result._type = InternalType.String;
                                    result._string = $"{a._number}%{b._string}";
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case InternalType.String:
                        switch (b._type)
                        {
                            case InternalType.Boolean:
                                if (bool.TryParse(a._string, out var bValue))
                                {
                                    result._type = InternalType.Boolean;
                                    result._boolean = bValue ^ b._boolean;
                                }
                                else if (double.TryParse(a._string, out var nValue))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue % (b._boolean ? 1 : 0);
                                }
                                else
                                {
                                    result._string = b._boolean ? a._string : string.Empty;
                                }
                                break;
                            case InternalType.Number:
                                if (double.TryParse(a._string, out var nValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = nValue2 % b._number;
                                }
                                else if (bool.TryParse(a._string, out var bValue2))
                                {
                                    result._type = InternalType.Number;
                                    result._number = (bValue2 ? 1 : 0) % b._number;
                                }
                                else if (!double.IsNaN(b._number) && !double.IsInfinity(b._number) && b._number != 0)
                                {
                                    result._string = a.Substring((int)Math.Max(0, Math.Floor(a._string.Length / b._number) * b._number));
                                }
                                break;
                            case InternalType.String:
                                if (double.TryParse(a._string, out var value1))
                                {
                                    if (double.TryParse(b._string, out var value2))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 % value2;
                                    }
                                    else if (bool.TryParse(b._string, out var bValue3))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = value1 % (bValue3 ? 1 : 0);
                                    }
                                    else
                                    {
                                        result._string = a._string.Replace(b._string, string.Empty);
                                    }
                                }
                                else if (bool.TryParse(a._string, out var bValue3))
                                {
                                    if (bool.TryParse(b._string, out var bValue4))
                                    {
                                        result._type = InternalType.Boolean;
                                        result._boolean = bValue3 ^ bValue4;
                                    }
                                    else if (double.TryParse(b._string, out var value))
                                    {
                                        result._type = InternalType.Number;
                                        result._number = (bValue3 ? 1 : 0) % value;
                                    }
                                    else
                                    {
                                        result._string = a._string.Replace(b._string, string.Empty);
                                    }
                                }
                                else
                                {
                                    result._string = a._string.Replace(b._string, string.Empty);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        result.CopyValue(b);
                        break;
                }
            }
            return result;
        }

        public static bool operator &(let a, let b) => (a?.ToBoolean() ?? false) && (b?.ToBoolean() ?? false);

        public static bool operator |(let a, let b) => (a?.ToBoolean() ?? false) || (b?.ToBoolean() ?? false);

        public static bool operator ^(let a, let b) => (a?.ToBoolean() ?? false) ^ (b?.ToBoolean() ?? false);

        public static let operator <<(let a, int b)
        {
            if (a == null)
            {
                return new let();
            }
            var result = a.ShallowCopy();
            if (b == 0)
            {
                return result;
            }
            switch (a._type)
            {
                case InternalType.Array:
                    if (a.Length > 0)
                    {
                        result._array.RemoveRange(0, result._array.Count - b);
                        for (int i = 0; i < b; i++)
                        {
                            result._array.Add(new let());
                        }
                    }
                    break;
                case InternalType.Boolean:
                    if (b % 2 != 0)
                    {
                        result._boolean = !a._boolean;
                    }
                    break;
                case InternalType.Number:
                    if (a._number.IsIntegral())
                    {
                        if (a._number <= int.MaxValue)
                        {
                            var v = Convert.ToInt32(a._number);
                            result._number = v << b;
                        }
                        else if (a._number <= long.MaxValue)
                        {
                            var v = Convert.ToInt64(a._number);
                            result._number = v << b;
                        }
                        else if (a._number > 0 && a._number <= ulong.MaxValue)
                        {
                            var v = Convert.ToUInt64(a._number);
                            result._number = v << b;
                        }
                    }
                    break;
                case InternalType.String:
                    if (double.TryParse(a._string, out var value))
                    {
                        if (value <= int.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToInt32(value);
                            result._number = v << b;
                        }
                        else if (value <= long.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToInt64(value);
                            result._number = v << b;
                        }
                        else if (value > 0 && a._number <= ulong.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToUInt64(value);
                            result._number = v << b;
                        }
                    }
                    else if (bool.TryParse(a._string, out var bValue))
                    {
                        result._type = InternalType.Boolean;
                        result._boolean = b % 2 == 0 ? bValue : !bValue;
                    }
                    if (result._type == InternalType.String)
                    {
                        var sb = new StringBuilder();
                        if (a._string.Length > b)
                        {
                            sb.Append(a._string.Substring(b));
                        }
                        for (int i = 0; i < b; i++)
                        {
                            sb.Append(" ");
                        }
                        result._string = sb.ToString();
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static let operator >>(let a, int b)
        {
            if (a == null)
            {
                return new let();
            }
            var result = a.ShallowCopy();
            if (b == 0)
            {
                return result;
            }
            switch (a._type)
            {
                case InternalType.Array:
                    if (a.Length > 0)
                    {
                        for (int i = 0; i < b; i++)
                        {
                            result._array.Insert(0, new let());
                        }
                        result.RemoveAll(result._array.Count - b);
                    }
                    break;
                case InternalType.Boolean:
                    if (b % 2 != 0)
                    {
                        result._boolean = !a._boolean;
                    }
                    break;
                case InternalType.Number:
                    if (a._number.IsIntegral())
                    {
                        if (a._number <= int.MaxValue)
                        {
                            var v = Convert.ToInt32(a._number);
                            result._number = v >> b;
                        }
                        else if (a._number <= long.MaxValue)
                        {
                            var v = Convert.ToInt64(a._number);
                            result._number = v >> b;
                        }
                        else if (a._number > 0 && a._number <= ulong.MaxValue)
                        {
                            var v = Convert.ToUInt64(a._number);
                            result._number = v >> b;
                        }
                    }
                    break;
                case InternalType.String:
                    if (double.TryParse(a._string, out var value))
                    {
                        if (value <= int.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToInt32(value);
                            result._number = v >> b;
                        }
                        else if (value <= long.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToInt64(value);
                            result._number = v >> b;
                        }
                        else if (value > 0 && a._number <= ulong.MaxValue)
                        {
                            result._type = InternalType.Number;
                            var v = Convert.ToUInt64(value);
                            result._number = v >> b;
                        }
                    }
                    else if (bool.TryParse(a._string, out var bValue))
                    {
                        result._type = InternalType.Boolean;
                        result._boolean = b % 2 == 0 ? bValue : !bValue;
                    }
                    if (result._type == InternalType.String)
                    {
                        var sb = new StringBuilder();
                        for (int i = 0; i < b; i++)
                        {
                            sb.Append(" ");
                        }
                        if (a._string.Length > b)
                        {
                            sb.Append(a._string.Substring(0, a._string.Length - b));
                        }
                        result._string = sb.ToString();
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static bool operator ==(let a, object b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null)
                    || (b.GetType() == typeof(let) && (b as let).Equals(null));
            }
            return a.Equals(b);
        }

        public static bool operator !=(let a, object b)
        {
            if (ReferenceEquals(a, null))
            {
                return !ReferenceEquals(b, null)
                    && (b.GetType() != typeof(let) || !(b as let).Equals(null));
            }
            return !a.Equals(b);
        }

        public static bool operator <(let a, let b)
        {
            if (b == null)
            {
                return false;
            }
            if (a == null)
            {
                return true;
            }
            return a.CompareTo(b) < 0;
        }

        public static bool operator >(let a, let b)
        {
            if (b == null)
            {
                return true;
            }
            if (a == null)
            {
                return false;
            }
            return a.CompareTo(b) > 0;
        }

        public static bool operator <=(let a, let b)
        {
            if (b == null)
            {
                return a == null;
            }
            if (a == null)
            {
                return true;
            }
            return a.CompareTo(b) <= 0;
        }

        public static bool operator >=(let a, let b)
        {
            if (b == null)
            {
                return true;
            }
            if (a == null)
            {
                return false;
            }
            return a.CompareTo(b) >= 0;
        }

        #region Conversions

        public static implicit operator bool(let v) => v.ToBoolean();

        public static implicit operator bool[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => i.ToBoolean()).ToArray();

        public static implicit operator byte(let v)
        {
            try
            {
                return Convert.ToByte(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return byte.MaxValue;
                }
                else
                {
                    return byte.MinValue;
                }
            }
        }

        public static implicit operator byte[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (byte)i).ToArray();

        public static implicit operator decimal(let v)
        {
            try
            {
                return Convert.ToDecimal(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return decimal.MaxValue;
                }
                else
                {
                    return decimal.MinValue;
                }
            }
        }

        public static implicit operator decimal[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (decimal)i).ToArray();

        public static implicit operator Dictionary<string, object>(let v) => v.ToDictionary();

        public static implicit operator double(let v) => v.ToDouble();

        public static implicit operator double[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => i.ToDouble()).ToArray();

        public static implicit operator float(let v) => (float)v.ToDouble();

        public static implicit operator float[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (float)i.ToDouble()).ToArray();

        public static implicit operator int(let v)
        {
            try
            {
                return Convert.ToInt32(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return int.MaxValue;
                }
                else
                {
                    return int.MinValue;
                }
            }
        }

        public static implicit operator int[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (int)i).ToArray();

        public static implicit operator let(bool v) => new let(v);

        public static implicit operator let(bool[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(byte v) => new let(v);

        public static implicit operator let(byte[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(Dictionary<string, object> v)
        {
            let newV;
            if (v.TryGetValue("Value", out var value))
            {
                newV = new let(value);
            }
            else
            {
                newV = new let();
            }
            foreach (var item in v.Where(i => i.Key != "Value"))
            {
                newV._children.Add(item.Key, new let(item.Value));
            }
            return newV;
        }

        public static implicit operator let(decimal v) => new let(v);

        public static implicit operator let(decimal[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(double v) => new let(v);

        public static implicit operator let(double[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(float v) => new let(v);

        public static implicit operator let(float[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(int v) => new let(v);

        public static implicit operator let(int[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(long v) => new let(v);

        public static implicit operator let(long[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(object[] v) => new let(v);

        public static implicit operator let(object[][] v) => new let(v);

        public static implicit operator let(object[,] v) => new let(v);

        public static implicit operator let(sbyte v) => new let(v);

        public static implicit operator let(sbyte[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(short v) => new let(v);

        public static implicit operator let(short[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(string v) => new let(v);

        public static implicit operator let(string[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(uint v) => new let(v);

        public static implicit operator let(uint[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(ulong v) => new let(v);

        public static implicit operator let(ulong[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator let(ushort v) => new let(v);

        public static implicit operator let(ushort[] v)
        {
            var arr = new let()
            {
                _type = InternalType.Array
            };
            arr._array.AddRange(v.Select(i => new let(i)));
            return arr;
        }

        public static implicit operator long(let v)
        {
            try
            {
                return Convert.ToInt64(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return long.MaxValue;
                }
                else
                {
                    return long.MinValue;
                }
            }
        }

        public static implicit operator long[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (long)i).ToArray();

        public static implicit operator object[] (let v) => v.ToArray();

        public static implicit operator object[][] (let v)
        {
            var obj = new List<object[]>
            {
                new object[] { "Value", v.ToObject() }
            };
            foreach (var item in v._children)
            {
                obj.Add(new object[] { item.Key, item.Value.ToObject() });
            }
            return obj.ToArray();
        }

        public static implicit operator object[,] (let v)
        {
            var obj = new List<object[]>
            {
                new object[] { "Value", v.ToObject() }
            };
            foreach (var item in v._children)
            {
                obj.Add(new object[] { item.Key, item.Value.ToObject() });
            }
            var jagged = obj.ToArray();
            object[,] result = new object[jagged.Length, 2];
            for (int i = 0; i < jagged.Length; i++)
            {
                result[i, 0] = jagged[i][0];
                result[i, 1] = jagged[i][1];
            }
            return result;
        }

        public static implicit operator sbyte(let v)
        {
            try
            {
                return Convert.ToSByte(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return sbyte.MaxValue;
                }
                else
                {
                    return sbyte.MinValue;
                }
            }
        }

        public static implicit operator sbyte[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (sbyte)i).ToArray();

        public static implicit operator short(let v)
        {
            try
            {
                return Convert.ToInt16(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return short.MaxValue;
                }
                else
                {
                    return short.MinValue;
                }
            }
        }

        public static implicit operator short[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (short)i).ToArray();

        public static implicit operator string(let v) => v.ToString();

        public static implicit operator string[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => i.ToString()).ToArray();

        public static implicit operator uint(let v)
        {
            try
            {
                return Convert.ToUInt32(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return uint.MaxValue;
                }
                else
                {
                    return uint.MinValue;
                }
            }
        }

        public static implicit operator uint[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (uint)i).ToArray();

        public static implicit operator ulong(let v)
        {
            try
            {
                return Convert.ToUInt64(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return ulong.MaxValue;
                }
                else
                {
                    return ulong.MinValue;
                }
            }
        }

        public static implicit operator ulong[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (ulong)i).ToArray();

        public static implicit operator ushort(let v)
        {
            try
            {
                return Convert.ToUInt16(v.ToDouble());
            }
            catch (OverflowException)
            {
                if (v._number > 0)
                {
                    return ushort.MaxValue;
                }
                else
                {
                    return ushort.MinValue;
                }
            }
        }

        public static implicit operator ushort[] (let v) => v.DeepClone().ConvertToArray()._array.Select(i => (ushort)i).ToArray();

        #endregion Conversions

        #endregion Operators

        /// <summary>
        /// Adds an item to the end of an array; the variable becomes an array if it wasn't already,
        /// with the first element equal to its former value.
        /// </summary>
        /// <param name="item">An item to add.</param>
        public void Add(let item)
        {
            if (_type != InternalType.Array)
            {
                _array.Clear();
                switch (_type)
                {
                    case InternalType.Boolean:
                        _array.Add(new let(_boolean));
                        break;
                    case InternalType.Number:
                        _array.Add(new let(_number));
                        break;
                    case InternalType.String:
                        _array.Add(new let(_string));
                        break;
                    default:
                        break;
                }
                _type = InternalType.Array;
            }
            _array.Add(item ?? new let());
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of an array; the variable becomes an array if it wasn't already, with the first element equal to its former value.
        /// </summary>
        public void AddRange(IEnumerable collection)
        {
            if (_type != InternalType.Array)
            {
                ConvertToArray();
            }
            foreach (var item in collection)
            {
                _array.Add(item.GetType() == typeof(let) ? item as let : new let(item));
            }
        }

        /// <summary>
        /// Removes all elements from an array; sets a string to ""; sets a boolean to false; sets a number to NaN.
        /// </summary>
        public void Clear()
        {
            _array.Clear();
            _boolean = false;
            _number = double.NaN;
            _string = string.Empty;
        }

        /// <summary>
        /// Compares this instance to a specified object and returns an integer that indicates
        /// whether this instance is less than, equal to, or greater than the specified object.
        /// </summary>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            let other;
            if (obj.GetType() == typeof(let))
            {
                other = obj as let;
            }
            else
            {
                other = new let(obj);
            }
            if (_type == InternalType.None)
            {
                return other._type == InternalType.None ? 0 : -1;
            }
            if (other._type == InternalType.None)
            {
                return 1;
            }
            return ToDouble().CompareTo(other.ToDouble());
        }

        /// <summary>
        /// Determines whether an item is in an array, or a string is a substring of a string; always
        /// returns false for numbers or booleans.
        /// </summary>
        /// <param name="item">An item to find.</param>
        /// <returns>true if the item is present; false otherwise.</returns>
        public bool Contains(let item)
        {
            if (item == null)
            {
                return false;
            }
            if (_type == InternalType.Array)
            {
                return _array.Contains(item);
            }
            var str = ToString();
            switch (item._type)
            {
                case InternalType.Array:
                    return item._array.Any(i => str.Contains(i.ToString()));
                case InternalType.Boolean:
                    return str.ToLower().Contains(item.ToString().ToLower());
                case InternalType.Number:
                case InternalType.String:
                    return str.Contains(item.ToString());
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines whether the object contains a property with the given name.
        /// </summary>
        public bool ContainsProperty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return _children.ContainsKey(name);
        }

        private let ConvertToArray()
        {
            if (_type == InternalType.Array)
            {
                return this;
            }
            _array.Clear();
            switch (_type)
            {
                case InternalType.Boolean:
                    _array.Add(new let(_boolean));
                    break;
                case InternalType.Number:
                    _array.Add(new let(_number));
                    break;
                case InternalType.String:
                    _array.Add(new let(_string));
                    break;
                default:
                    break;
            }
            _type = InternalType.Array;
            return this;
        }

        private void Copy(let other)
        {
            CopyValue(other);
            _array.Clear();
            if (other != null)
            {
                foreach (var item in other._array)
                {
                    _array.Add(item.DeepClone());
                }
                foreach (var item in other._children)
                {
                    _children.Add(item.Key, item.Value.DeepClone());
                }
            }
        }

        /// <summary>
        /// Copies the elements of an array to the destination array, starting at the specified
        /// index; fills the destination array with other types.
        /// </summary>
        /// <param name="array">A destination array (the variable will become an array if it isn't one already).</param>
        /// <param name="index">The starting index of the destination array.</param>
        public void CopyTo(let array, int index = 0)
        {
            if (array == null)
            {
                return;
            }
            var targetLength = _type == InternalType.Array
                ? _array.Count + index
                : (array._type == InternalType.Array ? array.Length - index : index + 1);
            if (targetLength < 0)
            {
                return;
            }
            if (index == 0 && _type != InternalType.Array && array._type != InternalType.Array)
            {
                array.Copy(this);
                return;
            }
            array.ConvertToArray();
            for (int i = Math.Max(0, index); i < targetLength; i++)
            {
                if (_type == InternalType.Array)
                {
                    array[i] = this[i - index].DeepClone();
                }
                else
                {
                    array[i] = DeepClone();
                }
            }
        }

        private let CopyValue(let other)
        {
            if (other == null)
            {
                _type = InternalType.None;
                _boolean = false;
                _number = double.NaN;
                _string = null;
                _array.Clear();
            }
            else
            {
                _type = other._type;
                _boolean = other._boolean;
                _number = other._number;
                _string = other._string;
                _array.AddRange(other._array);
            }
            return this;
        }

        /// <summary>
        /// Retrieves a deep copy of the current object.
        /// </summary>
        public let DeepClone()
        {
            let newItem = new let()
            {
                _type = _type,
                _boolean = _boolean,
                _number = _number,
                _string = _string
            };
            foreach (var item in _array)
            {
                newItem._array.Add(item.DeepClone());
            }
            foreach (var item in _children)
            {
                newItem._children.Add(item.Key, item.Value.DeepClone());
            }
            return newItem;
        }

        /// <summary>
        /// Determines whether this variable and another are equal.
        /// </summary>
        public bool Equals(let other)
        {
            if (other == null)
            {
                return _type == InternalType.None && _children.Count == 0;
            }
            if (_type != other._type
                || !_children.SequenceEqual(other._children))
            {
                return false;
            }
            switch (_type)
            {
                case InternalType.Array:
                    return _array.SequenceEqual(other._array);
                case InternalType.Boolean:
                    return _boolean == other._boolean;
                case InternalType.Number:
                    if (double.IsNaN(other._number))
                    {
                        return double.IsNaN(_number);
                    }
                    else
                    {
                        return _number == other._number;
                    }
                case InternalType.String:
                    return _string.Equals(other._string, StringComparison.Ordinal);
                default:
                    return true;
            }
        }

        /// <summary>
        /// Determines whether this variable and an object are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return _type == InternalType.None && _children.Count == 0;
            }
            var isLet = obj.GetType() == typeof(let);
            var other = isLet ? obj as let : ShallowCopy().SetValue(obj);
            return Equals(other);
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the first occurrence; for non-arrays, returns the value if it
        /// matches the predicate, or null if not.
        /// </summary>
        public let Find(Predicate<let> match)
        {
            if (match == null)
            {
                return null;
            }
            var index = FindIndex(match);
            if (_type == InternalType.Array)
            {
                return index == -1 ? null : _array[index];
            }
            else
            {
                return index == -1 ? null : this;
            }
        }

        /// <summary>
        /// Retrieves all the elements of an array that match the conditions defined by the specified
        /// predicate; for non-arrays, returns a list containing the item if it matches, or an empty
        /// list otherwise.
        /// </summary>
        public let FindAll(Predicate<let> match)
        {
            if (match == null)
            {
                return new let
                {
                    _type = InternalType.Array
                };
            }
            if (_type == InternalType.Array)
            {
                return new let(_array.FindAll(match));
            }
            else
            {
                var list = new List<let>();
                if (match.Invoke(this))
                {
                    list.Add(this);
                }
                return new let(list);
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the first occurrence within an array; for
        /// non-arrays, returns 0 if the value fulfills the predicate, and -1 otherwise.
        /// </summary>
        public int FindIndex(Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.FindIndex(match);
            }
            else
            {
                return match.Invoke(this) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the first occurrence within an array
        /// starting at the specified index; for non-arrays, returns 0 if the value fulfills the
        /// predicate and <paramref name="startIndex"/> is 0, and -1 otherwise.
        /// </summary>
        public int FindIndex(int startIndex, Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            if (_type == InternalType.Array)
            {
                return startIndex >= _array.Count ? -1 : _array.FindIndex(startIndex, match);
            }
            else
            {
                return startIndex > 0 ? -1 : (match.Invoke(this) ? 0 : -1);
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the first occurrence within an array
        /// starting at the specified index and containing the specified number of elements; for
        /// non-arrays, returns 0 if the value fulfills the predicate and <paramref
        /// name="startIndex"/> is 0, and -1 otherwise.
        /// </summary>
        public int FindIndex(int startIndex, int count, Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (startIndex < 0)
            {
                count += startIndex; // since it's negative, this reduces count
                startIndex = 0;
            }
            if (count <= 0)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                var targetCount = Math.Min(count, _array.Count - startIndex);
                return startIndex >= _array.Count ? -1 : _array.FindIndex(startIndex, targetCount, match);
            }
            else
            {
                return startIndex > 0 ? -1 : (match.Invoke(this) ? 0 : -1);
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the last occurrence; for non-arrays, returns the value if it
        /// matches the predicate, or null if not.
        /// </summary>
        public let FindLast(Predicate<let> match)
        {
            _array.Find(match);
            var index = FindLastIndex(match);
            if (_type == InternalType.Array)
            {
                return index == -1 ? null : _array[index];
            }
            else
            {
                return index == -1 ? null : this;
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the last occurrence within an array; for
        /// non-arrays, returns 0 if the value fulfills the predicate, and -1 otherwise.
        /// </summary>
        public int FindLastIndex(Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.FindLastIndex(match);
            }
            else
            {
                return match.Invoke(this) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the last occurrence within an array
        /// up to the specified index; for non-arrays, returns 0 if the value fulfills the
        /// predicate and <paramref name="startIndex"/> is 0, and -1 otherwise.
        /// </summary>
        public int FindLastIndex(int startIndex, Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            if (_type == InternalType.Array)
            {
                return startIndex >= _array.Count ? -1 : _array.FindLastIndex(startIndex, match);
            }
            else
            {
                return startIndex > 0 ? -1 : (match.Invoke(this) ? 0 : -1);
            }
        }

        /// <summary>
        /// Searches an array for an element that matches the conditions defined by the specified
        /// predicate, and returns the zero-based index of the last occurrence within an array
        /// up to the specified index and containing the specified number of elements; for
        /// non-arrays, returns 0 if the value fulfills the predicate and <paramref
        /// name="startIndex"/> is 0, and -1 otherwise.
        /// </summary>
        public int FindLastIndex(int startIndex, int count, Predicate<let> match)
        {
            if (match == null)
            {
                return -1;
            }
            if (startIndex < 0)
            {
                count += startIndex; // since it's negative, this reduces count
                startIndex = 0;
            }
            if (count <= 0)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                var targetCount = Math.Min(count, _array.Count - startIndex);
                return startIndex >= _array.Count ? -1 : _array.FindLastIndex(startIndex, targetCount, match);
            }
            else
            {
                return startIndex > 0 ? -1 : (match.Invoke(this) ? 0 : -1);
            }
        }

        /// <summary>
        /// Performs the specified action on each element of an array; for non-arrays, performs it on
        /// the value.
        /// </summary>
        public void ForEach(Action<let> action)
        {
            if (action == null)
            {
                return;
            }
            if (_type == InternalType.Array)
            {
                _array.ForEach(action);
            }
            else
            {
                action.Invoke(this);
            }
        }

        /// <summary>
        /// A hash function. WARNING: hash code will change when properties are changed, making this
        /// class unsafe for use in hash tables or as dictionary keys. However, this allows LINQ
        /// functions and other native code to detect equality correctly without the use of a
        /// separate <see cref="IEqualityComparer{T}"/> instance.
        /// </summary>
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + _type.GetHashCode();
            hash = hash * 23 + _boolean.GetHashCode();
            hash = hash * 23 + _number.GetHashCode();
            hash = hash * 23 + (_string?.GetHashCode() ?? 0);
            foreach (var item in _array)
            {
                hash = hash * 23 + item.GetHashCode();
            }
            foreach (var item in _children)
            {
                hash = hash * 23 + item.Key.GetHashCode();
                hash = hash * 23 + item.Value.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in a source array; or gets a substring; for
        /// other variable types, retrieves the item as an array with <paramref name="count"/> elements.
        /// </summary>
        /// <param name="index">The zero-based index at which the range starts.</param>
        /// <param name="count">The number of elements in the range.</param>
        public let GetRange(int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this reduces count
                index = 0;
            }
            if (count <= 0)
            {
                return new let();
            }
            switch (_type)
            {
                case InternalType.Array:
                    if (index >= _array.Count)
                    {
                        return new let
                        {
                            _type = InternalType.Array
                        };
                    }
                    else
                    {
                        var targetCount = index + count > _array.Count ? _array.Count - index : count;
                        return new let(_array.GetRange(index, targetCount));
                    }
                case InternalType.String:
                    if (index >= _string.Length)
                    {
                        return new let(string.Empty);
                    }
                    else if (index + count > _string.Length)
                    {
                        return new let(_string.Substring(index));
                    }
                    else
                    {
                        return new let(_string.Substring(index, count));
                    }
                default:
                    var newArr = new let
                    {
                        _type = InternalType.Array
                    };
                    newArr.Add(this);
                    return newArr;
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the first
        /// occurrence; for non-arrays, returns 0 if they are equal and -1 otherwise.
        /// </summary>
        public int IndexOf(let item)
        {
            if (item == null)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.IndexOf(item);
            }
            else if (_type == InternalType.String)
            {
                return _string.IndexOf(item.ToString());
            }
            else
            {
                return Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the first
        /// occurrence starting at <paramref name="index"/>; for non-arrays, returns 0 if they are equal and <paramref name="index"/> is 0, and -1 otherwise.
        /// </summary>
        public int IndexOf(let item, int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            if (item == null)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.IndexOf(item, index);
            }
            else if (_type == InternalType.String)
            {
                return _string.IndexOf(item.ToString(), index);
            }
            else
            {
                return index == 0 && Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the first
        /// occurrence starting at <paramref name="index"/> with the specified number of elements;
        /// for non-arrays, returns 0 if they are equal and <paramref name="index"/> is 0, and -1 otherwise.
        /// </summary>
        public int IndexOf(let item, int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this reduces count
                index = 0;
            }
            if (item == null || count <= 0)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.IndexOf(item, index, count);
            }
            else if (_type == InternalType.String)
            {
                return _string.IndexOf(item.ToString(), index, count);
            }
            else
            {
                return index == 0 && Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Inserts an element into an array at the specified index; the variable becomes an array if
        /// it wasn't already, with the first element equal to its former value.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public void Insert(int index, let item)
        {
            if (index < 0)
            {
                return;
            }
            if (_type != InternalType.Array)
            {
                ConvertToArray();
            }
            PadRight(index);
            _array.Insert(index, item ?? new let());
        }

        /// <summary>
        /// Inserts the elements of a collection into an array at the specified index; the variable
        /// becomes an array if it wasn't already, with the first element equal to its former value.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public void InsertRange(int index, let collection)
        {
            if (index < 0)
            {
                return;
            }
            if (_type != InternalType.Array)
            {
                ConvertToArray();
            }
            PadRight(index);
            if (collection == null)
            {
                _array.Insert(index, new let());
            }
            else if (collection._type == InternalType.Array)
            {
                _array.InsertRange(index, collection._array);
            }
            else
            {
                _array.Insert(index, collection);
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the last
        /// occurrence; for non-arrays, returns 0 if they are equal and -1 otherwise.
        /// </summary>
        public int LastIndexOf(let item)
        {
            if (_type == InternalType.Array)
            {
                return _array.LastIndexOf(item);
            }
            else
            {
                return Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the last
        /// occurrence starting at <paramref name="index"/>; for non-arrays, returns 0 if they are equal and <paramref name="index"/> is 0, and -1 otherwise.
        /// </summary>
        public int LastIndexOf(let item, int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            if (_type == InternalType.Array)
            {
                return _array.LastIndexOf(item, index);
            }
            else
            {
                return index == 0 && Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Searches an array for the specified object and returns the zero-based index of the last
        /// occurrence starting at <paramref name="index"/> with the specified number of elements;
        /// for non-arrays, returns 0 if they are equal and <paramref name="index"/> is 0, and -1 otherwise.
        /// </summary>
        public int LastIndexOf(let item, int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this reduces count
                index = 0;
            }
            if (count <= 0)
            {
                return -1;
            }
            if (_type == InternalType.Array)
            {
                return _array.LastIndexOf(item, index, count);
            }
            else
            {
                return index == 0 && Equals(item) ? 0 : -1;
            }
        }

        /// <summary>
        /// Adds additional items to the beginning of an array, and spaces to the beginning of a
        /// string, to match the given length. Has no effect on other variable types.
        /// </summary>
        public void PadLeft(int targetLength)
        {
            switch (_type)
            {
                case InternalType.Array:
                    if (_array.Count < targetLength)
                    {
                        for (int i = _array.Count; i < targetLength; i++)
                        {
                            _array.Insert(0, new let());
                        }
                    }
                    break;
                case InternalType.String:
                    _string = _string.PadLeft(targetLength);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Adds additional items to the end of an array, and spaces to the end of a string, to match
        /// the given length. Has no effect on other variable types.
        /// </summary>
        public void PadRight(int targetLength)
        {
            switch (_type)
            {
                case InternalType.Array:
                    if (_array.Count < targetLength)
                    {
                        for (int i = _array.Count; i < targetLength; i++)
                        {
                            _array.Add(new let());
                        }
                    }
                    break;
                case InternalType.String:
                    _string = _string.PadRight(targetLength);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Removes the first occurrence of the specified item from an array or all occurrences from
        /// a string; has no effect on other variables.
        /// </summary>
        /// <param name="item">The object to remove. Can be null.</param>
        public void Remove(let item)
        {
            switch (_type)
            {
                case InternalType.Array:
                    _array.Remove(item);
                    break;
                case InternalType.String:
                    if (item != null)
                    {
                        _string = _string.Replace(item.ToString(), string.Empty);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Removes the specified number of items from an array or characters from a string starting
        /// at the specified index; has no effect on other variables.
        /// </summary>
        /// <param name="index">The zero-based index at which to begin deleting items.</param>
        /// <param name="count">The number of items to remove.</param>
        public void Remove(int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this decreases count
                index = 0;
            }
            if (count <= 0)
            {
                return;
            }
            switch (_type)
            {
                case InternalType.Array:
                    _array.RemoveRange(index, count);
                    break;
                case InternalType.String:
                    _string = _string.Remove(index, count);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Removes all items from an array or characters from a string starting at the specified index; has no effect on other variables.
        /// </summary>
        /// <param name="index">The zero-based index at which to begin deleting items.</param>
        public void RemoveAll(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            switch (_type)
            {
                case InternalType.Array:
                    var count = _array.Count - index;
                    _array.RemoveRange(index, count);
                    break;
                case InternalType.String:
                    _string = _string.Remove(index);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Removes all the elements from an array that match the conditions defined by the specified predicate; has no effect on non-arrays.
        /// </summary>
        public void RemoveAll(Predicate<let> match)
        {
            if (match == null)
            {
                return;
            }
            if (_type == InternalType.Array)
            {
                _array.RemoveAll(match);
            }
        }

        /// <summary>
        /// Removes the element at the specified index of an array, or the character at the specified
        /// index of a string; has no effect on other variable types.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        public void RemoveAt(int index)
        {
            if (index < 0)
            {
                return;
            }
            switch (_type)
            {
                case InternalType.Array:
                    _array.RemoveAt(index);
                    break;
                case InternalType.String:
                    _string = _string.Remove(index, 1);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Reverses the order of elements in an array, or characters in a string; has no effect on
        /// other variable types.
        /// </summary>
        public void Reverse()
        {
            switch (_type)
            {
                case InternalType.Array:
                    _array.Reverse();
                    break;
                case InternalType.String:
                    _string = new string(_string.Reverse().ToArray());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Reverses the order of elements in the specified range of an array, or characters in a
        /// string; has no effect on other variable types.
        /// </summary>
        /// <param name="index">The zero-based index at which to begin reversing items.</param>
        /// <param name="count">The number of items whose order is to be reversed.</param>
        public void Reverse(int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this decreases count
                index = 0;
            }
            if (count <= 0)
            {
                return;
            }
            switch (_type)
            {
                case InternalType.Array:
                    _array.Reverse(index, count);
                    break;
                case InternalType.String:
                    var sb = new StringBuilder(_string.Substring(0, index));
                    sb.Append(_string.Substring(index, count).Reverse().ToArray());
                    if (index + count < _string.Length)
                    {
                        sb.Append(_string.Substring(index + count));
                    }
                    _string = sb.ToString();
                    break;
                default:
                    break;
            }
        }

        private let SetValue(object value)
        {
            if (value == null)
            {
                _type = InternalType.None;
            }
            if (value.GetType() == typeof(bool))
            {
                _type = InternalType.Boolean;
                _boolean = (bool)value;
            }
            else if (value.GetType() == typeof(string))
            {
                _type = InternalType.String;
                _string = (string)value;
            }
            else if (value.GetType().IsNumericType())
            {
                _type = InternalType.Number;
                _number = Convert.ToDouble(value);
            }
            else if (value is IEnumerable)
            {
                _type = InternalType.Array;
                foreach (var child in (value as IEnumerable))
                {
                    _array.Add(new let(child));
                }
            }
            return this;
        }

        /// <summary>
        /// Retrieves a shallow copy of the current object.
        /// </summary>
        public let ShallowCopy()
        {
            let newItem = new let()
            {
                _type = _type,
                _boolean = _boolean,
                _number = _number,
                _string = _string
            };
            newItem._array.AddRange(_array);
            foreach (var item in _children)
            {
                newItem._children.Add(item.Key, item.Value);
            }
            return newItem;
        }

        /// <summary>
        /// Sorts the elements in an array or characters in a string using the default comparer; has
        /// no effect on other variable types.
        /// </summary>
        public void Sort()
        {
            switch (_type)
            {
                case InternalType.Array:
                    _array.Sort();
                    break;
                case InternalType.String:
                    _string = new string(_string.OrderBy(c => c).ToArray());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Sorts the elements in a range of an array or characters in a string using the default
        /// comparer; has no effect on other variable types.
        /// </summary>
        /// <param name="index">The zero-based index at which to begin sorting items.</param>
        /// <param name="count">The number of items to be sorted.</param>
        public void Sort(int index, int count)
        {
            if (index < 0)
            {
                count += index; // since it's negative, this decreases count
                index = 0;
            }
            if (count <= 0)
            {
                return;
            }
            switch (_type)
            {
                case InternalType.Array:
                    _array.Sort(index, count, null);
                    break;
                case InternalType.String:
                    var sb = new StringBuilder(_string.Substring(0, index));
                    sb.Append(_string.Substring(index, count).OrderBy(c => c).ToArray());
                    if (index + count < _string.Length)
                    {
                        sb.Append(_string.Substring(index + count));
                    }
                    _string = sb.ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Returns an array that contains substrings in a string separated by the given separators,
        /// or the elements of an array (separators are ignored for arrays); up to the given count;
        /// the options parameter specifies whether to return empty elements. For other types,
        /// returns an array with the value as the only element (or an empty array if the value is
        /// null and null values are to be omitted).
        /// </summary>
        public let Split(let separators, StringSplitOptions options = StringSplitOptions.None)
        {
            switch (_type)
            {
                case InternalType.Array:
                    if (options == StringSplitOptions.RemoveEmptyEntries)
                    {
                        return new let(_array.Where(i => i != null));
                    }
                    else
                    {
                        return new let(_array);
                    }
                case InternalType.String:
                    string[] sep = separators == null
                        ? null
                        : (separators._type == InternalType.Array
                            ? separators._array.Select(i => i.ToString()).ToArray()
                            : new string[] { separators.ToString() });
                    return new let(_string.Split(sep, options).Select(i => new let(i)));
                default:
                    var result = new let { _type = InternalType.Array };
                    if (options != StringSplitOptions.RemoveEmptyEntries || !Equals(null))
                    {
                        result.CopyValue(this);
                    }
                    return result;
            }
        }

        /// <summary>
        /// Returns an array that contains up to a given number of substrings in a string separated
        /// by the given separators, or the elements of an array (separators are ignored for arrays)
        /// up to the given count; the options parameter specifies whether to return empty elements.
        /// For other types, returns an array with the value as the only element (or an empty array
        /// if the value is null and null values are to be omitted).
        /// </summary>
        public let Split(let separators, int count, StringSplitOptions options = StringSplitOptions.None)
        {
            if (count <= 0)
            {
                return new let { _type = InternalType.Array };
            }
            switch (_type)
            {
                case InternalType.Array:
                    if (options == StringSplitOptions.RemoveEmptyEntries)
                    {
                        return new let(_array.Where(i => i != null).Take(count));
                    }
                    else
                    {
                        return new let(_array.Take(count));
                    }
                case InternalType.String:
                    string[] sep = separators == null
                        ? null
                        : (separators._type == InternalType.Array
                            ? separators._array.Select(i => i.ToString()).ToArray()
                            : new string[] { separators.ToString() });
                    return new let(_string.Split(sep, count, options).Select(i => new let(i)));
                default:
                    var result = new let { _type = InternalType.Array };
                    if (options != StringSplitOptions.RemoveEmptyEntries || !Equals(null))
                    {
                        result.CopyValue(this);
                    }
                    return result;
            }
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in a source array; or gets a substring; for
        /// other variable types, retrieves the item as an array with <paramref name="count"/> elements.
        /// </summary>
        /// <param name="index">The zero-based index at which the range starts.</param>
        public let Substring(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            switch (_type)
            {
                case InternalType.Array:
                    var targetCount = _array.Count - index;
                    if (targetCount <= 0)
                    {
                        return new let()
                        {
                            _type = InternalType.Array
                        };
                    }
                    else
                    {
                        return new let(_array.GetRange(index, targetCount));
                    }
                case InternalType.String:
                    if (index >= _string.Length)
                    {
                        return new let(string.Empty);
                    }
                    else
                    {
                        return new let(_string.Substring(index));
                    }
                default:
                    var newArr = new let
                    {
                        _type = InternalType.Array
                    };
                    newArr.Add(this);
                    return newArr;
            }
        }

        /// <summary>
        /// Creates a shallow copy of a range of elements in a source array; or gets a substring; for
        /// other variable types, retrieves the item as an array with <paramref name="count"/> elements.
        /// </summary>
        /// <param name="index">The zero-based index at which the range starts.</param>
        /// <param name="count">The number of elements in the range.</param>
        public let Substring(int index, int count) => GetRange(index, count);

        /// <summary>
        /// Gets a copy of this variable as an array; if it is not an array its value will become the first
        /// element.
        /// </summary>
        public object[] ToArray() => (object[])DeepClone().ConvertToArray().ToObject();

        /// <summary>
        /// Gets this variable as a boolean. Arrays are considered true if they have a nonzero
        /// length. Strings are considered true if they have a nonzero length and their value is not
        /// convertible to <see cref="bool.FalseString"/> or 0.
        /// </summary>
        public bool ToBoolean()
        {
            switch (_type)
            {
                case InternalType.Array:
                    return _array.Count > 0;
                case InternalType.Boolean:
                    return _boolean;
                case InternalType.Number:
                    return Convert.ToBoolean(_number);
                case InternalType.String:
                    return _string.ToBoolean();
                default:
                    return false;
            }
        }

        /// <summary>
        /// Gets this variable's child properties as a dictionary. The value of the variable is
        /// represented under the "Value" key. To get an object which represents the value (but
        /// does not contain the child properties), use <see cref="ToObject"/>.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            var d = new Dictionary<string, object>
            {
                { "Value", ToObject() }
            };
            foreach (var item in _children)
            {
                d.Add(item.Key, item.Value.ToObject());
            }
            return d;
        }

        /// <summary>
        /// Gets this variable as a double. Arrays return their length, as do strings which are not
        /// convertible to numbers or booleans.
        /// </summary>
        public double ToDouble()
        {
            switch (_type)
            {
                case InternalType.Array:
                    return _array.Count;
                case InternalType.Boolean:
                    return Convert.ToDouble(_boolean);
                case InternalType.Number:
                    return _number;
                case InternalType.String:
                    if (double.TryParse(_string, out var value))
                    {
                        return value;
                    }
                    else if (bool.TryParse(_string, out var bValue))
                    {
                        return Convert.ToDouble(bValue);
                    }
                    return string.IsNullOrEmpty(_string) ? 0 : _string.Length;
                default:
                    return double.NaN;
            }
        }

        /// <summary>
        /// Gets this variable's child properties as a dynamic object. The value of the variable is
        /// represented in the "Value" property. To get an object which represents the value (but
        /// does not contain the child properties), use <see cref="ToObject"/>.
        /// </summary>
        public dynamic ToDynamic()
        {
#pragma warning disable IDE0017
            dynamic obj = new ExpandoObject();
#pragma warning restore IDE0017
            obj.Value = ToObject();
            foreach (var item in _children)
            {
                obj[item.Key] = item.Value;
            }
            return obj;
        }

        /// <summary>
        /// Returns a copy of this variable with a string value, and any strings in an array, converted to lowercase.
        /// </summary>
        public let ToLower()
        {
            var result = DeepClone();
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.Select(i => i.ToLower()).ToList();
                    break;
                case InternalType.String:
                    result._string = _string.ToLower();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets this variable's value as an object. Child properties are not represented. To get an
        /// object which contains all child properties (but not the value), use <see cref="ToDynamic"/>.
        /// </summary>
        public object ToObject()
        {
            switch (_type)
            {
                case InternalType.Array:
                    return _array.Select(i => i.ToObject()).ToArray();
                case InternalType.Boolean:
                    return _boolean;
                case InternalType.Number:
                    if (_number.IsIntegral())
                    {
                        if (_number < int.MaxValue)
                        {
                            return Convert.ToInt32(_number);
                        }
                        else if (_number < long.MaxValue)
                        {
                            return Convert.ToInt64(_number);
                        }
                    }
                    return _number;
                case InternalType.String:
                    return _string;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Converts the value of this variable to a string representation. Operates recursively on arrays.
        /// </summary>
        public override string ToString()
        {
            switch (_type)
            {
                case InternalType.Array:
                    return $"[{string.Join(";", _array.Select(i => i.ToString()))}]";
                case InternalType.Boolean:
                    return _boolean.ToString();
                case InternalType.Number:
                    return _number.ToString();
                case InternalType.String:
                    return _string;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Returns a copy of this variable with a string value, and any strings in an array, converted to uppercase.
        /// </summary>
        public let ToUpper()
        {
            var result = DeepClone();
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.Select(i => i.ToUpper()).ToList();
                    break;
                case InternalType.String:
                    result._string = _string.ToUpper();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Converts the value of this variable to a string representation, using the specified
        /// format for numbers. Operates recursively on arrays.
        /// </summary>
        public string ToString(string format)
        {
            if (_type == InternalType.Number)
            {
                try
                {
                    return _number.ToString(format);
                }
                catch (FormatException)
                {
                    return _number.ToString();
                }
            }
            else
            {
                return ToString();
            }
        }

        /// <summary>
        /// Gets a deep copy of this variable with all null elements removed from the beginning and
        /// end of an array, and whitespace characters removed from the beginning and end of a
        /// string; gets an unchanged deep copy of other variable types.
        /// </summary>
        public let Trim()
        {
            var result = DeepClone();
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.SkipWhile(i => i == null).Reverse().SkipWhile(i => i == null).Reverse().ToList();
                    break;
                case InternalType.String:
                    result._string = _string.Trim();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets a deep copy of this variable with all indicated elements removed from the beginning
        /// and end of an array, and from the beginning and end of a string if they are strings; gets
        /// an unchanged deep copy of other variable types.
        /// </summary>
        public let Trim(let trimElements)
        {
            var result = DeepClone();
            if (trimElements == null)
            {
                return result;
            }
            var elements = trimElements._type == InternalType.Array ? trimElements._array : new List<let> { trimElements };
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.SkipWhile(i => elements.Any(t => i == t)).Reverse().SkipWhile(i => elements.Any(t => i == t)).Reverse().ToList();
                    break;
                case InternalType.String:
                    var retry = false;
                    do
                    {
                        retry = false;
                        foreach (var item in elements.Where(t => t._type == InternalType.String))
                        {
                            if (result._string.StartsWith(item._string))
                            {
                                result._string = result._string.Remove(0, item.Length);
                                retry = true;
                            }
                            if (result._string.EndsWith(item._string))
                            {
                                result._string = result._string.Remove(result.Length - item.Length);
                                retry = true;
                            }
                        }
                    } while (retry);
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets a deep copy of this variable with all null elements removed from the end of an
        /// array, and whitespace characters removed from the end of a string; gets an unchanged deep
        /// copy of other variable types.
        /// </summary>
        public let TrimEnd()
        {
            var result = DeepClone();
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.AsEnumerable().Reverse().SkipWhile(i => i == null).Reverse().ToList();
                    break;
                case InternalType.String:
                    result._string = _string.TrimEnd();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets a deep copy of this variable with all indicated elements removed from the beginning
        /// and end of an array, and from the beginning and end of a string if they are strings; gets
        /// an unchanged deep copy of other variable types.
        /// </summary>
        public let TrimEnd(let trimElements)
        {
            var result = DeepClone();
            if (trimElements == null)
            {
                return result;
            }
            var elements = trimElements._type == InternalType.Array ? trimElements._array : new List<let> { trimElements };
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.AsEnumerable().Reverse().SkipWhile(i => elements.Any(t => i == t)).Reverse().ToList();
                    break;
                case InternalType.String:
                    var retry = false;
                    do
                    {
                        retry = false;
                        foreach (var item in elements.Where(t => t._type == InternalType.String))
                        {
                            if (result._string.EndsWith(item._string))
                            {
                                result._string = result._string.Remove(result.Length - item.Length);
                                retry = true;
                            }
                        }
                    } while (retry);
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets a deep copy of this variable with all null elements removed from the beginning of an
        /// array, and whitespace characters removed from the beginning of a string; gets an
        /// unchanged deep copy of other variable types.
        /// </summary>
        public let TrimStart()
        {
            var result = DeepClone();
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.SkipWhile(i => i == null).ToList();
                    break;
                case InternalType.String:
                    result._string = _string.TrimStart();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets a deep copy of this variable with all indicated elements removed from the beginning
        /// of an array, and from the beginning of a string if they are strings; gets an unchanged
        /// deep copy of other variable types.
        /// </summary>
        public let TrimStart(let trimElements)
        {
            var result = DeepClone();
            if (trimElements == null)
            {
                return result;
            }
            var elements = trimElements._type == InternalType.Array ? trimElements._array : new List<let> { trimElements };
            switch (_type)
            {
                case InternalType.Array:
                    result._array = _array.SkipWhile(i => elements.Any(t => i == t)).ToList();
                    break;
                case InternalType.String:
                    var retry = false;
                    do
                    {
                        retry = false;
                        foreach (var item in elements.Where(t => t._type == InternalType.String))
                        {
                            if (result._string.StartsWith(item._string))
                            {
                                result._string = result._string.Remove(0, item.Length);
                                retry = true;
                            }
                        }
                    } while (retry);
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// Determines whether every element in an array matches the conditions defined by the
        /// specified predicate; for non-arrays evaluates the predicate for the value.
        /// </summary>
        public bool TrueForAll(Predicate<let> match)
        {
            if (match == null)
            {
                return false;
            }
            if (_type == InternalType.Array)
            {
                return _array.TrueForAll(match);
            }
            else
            {
                return match.Invoke(this);
            }
        }
    }
}
