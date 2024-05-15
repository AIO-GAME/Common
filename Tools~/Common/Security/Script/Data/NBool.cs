namespace AIO.Security
{
    public partial struct NBool
    {
        private int  _value;
        private int  _offset;
        private bool _oldValue;

        private bool Value
        {
            get
            {
                unchecked
                {
                    var result = _value - _offset != 0;
                    if (!_oldValue.Equals(result)) SecurityUtil.OnDetected();
                    return result;
                }
            }
            set
            {
                _oldValue = value;
                _offset   = SecurityUtil.RandomInt();
                _value    = value ? 1 : 0 + _offset;
            }
        }

        /// <param name="value"> <see cref="bool"/> </param>
        public NBool(bool value = false)
        {
            _value    = 0;
            _offset   = 0;
            _oldValue = true;
            Value     = value;
        }

        /// <param name="a"> <see cref="NBool"/> </param>
        public static bool operator !(NBool a) { return !a.Value; }
    }
}