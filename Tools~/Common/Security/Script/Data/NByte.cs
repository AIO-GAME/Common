using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    public partial struct NByte
    {
        private byte _value;
        private byte _offset;
        private byte _oldValue;

        private byte Value
        {
            get
            {
                var result = (byte)(_value - _offset);
                if (!_oldValue.Equals(result)) SecurityUtil.OnDetected();
                return result;
            }

            set
            {
                unchecked
                {
                    _oldValue = value;
                    _offset   = (byte)SecurityUtil.RandomInt();
                    _value    = (byte)(value + _offset);
                }
            }
        }

        /// <param name="value"> <see cref="byte"/> </param>
        public NByte(byte value = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = value;
        }

        /// <param name="value"> <see cref="byte"/> </param>
        public NByte(int value = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = value > byte.MaxValue ? byte.MaxValue : (byte)value;
        }

        public static NByte operator *(NByte a, NByte b) => new NByte(a.Value * b.Value);
        public static NByte operator *(NByte a, byte  b) => new NByte(a.Value * b);

        public static NByte operator /(NByte a, NByte b) => new NByte(a.Value / b.Value);
        public static NByte operator /(NByte a, byte  b) => new NByte(a.Value / b);

        public static NByte operator %(NByte a, NByte b) => new NByte(a.Value % b.Value);
        public static NByte operator %(NByte a, byte  b) => new NByte(a.Value % b);
    }
}