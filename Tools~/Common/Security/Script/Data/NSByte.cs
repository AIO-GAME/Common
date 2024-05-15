namespace AIO.Security
{
    public partial struct NSByte
    {
        private sbyte _value;
        private sbyte _offset;
        private sbyte _oldValue;

        private sbyte Value
        {
            get
            {
                var result = (sbyte)(_value - _offset);
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;
                _offset   = (sbyte)SecurityUtil.RandomInt();
                _value    = (sbyte)(value + _offset);
            }
        }

        public NSByte(sbyte val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;
            Value     = val;
        }

        public NSByte(int val = 0)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = 0;

            Value = val > sbyte.MaxValue ? sbyte.MaxValue : (sbyte)val;
        }

        public static NSByte operator ++(NSByte a)
        {
            a.Value++;
            return a;
        }

        public static NSByte operator --(NSByte a)
        {
            a.Value--;
            return a;
        }

        public static NSByte operator *(NSByte a, NSByte b) => new NSByte(a.Value * b.Value);
        public static NSByte operator *(NSByte a, sbyte  b) => new NSByte(a.Value * b);

        public static NSByte operator /(NSByte a, NSByte b) => new NSByte(a.Value / b.Value);
        public static NSByte operator /(NSByte a, sbyte  b) => new NSByte(a.Value / b);

        public static NSByte operator %(NSByte a, NSByte b) => new NSByte(a.Value % b.Value);
        public static NSByte operator %(NSByte a, sbyte  b) => new NSByte(a.Value % b);

    }
}