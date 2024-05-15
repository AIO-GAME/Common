using System;

namespace AIO.Security
{
    public partial struct NUInt
    {
        private uint _value;
        private uint _offset;
        private uint _oldValue;

        private uint Value
        {
            get
            {
                var result = (uint)(_value - _offset);
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;

                _offset = SecurityUtil.RandomUInt();
                _value  = (uint)(value + _offset);
            }
        }

        public NUInt(uint val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public NUInt(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = (uint)val;
        }

        public static NUInt operator ++(NUInt a)
        {
            a.Value++;
            return a;
        }

        public static NUInt operator --(NUInt a)
        {
            a.Value--;
            return a;
        }

        public static NUInt operator *(NUInt a, NUInt b) => new NUInt(a.Value * b.Value);
        public static NUInt operator *(NUInt a, uint  b) => new NUInt(a.Value * b);

        public static NUInt operator /(NUInt a, NUInt b) => new NUInt(a.Value / b.Value);
        public static NUInt operator /(NUInt a, uint  b) => new NUInt(a.Value / b);

        public static NUInt operator %(NUInt a, NUInt b) => new NUInt(a.Value % b.Value);
        public static NUInt operator %(NUInt a, uint  b) => new NUInt(a.Value % b);
    }
}