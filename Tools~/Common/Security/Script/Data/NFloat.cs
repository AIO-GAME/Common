using System;

namespace AIO.Security
{
    public partial struct NFloat
    {
        private float _value;
        private int   _offset;
        private float _oldValue;

        private float Value
        {
            get
            {
                var result = _value - _offset;
                var invalid = _oldValue > result
                    ? _oldValue - result > 0.0001
                    : result - _oldValue > 0.0001;
                if (invalid)
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

        public NFloat(float val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public static NFloat operator ++(NFloat a)
        {
            a.Value++;
            return a;
        }

        public static NFloat operator --(NFloat a)
        {
            a.Value--;
            return a;
        }

        public static NFloat operator +(NFloat a, int    b) => new NFloat(a.Value + b);
        public static NFloat operator -(NFloat a, int    b) => new NFloat(a.Value - b);

        public static NFloat operator *(NFloat a, NFloat b) => new NFloat(a.Value * b.Value);
        public static NFloat operator *(NFloat a, float  b) => new NFloat(a.Value * b);
        public static NFloat operator *(NFloat a, int    b) => new NFloat(a.Value * b);

        public static NFloat operator /(NFloat a, NFloat b) => new NFloat(a.Value / b.Value);
        public static NFloat operator /(NFloat a, float  b) => new NFloat(a.Value / b);
        public static NFloat operator /(NFloat a, int    b) => new NFloat(a.Value / b);

        public static NFloat operator %(NFloat a, NFloat b) => new NFloat(a.Value % b.Value);
        public static NFloat operator %(NFloat a, float  b) => new NFloat(a.Value % b);
        public static NFloat operator %(NFloat a, int    b) => new NFloat(a.Value % b);
    }
}