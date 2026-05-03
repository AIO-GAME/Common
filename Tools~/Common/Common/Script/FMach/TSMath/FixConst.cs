namespace AIO.FMach
{
    partial struct FP
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

        public const long MAX_VALUE         = long.MaxValue;
        public const long MIN_VALUE         = long.MinValue;
        public const int  NUM_BITS          = 64;
        public const int  FRACTIONAL_PLACES = 32;
        public const long ONE               = 1L << FRACTIONAL_PLACES;
        public const long TEN               = 10L << FRACTIONAL_PLACES;
        public const long HALF              = 1L << (FRACTIONAL_PLACES - 1);
        public const long PI_TIMES_2        = 0x6487ED511;
        public const long PI                = 0x3243F6A88;
        public const long PI_OVER_2         = 0x1921FB544;
        public const long LN2               = 0xB17217F7;
        public const long LOG2MAX           = 0x1F00000000;
        public const long LOG2MIN           = -0x2000000000;
        public const int  LUT_SIZE          = (int)(PI_OVER_2 >> 15);

        // Precision of this type is 2^-32, that is 2,3283064365386962890625E-10
        public static readonly decimal Precision = (decimal)(new FP(1L)); //0.00000000023283064365386962890625m;
        public static readonly FP      MaxValue  = new FP(MAX_VALUE - 1);
        public static readonly FP      MinValue  = new FP(MIN_VALUE + 2);
        public static readonly FP      One       = new FP(ONE);
        public static readonly FP      Ten       = new FP(TEN);
        public static readonly FP      Half      = new FP(HALF);

        public static readonly FP Zero             = new FP();
        public static readonly FP PositiveInfinity = new FP(MAX_VALUE);
        public static readonly FP NegativeInfinity = new FP(MIN_VALUE + 1);
        public static readonly FP NaN              = new FP(MIN_VALUE);

        public static readonly FP EN1     = One / 10;
        public static readonly FP EN2     = One / 100;
        public static readonly FP EN3     = One / 1000;
        public static readonly FP EN4     = One / 10000;
        public static readonly FP EN5     = One / 100000;
        public static readonly FP EN6     = One / 1000000;
        public static readonly FP EN7     = One / 10000000;
        public static readonly FP EN8     = One / 100000000;
        public static readonly FP Epsilon = EN3;

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释

        /// <summary>
        /// The value of Pi
        /// </summary>
        public static readonly FP Pi = new FP(PI);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP PiOver2 = new FP(PI_OVER_2);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP PiTimes2 = new FP(PI_TIMES_2);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP PiInv = (FP)0.3183098861837906715377675267M;

        /// <summary>
        ///
        /// </summary>
        public static readonly FP PiOver2Inv = (FP)0.6366197723675813430755350535M;

        /// <summary>
        ///
        /// </summary>
        public static readonly FP Deg2Rad = Pi / new FP(180);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP Rad2Deg = new FP(180) / Pi;

        /// <summary>
        ///
        /// </summary>
        public static readonly FP LutInterval = (FP)(LUT_SIZE - 1) / PiOver2;

        /// <summary>
        ///
        /// </summary>
        public static readonly FP Log2Max = new FP(LOG2MAX);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP Log2Min = new FP(LOG2MIN);

        /// <summary>
        ///
        /// </summary>
        public static readonly FP Ln2 = new FP(LN2);
    }
}