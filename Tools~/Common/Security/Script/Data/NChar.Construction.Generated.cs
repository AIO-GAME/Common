using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    partial struct NChar
    {
        #region bool

        /// <param name="value"> <see cref="bool"/> </param>
        public NChar(bool value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region sbyte

        /// <param name="value"> <see cref="sbyte"/> </param>
        public NChar(sbyte value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region short

        /// <param name="value"> <see cref="short"/> </param>
        public NChar(short value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region int

        /// <param name="value"> <see cref="int"/> </param>
        public NChar(int value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region long

        /// <param name="value"> <see cref="long"/> </param>
        public NChar(long value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region byte

        /// <param name="value"> <see cref="byte"/> </param>
        public NChar(byte value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region ushort

        /// <param name="value"> <see cref="ushort"/> </param>
        public NChar(ushort value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region uint

        /// <param name="value"> <see cref="uint"/> </param>
        public NChar(uint value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region ulong

        /// <param name="value"> <see cref="ulong"/> </param>
        public NChar(ulong value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region char

        /// <param name="value"> <see cref="char"/> </param>
        public NChar(char value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region string

        /// <param name="value"> <see cref="string"/> </param>
        public NChar(string value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region float

        /// <param name="value"> <see cref="float"/> </param>
        public NChar(float value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region double

        /// <param name="value"> <see cref="double"/> </param>
        public NChar(double value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

        #region decimal

        /// <param name="value"> <see cref="decimal"/> </param>
        public NChar(decimal value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = ' ';
            try
            {
                Value = Convert.ToChar(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{char.MinValue}");
                Value = char.MinValue;
            }
        }

        #endregion

    }
}