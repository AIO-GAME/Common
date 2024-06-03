namespace AIO.Security
{
    public partial struct NLong
    {
        private long _value;
        private int  _offset;
        private long _oldValue;

        private long Value
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

        public NLong(long val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public static NLong operator *(NLong a, NLong b) => new NLong(a.Value * b.Value);
        public static NLong operator *(NLong a, long  b) => new NLong(a.Value * b);

        public static NLong operator /(NLong a, NLong b) => new NLong(a.Value / b.Value);
        public static NLong operator /(NLong a, long  b) => new NLong(a.Value / b);

        public static NLong operator %(NLong a, NLong b) => new NLong(a.Value % b.Value);
        public static NLong operator %(NLong a, long  b) => new NLong(a.Value % b);
    }
}