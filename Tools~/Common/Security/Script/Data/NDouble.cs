namespace AIO.Security
{
    public partial struct NDouble
    {
        private double _value;
        private int    _offset;
        private double _oldValue;

        private double Value
        {
            get
            {
                var result = _value - _offset;
                var invalid = _oldValue > result
                    ? _oldValue - result > double.Epsilon
                    : result - _oldValue > double.Epsilon;
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

        public NDouble(double val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public static NDouble operator +(NDouble a, float b) => new NDouble(a.Value + b);
        public static NDouble operator +(NDouble a, int   b) => new NDouble(a.Value + b);

        public static NDouble operator -(NDouble a, float b) => new NDouble(a.Value - b);
        public static NDouble operator -(NDouble a, int   b) => new NDouble(a.Value - b);

        public static NDouble operator *(NDouble a, NDouble b) => new NDouble(a.Value * b.Value);
        public static NDouble operator *(NDouble a, double  b) => new NDouble(a.Value * b);
        public static NDouble operator *(NDouble a, float   b) => new NDouble(a.Value * b);
        public static NDouble operator *(NDouble a, int     b) => new NDouble(a.Value * b);

        public static NDouble operator /(NDouble a, NDouble b) => new NDouble(a.Value / b.Value);
        public static NDouble operator /(NDouble a, double  b) => new NDouble(a.Value / b);
        public static NDouble operator /(NDouble a, float   b) => new NDouble(a.Value / b);
        public static NDouble operator /(NDouble a, int     b) => new NDouble(a.Value / b);

        public static NDouble operator %(NDouble a, NDouble b) => new NDouble(a.Value % b.Value);
        public static NDouble operator %(NDouble a, double  b) => new NDouble(a.Value % b);
        public static NDouble operator %(NDouble a, float   b) => new NDouble(a.Value % b);
        public static NDouble operator %(NDouble a, int     b) => new NDouble(a.Value % b);
    }
}