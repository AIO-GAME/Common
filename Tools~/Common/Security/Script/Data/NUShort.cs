using System;

namespace AIO.Security
{
    public partial struct NUShort
    {
        private ushort _value;
        private ushort _offset;
        private ushort _oldValue;

        private ushort Value
        {
            get
            {
                var result = (ushort)(_value - _offset);
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;
                _offset   = SecurityUtil.RandomUShort();
                _value    = (ushort)(value + _offset);
            }
        }

        public NUShort(ushort val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public NUShort(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val > ushort.MaxValue ? ushort.MaxValue : (ushort)val;
        }

        public static NUShort operator ++(NUShort a)
        {
            a.Value++;
            return a;
        }

        public static NUShort operator --(NUShort a)
        {
            a.Value--;
            return a;
        }

        public static NUShort operator *(NUShort a, NUShort b) => new NUShort(a.Value * b.Value);
        public static NUShort operator *(NUShort a, ushort  b) => new NUShort(a.Value * b);

        public static NUShort operator /(NUShort a, NUShort b) => new NUShort(a.Value / b.Value);
        public static NUShort operator /(NUShort a, ushort  b) => new NUShort(a.Value / b);

        public static NUShort operator %(NUShort a, NUShort b) => new NUShort(a.Value % b.Value);
        public static NUShort operator %(NUShort a, ushort  b) => new NUShort(a.Value % b);
    }
}