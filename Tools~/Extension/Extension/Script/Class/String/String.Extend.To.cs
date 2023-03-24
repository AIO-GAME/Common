using System;

namespace AIO
{
    public static partial class StringExtend
    {
        public static string ToBinaryString(this int value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static string ToBinaryString(this long value)
        {
            return Convert.ToString(value, 2).PadLeft(16, '0');
        }

        public static string ToBinaryString(this Enum value)
        {
            return Convert.ToString(Convert.ToInt64(value), 2).PadLeft(16, '0');
        }
    }
}