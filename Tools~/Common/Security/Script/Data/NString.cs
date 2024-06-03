namespace AIO.Security
{
    public partial struct NString
    {
        private string _key;
        private string _oldValue;

        private string Value
        {
            get
            {
                var result = string.IsNullOrEmpty(_key) ? _key : _key.AESDecrypt(SecurityUtil.AESKey);
                if (!_oldValue.Equals(result))
                {
                    SecurityUtil.OnDetected();
                }

                return result;
            }

            set
            {
                _oldValue = value;

                _key = string.IsNullOrEmpty(value) ? value : value.AESEncrypt(SecurityUtil.AESKey);
            }
        }

        public NString(string val = "")
        {
            _key      = "";
            _oldValue = "";
            Value     = val;
        }

        public static NString operator +(NString a, NString b) => new NString(a.Value + b.Value);
        public static NString operator +(NString a, string  b) => new NString(a.Value + b);
    }
}