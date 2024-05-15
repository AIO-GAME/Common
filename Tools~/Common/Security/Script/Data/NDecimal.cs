using System;
using System.Globalization;

namespace AIO.Security
{
    public partial struct NDecimal
    {
        private decimal _value;
        private int     _offset;
        private decimal _oldValue;

        private decimal Value
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

        public static NDecimal operator ++(NDecimal a)
        {
            a.Value++;
            return a;
        }

        public static NDecimal operator --(NDecimal a)
        {
            a.Value--;
            return a;
        }

        public static NDecimal operator *(NDecimal a, NDecimal b) => new NDecimal(a.Value * b.Value);
        public static NDecimal operator *(NDecimal a, decimal  b) => new NDecimal(a.Value * b);

        public static NDecimal operator /(NDecimal a, NDecimal b) => new NDecimal(a.Value / b.Value);
        public static NDecimal operator /(NDecimal a, decimal  b) => new NDecimal(a.Value / b);

        public static NDecimal operator %(NDecimal a, NDecimal b) => new NDecimal(a.Value % b.Value);
        public static NDecimal operator %(NDecimal a, decimal  b) => new NDecimal(a.Value % b);
    }
}