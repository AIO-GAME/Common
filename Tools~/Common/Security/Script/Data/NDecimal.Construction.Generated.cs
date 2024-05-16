﻿


using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    partial struct NDecimal
    {

        #region bool

        /// <param name="value"> <see cref="bool"/> </param>
        public NDecimal(bool value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region sbyte

        /// <param name="value"> <see cref="sbyte"/> </param>
        public NDecimal(sbyte value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region short

        /// <param name="value"> <see cref="short"/> </param>
        public NDecimal(short value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region int

        /// <param name="value"> <see cref="int"/> </param>
        public NDecimal(int value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region long

        /// <param name="value"> <see cref="long"/> </param>
        public NDecimal(long value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region byte

        /// <param name="value"> <see cref="byte"/> </param>
        public NDecimal(byte value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region ushort

        /// <param name="value"> <see cref="ushort"/> </param>
        public NDecimal(ushort value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region uint

        /// <param name="value"> <see cref="uint"/> </param>
        public NDecimal(uint value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region ulong

        /// <param name="value"> <see cref="ulong"/> </param>
        public NDecimal(ulong value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region char

        /// <param name="value"> <see cref="char"/> </param>
        public NDecimal(char value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region string

        /// <param name="value"> <see cref="string"/> </param>
        public NDecimal(string value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region float

        /// <param name="value"> <see cref="float"/> </param>
        public NDecimal(float value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region double

        /// <param name="value"> <see cref="double"/> </param>
        public NDecimal(double value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


        #region decimal

        /// <param name="value"> <see cref="decimal"/> </param>
        public NDecimal(decimal value = default)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            try
            {
                Value = Convert.ToDecimal(value);
            }
            catch
            {
                Console.WriteLine($"无法将{value}变为{Value.GetType()},已改为{decimal.Zero}");
                Value = decimal.Zero;
            }
        }

        #endregion


    }
}