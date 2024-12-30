using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYAML_Library
{
    public class CustomValueTypeClass
    {
        #region Int24, UInt24

        /// <summary>
        /// 24bit符号付き整数
        /// </summary>
        public struct Int24 : IComparable
        {
            private int value { get; set; }
            public Int24 Value
            {
                get
                {
                    return value;
                }
                set
                {
                    this.value = value;
                }

                //get
                //{
                //    return (Int24)value;
                //}
                //set
                //{
                //    this.value = (int)value;
                //}
            }

            public static implicit operator int(Int24 v)
            {
                return v.value;
            }

            public static implicit operator Int24(int v)
            {
                return new Int24(v);
            }

            //public static explicit operator int(Int24 v)
            //{
            //    //TODO
            //    return v.value;
            //}

            //public static explicit operator Int24(int v)
            //{
            //    return new Int24(v);
            //}


            public Int24 MaxValue => 16777215;

            public Int24 MinValue => unchecked((Int24)0b_1111_1111_1111_1111_1111_1111);

            public override bool Equals(object obj)
            {
                return obj is Int24 @int && Equals(@int);
            }

            public bool Equals(Int24 other)
            {
                return EqualityComparer<Int24>.Default.Equals(value, other.value) &&
                       EqualityComparer<Int24>.Default.Equals(Value, other.Value) &&
                       EqualityComparer<Int24>.Default.Equals(MaxValue, other.MaxValue) &&
                       EqualityComparer<Int24>.Default.Equals(MinValue, other.MinValue);
            }

            public int CompareTo(object obj)
            {
                if ((object)obj == null) return 1;
                if (this.GetType() != obj.GetType()) throw new ArgumentException();

                return this.value.CompareTo(((Int24)obj).value);
            }

            public override int GetHashCode()
            {
                int hashCode = 325600102;
                hashCode = hashCode * -1521134295 + value.GetHashCode();
                hashCode = hashCode * -1521134295 + Value.GetHashCode();
                hashCode = hashCode * -1521134295 + MaxValue.GetHashCode();
                hashCode = hashCode * -1521134295 + MinValue.GetHashCode();
                return hashCode;
            }

            public static bool operator ==(Int24 left, Int24 right)
            {
                if ((object)left == null)
                {
                    return ((object)right == null);
                }
                if ((object)right == null)
                {
                    return false;
                }

                return left.Equals(right);
                //return left == right;
            }

            public static bool operator !=(Int24 left, Int24 right)
            {
                return left != right;
            }

            public static bool operator <(Int24 left, Int24 right)
            {
                if ((object)left == null || (object)right == null) throw new ArgumentNullException();
                return (left.CompareTo(right) < 0);

                //return left < right;
            }

            public static bool operator >(Int24 left, Int24 right)
            {
                return (right < left);
                //return left > right;
            }

            public static bool operator <=(Int24 left, Int24 right)
            {
                return left <= right;
            }

            public static bool operator >=(Int24 left, Int24 right)
            {
                return left >= right;
            }

            public static Int24 operator +(Int24 left, Int24 right)
            {
                return new Int24(left.value + right.value);
            }

            public static Int24 operator -(Int24 left, Int24 right)
            {
                return new Int24(left.value - right.value);
            }

            public static Int24 operator *(Int24 left, Int24 right)
            {
                return new Int24(left.value * right.value);
            }

            public static Int24 operator /(Int24 left, Int24 right)
            {
                return new Int24(left.value / right.value);
            }

            public static Int24 operator %(Int24 left, Int24 right)
            {
                return new Int24(left.value % right.value);
            }

            public override string ToString()
            {
                return value.ToString();
            }

            public Int24(int v)
            {
                value = v;
            }
        }

        /// <summary>
        /// 24bit符号なし整数
        /// </summary>
        public struct UInt24
        {
            private uint value { get; set; }
            public UInt24 Value
            {
                get
                {
                    return (UInt24)value;
                }
                set
                {
                    this.value = (uint)value;
                }
            }

            public static implicit operator UInt24(uint v)
            {
                return new UInt24(v);
            }

            public static explicit operator uint(UInt24 v)
            {
                //TODO
                return (uint)v;
            }

            public UInt24 MaxValue => 16777215;

            public UInt24 MinValue => 0;

            public override bool Equals(object obj)
            {
                return obj is UInt24 @int && Equals(@int);
            }

            public bool Equals(UInt24 other)
            {
                return EqualityComparer<UInt24>.Default.Equals(value, other.value) &&
                       EqualityComparer<UInt24>.Default.Equals(Value, other.Value) &&
                       EqualityComparer<UInt24>.Default.Equals(MaxValue, other.MaxValue) &&
                       EqualityComparer<UInt24>.Default.Equals(MinValue, other.MinValue);
            }

            public override int GetHashCode()
            {
                int hashCode = 325600102;
                hashCode = hashCode * -1521134295 + value.GetHashCode();
                hashCode = hashCode * -1521134295 + Value.GetHashCode();
                hashCode = hashCode * -1521134295 + MaxValue.GetHashCode();
                hashCode = hashCode * -1521134295 + MinValue.GetHashCode();
                return hashCode;
            }

            public static bool operator ==(UInt24 left, UInt24 right)
            {
                return left == right;
            }

            public static bool operator !=(UInt24 left, UInt24 right)
            {
                return left != right;
            }

            public static bool operator <(UInt24 left, UInt24 right)
            {
                return left < right;
            }

            public static bool operator >(UInt24 left, UInt24 right)
            {
                return left > right;
            }

            public static bool operator <=(UInt24 left, UInt24 right)
            {
                return left <= right;
            }

            public static bool operator >=(UInt24 left, UInt24 right)
            {
                return left >= right;
            }

            public override string ToString()
            {
                //TODO
                //return ((UInt24)value).ToString();

                return value.ToString();
            }

            public static implicit operator UInt24(Int24 data)
            {
                return (UInt24)data;
            }

            public UInt24(uint v)
            {
                value = v;
            }
        }
        #endregion

        /// <summary>
        /// Convert to Int24
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="IsLittleEndian"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public unsafe static Int24 ToInt24(byte[] value, int startIndex, bool IsLittleEndian = false)
        {
            if (value == null) throw new ArgumentNullException("value");
            if ((uint)startIndex >= value.Length) throw new ArgumentOutOfRangeException();
            if (startIndex > value.Length - 3) throw new ArgumentException();

            fixed (byte* ptr = &value[startIndex])
            {
                if (startIndex % 3 == 0)
                {
                    return *(Int24*)ptr;
                }

                if (IsLittleEndian)
                {
                    return *ptr | (ptr[1] << 8) | (ptr[2] << 16);
                }

                return (*ptr << 16) | (ptr[1] << 8) | ptr[2];
            }
        }

        /// <summary>
        /// Getbytes (Int24)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public unsafe static byte[] GetBytes(Int24 value)
        {
            byte[] array = new byte[3];
            fixed (byte* ptr = array)
            {
                *(Int24*)ptr = value;
            }

            return array;
        }

        /// <summary>
        /// Convert to UInt24
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <param name="IsLittleEndian"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public unsafe static UInt24 ToUInt24(byte[] value, int startIndex, bool IsLittleEndian = false)
        {
            if (value == null) throw new ArgumentNullException("value");
            if ((uint)startIndex >= value.Length) throw new ArgumentOutOfRangeException();
            if (startIndex > value.Length - 3) throw new ArgumentException();

            return (UInt24)ToInt24(value, startIndex, IsLittleEndian);
        }

        /// <summary>
        /// Getbytes (Int24)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public unsafe static byte[] GetBytes(UInt24 value)
        {
            byte[] array = new byte[3];
            fixed (byte* ptr = array)
            {
                *(UInt24*)ptr = value;
            }

            return array;
        }
    }
}
