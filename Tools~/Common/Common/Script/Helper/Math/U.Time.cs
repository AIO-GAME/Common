/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Text;
using AIO;
using EDateTimeUnit = AIO.Unit.Time.DateTimeUnit;
using ESecondUnit = AIO.Unit.Time.SencondUnit;

public partial class AHelper
{
    private AHelper()
    {
    }

    private static AHelper Instance { get; } = new AHelper();

    /// <summary>
    /// 时间方法库
    /// </summary>
    public partial class Time
    {
        /// <summary>
        /// 前天开始时间 单位毫秒
        /// </summary>
        public static long BeforeYesterday => GetCurrDateToDay(-2);

        /// <summary>
        /// 昨天开始时间 单位毫秒
        /// </summary>
        public static long Yesterday => GetCurrDateToDay(-1);

        /// <summary>
        /// 当天开始时间 单位毫秒
        /// </summary>
        public static long Today => GetCurrDateToDay();

        /// <summary>
        /// 明天开始时间 单位毫秒
        /// </summary>
        public static long TomorrowDay => GetCurrDateToDay(1);

        /// <summary>
        /// 后天开始时间 单位毫秒
        /// </summary>
        public static long AfterDay => GetCurrDateToDay(2);

        /// <summary>
        /// 获取当前时间 文字格式
        /// </summary>
        public static string GetCurrTimeStr(in string format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 获取当前时间搓
        /// </summary>
        public static long GetCurrTime(in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            return Normalize(DateTime.Now, unit);
        }

        /// <summary>
        /// 获取时间搓
        /// </summary>
        public static long GetDateTime(in string format, in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            return Normalize(Convert.ToDateTime(format), unit);
        }

        /// <summary>
        /// 格式化时间，参数：格林威治时间，格式化格式（具体见文件末尾）
        /// </summary>
        public static string Format(in long time, in string format = "yyyy-MM-dd 00:00:00",
            ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            return TimeZoneInfo.ConvertTimeToUtc(GetDateTime(time, unit)).ToString(format);
        }

        /// <summary>
        /// © 获取DateTime 时间磋单位支持 纳秒 微秒 毫秒 秒
        /// </summary>
        public static DateTime GetDateTime(in long time, in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            long Ticks;
            switch (unit)
            {
                case ESecondUnit.SECOND:
                    Ticks = Unit.Time.NS_SECOND / 100;
                    break;
                case ESecondUnit.MILLISCOND:
                    Ticks = Unit.Time.μS_SECOND / 100;
                    break;
                case ESecondUnit.MICROSECOND:
                    Ticks = Unit.Time.MS_SECOND / 100;
                    break;
                case ESecondUnit.NANOSECOND:
                    Ticks = Unit.Time.SECOND / 100;
                    break;
                case ESecondUnit.NANOSECOND_100:
                default: return new DateTime(time);
            }

            return new DateTime(time * Ticks, DateTimeKind.Utc);
        }

        /// <summary>
        /// © 获取TimeSpan 时间磋单位支持 纳秒 微秒 毫秒 秒
        /// </summary>
        public static TimeSpan GetTimeSpan(in long time, in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            switch (unit)
            {
                case ESecondUnit.SECOND:
                    return new TimeSpan(time * Unit.Time.NS_SECOND / 100);
                case ESecondUnit.MILLISCOND:
                    return new TimeSpan(time * Unit.Time.μS_SECOND / 100);
                case ESecondUnit.MICROSECOND:
                    return new TimeSpan(time * Unit.Time.MS_SECOND / 100);
                case ESecondUnit.NANOSECOND:
                    return new TimeSpan(time * Unit.Time.SECOND / 100);
                case ESecondUnit.NANOSECOND_100:
                default: return new TimeSpan(time);
            }
        }

        /// <summary>
        /// 获取指定单位的时间搓
        /// </summary>
        public static long Normalize(in DateTime date, in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            switch (unit)
            {
                case ESecondUnit.SECOND: return date.Ticks / 10000000;
                case ESecondUnit.MILLISCOND: return date.Ticks / 10000;
                case ESecondUnit.MICROSECOND: return date.Ticks / 10;
                case ESecondUnit.NANOSECOND: return date.Ticks * 100;
                case ESecondUnit.NANOSECOND_100:
                default: return date.Ticks;
            }
        }

        /// <summary>
        /// 获取当天差距 时间信息
        /// </summary>
        public static long GetCurrDateToDay(in int space = 0, in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            return Normalize(DateTime.Today.AddDays(space), unit);
        }

        /// <summary>
        /// 获取开始时间
        /// </summary>
        /// <param name="time">时间搓</param>
        /// <param name="DateType">获取类型 年 季 月 周 日</param>
        /// <param name="unit">时间搓 单位 纳秒 微秒 毫秒 秒</param>
        public static long GetTimeStartByType(
            in long time,
            in EDateTimeUnit DateType,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            var T = GetDateTime(time, unit);
            switch (DateType)
            {
                case EDateTimeUnit.Day:
                    T = T.AddDays(0);
                    break;
                case EDateTimeUnit.Week:
                    T = T.AddDays(-Convert.ToInt16(T.DayOfWeek) + 1);
                    break;
                case EDateTimeUnit.Month:
                    T = T.AddDays(-T.Day + 1);
                    break;
                case EDateTimeUnit.Season:
                    var time1 = T.AddMonths(0 - ((T.Month - 1) % 3));
                    T = time1.AddDays(-time1.Day + 1);
                    break;
                case EDateTimeUnit.Year:
                    T = T.AddDays(-T.DayOfYear + 1);
                    break;
            }

            return Normalize(T, unit);
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="time">时间搓</param>
        /// <param name="DateType">获取类型 年 季 月 周 日</param>
        /// <param name="unit">时间搓 单位 纳秒 微秒 毫秒 秒</param>
        public static long GetTimeEndByType(
            in long time,
            in EDateTimeUnit DateType,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            var T = GetDateTime(time, unit);
            switch (DateType)
            {
                case EDateTimeUnit.Day:
                    T = T.AddDays(0);
                    break;
                case EDateTimeUnit.Week:
                    T = T.AddDays(7 - Convert.ToInt16(T.DayOfWeek));
                    break;
                case EDateTimeUnit.Month:
                    T = T.AddDays(DateTime.DaysInMonth(T.Year, T.Month) - T.Day);
                    break;
                case EDateTimeUnit.Season:
                    T = T.AddMonths(2 - ((T.Month - 1) % 3));
                    T = T.AddDays(DateTime.DaysInMonth(T.Year, T.Month) - T.Day);
                    break;
                case EDateTimeUnit.Year:
                    T = T.AddYears(1);
                    T = T.AddDays(-T.DayOfYear);
                    break;
            }

            return Normalize(T.Add(new TimeSpan(23 - T.Hour, 59 - T.Minute, 59 - T.Second)), unit);
        }

        /// <summary>
        /// 获取时间倒计时字符串表示(ms) 01:59:08
        /// </summary>
        public static string GetCountDown(
            in long time,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            var buff = new StringBuilder();
            var Times = GetTimeSpan(time, unit);

            if (Times.Days < 10) buff.Append('0');
            buff.Append(Times.Days).Append('天');

            if (Times.Hours < 10) buff.Append('0');
            buff.Append(Times.Hours).Append('时');

            if (Times.Minutes < 10) buff.Append('0');
            buff.Append(Times.Minutes).Append('分');

            if (Times.Seconds < 10) buff.Append('0');
            buff.Append(Times.Seconds).Append('秒');

            return buff.ToString();
        }

        /// <summary>
        /// 获取传入时间距离当前时间的文字描述
        /// </summary>
        public static string GetPreHumanityTime(
            long time,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            if (time <= 0) return "传入值错误";

            time = (GetCurrTime(unit) - time) / Unit.Time.GetSencondUnit(unit); //单位归一化

            var dayUnit = Unit.Time.GetDayUnit(unit);
            var day = time / dayUnit;
            time %= dayUnit;

            var hourUnit = Unit.Time.GetHourUnit(unit);
            var hour = time / hourUnit;
            time %= hourUnit;

            var minUnit = Unit.Time.GetMinUnit(unit);
            var min = time / minUnit;
            time %= minUnit; //秒

            var buff = new StringBuilder();

            if (day > 0)
                buff.Append(day).Append("天");
            if (day > 0 || hour > 0)
                buff.Append(hour).Append("小时");
            if (day > 0 || hour > 0 || min > 0)
                buff.Append(min).Append("分");
            if (day > 0 || hour > 0 || min > 0 || time > 0)
                buff.Append(time).Append("秒前");

            return buff.ToString();
        }

        #region 时间比较

        /// <summary>
        /// 与当前时间比较 如果小于当前时间为Ture
        /// </summary>
        public static bool CompareNowTime(in DateTime dateTime)
        {
            return DateTime.Now > dateTime;
        }

        /// <summary>
        /// 与当前时间比较 如果小于当前时间为Ture
        /// </summary>
        public static bool CompareNowTime(
            in long time,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            return GetCurrTime(unit) > time;
        }

        #endregion

        /// <summary>
        /// 求离最近发表时间的函数
        /// </summary>
        /// <returns> 返回时间描述 </returns>
        public static string DateStringFromNow(in DateTime dt)
        {
            var span = DateTime.Now - dt;
            if (span.TotalDays > 30) return $"{span.TotalDays % 30}个月前";
            if (span.TotalDays > 7) return $"{span.TotalDays % 7}周前";
            if (span.TotalDays > 1) return $"{(int)Math.Floor(span.TotalDays)}天前";
            if (span.TotalHours > 1) return $"{(int)Math.Floor(span.TotalHours)}小时前";
            if (span.TotalMinutes > 1) return $"{(int)Math.Floor(span.TotalMinutes)}分钟前";
            if (span.TotalSeconds >= 1) return $"{(int)Math.Floor(span.TotalSeconds)}秒前";
            return "1秒前";
        }

        /// <summary>
        /// 日期比较
        /// </summary>
        /// <param name="today">当前日期</param>
        /// <param name="writeDate">输入日期</param>
        /// <param name="n">比较天数</param>
        /// <returns>大于天数返回true，小于返回false</returns>
        public static bool CompareDateDay(
            in string today,
            in string writeDate,
            in int n)
        {
            var dateTime = Convert.ToDateTime(today);
            var WriteDate = Convert.ToDateTime(writeDate).AddDays(n);
            return dateTime < WriteDate;
        }

        /// <summary>
        /// 获取两时间相差
        /// </summary>
        public static string GetDisTime(
            in DateTime dateBegin,
            in DateTime dateEnd,
            in string format = "HH:mm:ss")
        {
            return GetDisTime(dateBegin.Ticks, dateEnd.Ticks, format);
        }

        /// <summary>
        /// 获取两时间相差
        /// </summary>
        public static string GetDisTime(
            in long dateBegin,
            in long dateEnd,
            in string format = "HH:mm:ss")
        {
            return (new TimeSpan(dateEnd) - new TimeSpan(dateBegin)).ToString(format);
        }

        /// <summary>
        /// 判断是否同日
        /// </summary>
        public static bool IsToday(
            in long time1,
            in long time2,
            in ESecondUnit unit = ESecondUnit.MILLISCOND)
        {
            var dt1 = GetDateTime(time1, unit);
            var dt2 = GetDateTime(time2, unit);
            return (dt1.Day == dt2.Day);
        }
    }
}
/*
  format参数格式详细用法:
  1.标准格式代表字符
    格式字符    关联属性/说明
    d         短日期 (样式示例：08/30/2006)
    D         长日期 (样式示例：Wednesday, 30 August 2006)
    f         完整日期和时间（长日期和短时间） (样式示例：Wednesday, 30 August 2006 23:21)
    F         完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 23:22:02)
    g         常规（短日期和短时间） (样式示例：08/30/2006 23:22)
    G         常规（短日期和长时间） (样式示例：08/30/2006 23:23:11)
    m,M       MonthDayPattern
    r,R       RFC1123Pattern
    s         使用当地时间的 SortableDateTimePattern（基于 ISO 8601）
    t         短时间（无秒） (样式示例：23:24
    T         长时间（带秒） (样式示例：23:24:30
    u         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：2006-08-30 23:25:10Z)
    U         使用通用时间的完整日期和时间（长日期和长时间） (样式示例：Wednesday, 30 August 2006 15:25:37)
    y,Y       YearMonthPattern

  2.自定义格式符号
    下表列出了可被合并以构造自定义模式的模式。这些模式是区分大小写的；例如，识别“MM”，但不识别“mm”。
    如果自定义模式包含空白字符或用单引号括起来的字符，则输出字符串页也将包含这些字符。
    未定义为格式模式的一部分或未定义为格式字符的字符按其原义复制。
    格式符号    说明
    d         月中的某一天。一位数的日期没有前导零。
    dd        月中的某一天。一位数的日期有一个前导零。
    ddd       周中某天的缩写名称，在 AbbreviatedDayNames 中定义。
    dddd      周中某天的完整名称，在 DayNames 中定义。
    M         月份数字。一位数的月份没有前导零。
    MM        月份数字。一位数的月份有一个前导零。
    MMM       月份的缩写名称，在 AbbreviatedMonthNames 中定义。
    MMMM      月份的完整名称，在 MonthNames 中定义。
    y         不含纪元的年份，且无前导零。（例如：2008年，纪元为20，非纪元年份为08，显示8）
    yy        不含纪元的年份。且有前导零。（例如：2008年，纪元为20，非纪元年份为08，显示08）
    yyyy      包含纪元的四位数年份。（例如：2008年，纪元为20，非纪元年份为08，显示2008）
    gg        时期或纪元。如果要设置格式的日期不具有关联的时期或纪元字符串，则忽略该模式。
    h         12 小时制的小时。一位数的小时数无前导零。
    hh        12 小时制的小时。一位数的小时数有前导零。
    H         24 小时制的小时。一位数的小时数无前导零。
    HH        24 小时制的小时。一位数的小时数有前导零。
    m         分钟。一位数的分钟数无前导零。
    mm        分钟。一位数的分钟数有前导零。
    s         秒。一位数的秒数无前导零。
    ss        秒。一位数的秒数有前导零。
    f         秒的小数精度为一位。其余数字被截断。
    ff        秒的小数精度为两位。其余数字被截断。
    fff       秒的小数精度为三位。其余数字被截断。
    ffff      秒的小数精度为四位。其余数字被截断。
    fffff     秒的小数精度为五位。其余数字被截断。
    ffffff    秒的小数精度为六位。其余数字被截断。
    fffffff   秒的小数精度为七位。其余数字被截断。
    t         在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项的第一个字符（如果存在）。
    tt        在 AMDesignator 或 PMDesignator 中定义的 AM/PM 指示项（如果存在）。
    z         时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数无前导零。例如，太平洋标准时间是“-8”。
    zz        时区偏移量（“+”或“-”后面仅跟小时）。一位数的小时数有前导零。例如，太平洋标准时间是“-08”。
    zzz       时区偏移量（“+”或“-”后面跟有小时和分钟）。一位数的小时数和分钟数有前导零。例如，太平洋标准时间是“-08:00”。
    :         在 TimeSeparator 中定义的默认时间分隔符。
    /         在 DateSeparator 中定义的默认日期分隔符。

  3.特殊情况注意：
    只有2中列出的格式符号才能用于创建自定义模式；在1中列出的标准格式字符不能用于创建自定义模式。
    1中定义的标准格式简称使用时都是一个字符，2中自定义模式使用时至少需要两个字符；
    若使用自定义格式符号为一个字符的为避免和标准格式简称冲突，需要在前面加上“%”
    例如:
    format(time,"d");       此时的“d”是标准格式简称，返回1中定义的短日期模式。
    format(time,"%d");      此时的“d”是自定义符号，“%”指定自定义模式，返回月中的某天。
    format(time,"d ");      此时的“d”是自定义符号，字符数大于等于2，返回后面跟有一个空白字符的月中的某天。
    2中的格式符号可以随意组合
    例如:
    format(time,"yyyy年MM月")
    format(time,"yyyy/MM/dd HH:mm:ss")
*/

/*
    TimeSpan 结构  表示一个时间间隔。

    命名空间:System 程序集:mscorlib（在 mscorlib.dll 中）

    说明：
    1.DateTime值类型代表了一个从公元0001年1月1日0点0分0秒到公元9999年12月31日23点59分59秒之间的具体日期时刻。因此，
    你可以用DateTime值类型来描述任何在想象范围之内的时间。TimeSpan值包含了许多属性与方法，用于访问或处理一个TimeSpan值，

    其中的五个重载方法之一的结构 TimeSpan( int days, int hours, int minutes, int seconds )

    下面的列表涵盖了其中的一部分方法及属性解释

    Add：与另一个TimeSpan值相加。

    Days:返回用天数计算的TimeSpan值。

    Duration:获取TimeSpan的绝对值。

    Hours:返回用小时计算的TimeSpan值

    Milliseconds:返回用毫秒计算的TimeSpan值。

    Minutes:返回用分钟计算的TimeSpan值。

    Negate:返回当前实例的相反数。

    Seconds:返回用秒计算的TimeSpan值。

    Subtract:从中减去另一个TimeSpan值。

    Ticks:返回TimeSpan值的tick数。

    TotalDays:返回TimeSpan值表示的天数。

    TotalHours:返回TimeSpan值表示的小时数。

    TotalMilliseconds:返回TimeSpan值表示的毫秒数。

    TotalMinutes:返回TimeSpan值表示的分钟数。

    TotalSeconds:返回TimeSpan值表示的秒数。

    负数

    上面是较晚的日期减较早的日期，所以各属性值为正数，如果是较早的日期减较晚的日期，则属性值为负数。

    ASP.NET 中，两个时间相减，得到一个 TimeSpan 实例，TimeSpan 有一些属性：Days、TotalDays、Hours、TotalHours、Minutes、TotalMinutes、Seconds、TotalSeconds、Ticks，注意没有 TotalTicks。

    举例说明

    •时间 1 是 2010-1-2 8:43:35；

    时间 2 是 2010-1-12 8:43:34。

    用时间 2 减时间 1，得到一个 TimeSpan 实例。

    那么时间 2 比时间 1 多 9 天 23 小时 59 分 59 秒。

    那么，Days 就是 9，Hours 就是 23，Minutes 就是 59，Seconds 就是 59。

    再来看 Ticks，Tick 是一个计时周期，表示一百纳秒，即一千万分之一秒，那么 Ticks 在这里表示总共相差多少个时间周期，即：9 * 24 * 3600 * 10000000 + 23 * 3600 * 10000000 +59 * 60 * 10000000 + 59 * 10000000 = 8639990000000。3600 是一小时的秒数。

    TotalDays 就是把 Ticks 换算成日数，即：8639990000000 / (10000000 * 24 * 3600) = 9.99998842592593。

    TotalHours 就是把 Ticks 换算成小时数，即：8639990000000 / (10000000 * 3600) = 239.999722222222。

    TotalMinutes 就是把 Ticks 换算成分钟数，即：8639990000000 / (10000000 * 60) = 14399.9833333333。

    TotalSeconds 就是把 Ticks 换算成秒数，即：8639990000000 / (10000000) = 863999。

    1. Date数值必须以数字符号"#"括起来。

    2. Date数值中的日期数据可有可无，如果有必须符合格式"m/d/yyyy"。

    3. Date数值中的时间数据可有可无，如果有必须和日期数据通过空格分开，并且时分秒之间以":"分开。

    一．DateTime和TimeSpan的关系和区别：

    DateTime和TimeSpan是Visual Basic .Net中用以处理时间日期类型数据的二个主要的结构，这二者的区别在于，
    DatTime表示一个固定的时间，而TimeSpan表示的是一个时间间隔，
    即一段时间。在下面介绍的程序示例中，TimeSpan就用以当前时间和给定时间之差。

    DateTime结构和TimeSpan结构提供了丰富的方法和属性，

    属性 说明
    Date 获取此实例的日期部分。
    Day 获取此实例所表示的日期为该月中的第几天。
    DayOfWeek 获取此实例所表示的日期是星期几。
    DayOfYear 获取此实例所表示的日期是该年中的第几天。
    Hour 获取此实例所表示日期的小时部分。
    Millisecond 获取此实例所表示日期的毫秒部分。
    Minute 获取此实例所表示日期的分钟部分。
    Month 获取此实例所表示日期的月份部分。
    Now 创建一个DateTime实例，它是此计算机上的当前本地日期和时间。
    Second 获取此实例所表示日期的秒部分。
    TimeOfDay 获取此实例的当天的时间。
    Today 获取当前日期。
    Year 获取此实例所表示日期的年份部分。

    Add 将指定的TimeSpan的值加到此实例的值上。
    AddDays 将指定的天数加到此实例的值上。
    AddHours 将指定的小时数加到此实例的值上。
    AddMilliseconds 将指定的毫秒数加到此实例的值上。
    AddMinutes 将指定的分钟数加到此实例的值上。
    AddMonths 将指定的月份数加到此实例的值上。
    AddSeconds 将指定的秒数加到此实例的值上。
    AddYears 将指定的年份数加到此实例的值上。
    DaysInMonth 返回指定年份中指定月份的天数。
    IsLeapYear 返回指定的年份是否为闰年的指示。
    Parse 将日期和时间的指定字符串表示转换成其等效的DateTime实例。
    Subtract 从此实例中减去指定的时间或持续时间。
    ToLongDateString 将此实例的值转换为其等效的长日期字符串表示形式。
    ToLongTimeString 将此实例的值转换为其等效的长时间字符串表示形式。
    ToShortTimeString 将此实例的值转换为其等效的短时间字符串表示形式。
    ToShortDateString 将此实例的值转换为其等效的短日期字符串表示形式。
 */


//  TimeSpan ToString formmat
//  c: 00:00:00
//  g: 0:00:00
//  G: 0:00:00:00.0000000
//  hh\:mm\:ss: 00:00:00
//  %m' min.': 0 min.