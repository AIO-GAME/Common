namespace AIO.Security
{
    public partial struct NShort
    {
        private short _value;
        private short _offset;
        private short _oldValue;

        private short Value
        {
            get
            {
                var result = (short)(_value - _offset);
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;

                _offset = (short)SecurityUtil.RandomInt();
                _value  = (short)(value + _offset);
            }
        }

        public NShort(short val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public NShort(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val > short.MaxValue ? short.MaxValue : (short)val;
        }

        public static NShort operator *(NShort a, NShort b) => new NShort(a.Value * b.Value);
        public static NShort operator *(NShort a, short  b) => new NShort(a.Value * b);

        public static NShort operator /(NShort a, NShort b) => new NShort(a.Value / b.Value);
        public static NShort operator /(NShort a, short  b) => new NShort(a.Value / b);

        public static NShort operator %(NShort a, NShort b) => new NShort(a.Value % b.Value);
        public static NShort operator %(NShort a, short  b) => new NShort(a.Value % b);
    }
}