namespace AIO
{
    public partial class Unit
    {
        #region Nested type: Chinese

        /// <summary>
        /// 中文单位
        /// </summary>
        public static class Chinese
        {
            /// <summary>
            /// 数字单位:个数组
            /// </summary>
            public static readonly string[] CNSNum = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

            /// <summary>
            /// 数字单位:位数组
            /// </summary>
            public static readonly string[] CNSDigit = { "", "十", "百", "千" };

            /// <summary>
            /// 数字单位:单位数组
            /// </summary>
            public static readonly string[] CNSUnits = { "", "万", "亿", "万亿" };

            /// <summary>
            /// 数字字符串
            /// </summary>
            public static readonly string[] NumberChar =
            {
                "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"
            };
        }

        #endregion
    }
}