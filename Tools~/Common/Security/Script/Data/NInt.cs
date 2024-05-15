using System;
using System.Globalization;

namespace AIO.Security
{
    public partial struct NInt
    {
        private int _value;
        private int _offset;
        private int _oldValue;

        private int Value
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
                _offset   = SecurityUtil.RandomInt();
                _value    = value + _offset;
            }
        }

        public NInt(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public static NInt operator ++(NInt a)
        {
            a.Value++;
            return a;
        }

        public static NInt operator --(NInt a)
        {
            a.Value--;
            return a;
        }

        public static NInt operator *(NInt a, NInt b) => new NInt(a.Value * b.Value);
        public static NInt operator *(NInt a, int  b) => new NInt(a.Value * b);

        public static NInt operator /(NInt a, NInt b) => new NInt(a.Value / b.Value);
        public static NInt operator /(NInt a, int  b) => new NInt(a.Value / b);

        public static NInt operator %(NInt a, NInt b) => new NInt(a.Value % b.Value);
        public static NInt operator %(NInt a, int  b) => new NInt(a.Value % b);
    }
}