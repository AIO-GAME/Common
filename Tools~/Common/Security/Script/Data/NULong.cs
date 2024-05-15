using System;

namespace AIO.Security
{
    public partial struct NULong
    {
        private ulong _value;
        private uint  _offset;
        private ulong _oldValue;

        private ulong Value
        {
            get
            {
                var result = _value - _offset;
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;
                _offset   = SecurityUtil.RandomUInt();
                _value    = value + _offset;
            }
        }

        public NULong(ulong val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public NULong(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = (ulong)val;
        }

        public static NULong operator ++(NULong a)
        {
            a.Value++;
            return a;
        }

        public static NULong operator --(NULong a)
        {
            a.Value--;
            return a;
        }

        public static NULong operator *(NULong a, NULong b) => new NULong(a.Value * b.Value);
        public static NULong operator *(NULong a, ulong  b) => new NULong(a.Value * b);

        public static NULong operator /(NULong a, NULong b) => new NULong(a.Value / b.Value);
        public static NULong operator /(NULong a, ulong  b) => new NULong(a.Value / b);

        public static NULong operator %(NULong a, NULong b) => new NULong(a.Value % b.Value);
        public static NULong operator %(NULong a, ulong  b) => new NULong(a.Value % b);
    }
}