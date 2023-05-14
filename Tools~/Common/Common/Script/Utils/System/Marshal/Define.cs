namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    public static class Define //define some constant
    {
        /// <summary>
        /// maximum length of identicardid
        /// </summary>
        public const int MAX_LENGTH_OF_IDENTICARDID = 20;

        /// <summary>
        /// maximum length of name
        /// </summary>
        public const int MAX_LENGTH_OF_NAME = 50;

        /// <summary>
        /// maximum length of country
        /// </summary>
        public const int MAX_LENGTH_OF_COUNTRY = 50;

        /// <summary>
        /// maximum length of nation
        /// </summary>
        public const int MAX_LENGTH_OF_NATION = 50;

        /// <summary>
        /// maximum length of birthday
        /// </summary>
        public const int MAX_LENGTH_OF_BIRTHDAY = 8;

        /// <summary>
        /// maximum length of address
        /// </summary>
        public const int MAX_LENGTH_OF_ADDRESS = 200;

        //MarshalAs:指示如何在托管代码和非托管代码之间封送数据
        //UnmanagedType:指定如何将参数或字段封送到非托管内存块
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = Define.MAX_LENGTH_OF_IDENTICARDID)]
    }
}
