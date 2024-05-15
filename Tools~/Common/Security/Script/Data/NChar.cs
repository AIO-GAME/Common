using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    public partial struct NChar
    {
        private int  _value;
        private int  _offset;
        private char _oldValue;

        private char Value
        {
            get
            {
                unchecked
                {
                    var result = (char)(_value - _offset);
                    if (!_oldValue.Equals(result)) SecurityUtil.OnDetected();
                    return result;
                }
            }

            set
            {
                _oldValue = value;
                _offset   = SecurityUtil.RandomInt();
                _value    = value + _offset;
            }
        }

        public static NChar operator ++(NChar a)
        {
            a.Value++;
            return a;
        }

        public static NChar operator --(NChar a)
        {
            a.Value--;
            return a;
        }

        public static NChar operator +(NChar a, int   b) => new NChar(a.Value + b);
        public static NChar operator -(NChar a, int   b) => new NChar(a.Value - b);

        public static NChar operator *(NChar a, NChar b) => new NChar(a.Value * b.Value);
        public static NChar operator *(NChar a, int   b) => new NChar(a.Value * b);

        public static NChar operator /(NChar a, NChar b) => new NChar(a.Value / b.Value);
        public static NChar operator /(NChar a, int   b) => new NChar(a.Value / b);

        public static NChar operator %(NChar a, NChar b) => new NChar(a.Value % b.Value);
        public static NChar operator %(NChar a, int   b) => new NChar(a.Value % b);
    }
}