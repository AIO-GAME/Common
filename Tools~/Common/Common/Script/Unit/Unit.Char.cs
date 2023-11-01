/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/

namespace AIO
{
    /// <summary>
    /// 单位
    /// </summary>
    public static partial class Unit
    {
        /// <summary>
        /// 只读变量
        /// </summary>
        public static class Char
        {
            /// <summary>
            /// 0的字符
            /// </summary>
            public const int C0 = 0;

            /// <summary>
            /// 响铃
            /// </summary>
            public const int BEL = 7;

            /// <summary>
            /// 回格
            /// </summary>
            public const int BackSpace = 8;

            /// <summary>
            /// tab(水平制表符)
            /// </summary>
            public const int TabH = 9;

            /// <summary>
            /// 换行
            /// </summary>
            public const int NewLine = 10;

            /// <summary>
            /// tab(垂直制表符)
            /// </summary>
            public const int TabV = 11;

            /// <summary>
            /// 换页
            /// </summary>
            public const int Page = 12;

            /// <summary>
            /// 回车 chr(13) &amp; chr(10) 回车和换行的组合
            /// </summary>
            public const int EnterNewLine = 13;

            /// <summary>
            /// 结束 End
            /// </summary>
            public const int End = 26;

            /// <summary>
            /// 脱离 Pause Break
            /// </summary>
            public const int PauseBreak = 27;

            /// <summary>
            /// 空格
            /// </summary>
            public const int Space = 32;

            /// <summary>
            /// !
            /// </summary>
            public const int Exclamation = 33;

            /// <summary>
            /// "
            /// </summary>
            public const int DoubleQuote = 34;

            /// <summary>
            /// #
            /// </summary>
            public const int Pound = 35;

            /// <summary>
            /// $
            /// </summary>
            public const int USD = 36;

            /// <summary>
            /// -%
            /// </summary>
            public const int Modulus = 37;

            /// <summary>
            /// -&amp;
            /// </summary>
            public const int AND = 38;

            /// <summary>
            /// -’
            /// </summary>
            public const int SingleQuote = 39;

            /// <summary>
            /// -(
            /// </summary>
            public const int LeftBracket = 40;

            /// <summary>
            /// -)
            /// </summary>
            public const int RightBracket = 41;

            /// <summary>
            /// *
            /// </summary>
            public const int Asterisk = 42;

            /// <summary>
            /// +
            /// </summary>
            public const int ADD = 43;

            /// <summary>
            /// ,
            /// </summary>
            public const int Comma = 44;

            /// <summary>
            /// -
            /// </summary>
            public const int SUB = 45;

            /// <summary>
            /// .
            /// </summary>
            public const int Point = 46;

            /// <summary>
            /// /
            /// </summary>
            public const int Slash = 47;

            /// <summary>
            /// 0
            /// </summary>
            public const int N0 = 48;

            /// <summary>
            /// 1
            /// </summary>
            public const int N1 = 49;

            /// <summary>
            /// 2
            /// </summary>
            public const int N2 = 50;

            /// <summary>
            /// 3
            /// </summary>
            public const int N3 = 51;

            /// <summary>
            /// 4
            /// </summary>
            public const int N4 = 52;

            /// <summary>
            /// 5
            /// </summary>
            public const int N5 = 53;

            /// <summary>
            /// 6
            /// </summary>
            public const int N6 = 54;

            /// <summary>
            /// 7
            /// </summary>
            public const int N7 = 55;

            /// <summary>
            /// 8
            /// </summary>
            public const int N8 = 56;

            /// <summary>
            /// 9
            /// </summary>
            public const int N9 = 57;

            /// <summary>
            /// 数字
            /// </summary>
            public static int[] Number = new int[] { N0, N1, N2, N3, N4, N5, N6, N7, N8, N9 };

            /// <summary>
            /// :
            /// </summary>
            public const int Colon = 58;

            /// <summary>
            /// ;
            /// </summary>
            public const int Semicolon = 59;

            /// <summary>
            /// &lt;
            /// </summary>
            public const int LessThanSign = 60;

            /// <summary>
            /// 61 =
            /// </summary>
            public const int Equality = 61;

            /// 62 >　
            /// 63 ?
            /// 64 @
            /// <summary>
            /// 65 A
            /// </summary>
            public const int EA = 65;

            /// 66 B
            /// 67 C
            /// 68 D
            /// 69 E
            /// 70 F
            /// 71 G
            /// 72 H
            /// 73 I
            /// 74 J
            /// 75 K
            /// 76 L
            /// 77 M
            /// 78 N
            /// 79 O
            /// 80 P
            /// 81 Q
            /// 82 R
            /// 83 S
            /// 84 T
            /// 85 U
            /// 86 V
            /// 87 W
            /// 88 X
            /// 89 Y
            /// <summary>
            /// 90 Z
            /// </summary>
            public const int EZ = 90;

            /// <summary>
            /// 大写A-大写Z
            /// </summary>
            public static int[] EAlphaBet = new int[] { EA, EZ };

            /// 91 [
            /// 92 /　
            /// 92 /
            /// 93]
            /// 94 ^
            /// <summary>
            /// 95 _
            /// </summary>
            public const int UnderLine = 95;

            /// 96 `
            /// <summary>
            /// 97 a
            /// </summary>
            public const int ea = 97;

            /// 98 b
            /// 99 c
            /// 100 d
            /// 101 e
            /// 102 f
            /// 103 g
            /// 104 h
            /// 105 i
            /// 106 j
            /// 107 k
            /// 108 l
            /// 109 m
            /// 110 n
            /// 111 o
            /// 112 p
            /// 113 q
            /// 114 r
            /// 115 s
            /// 116 t
            /// 117 u
            /// 118 v
            /// 119 w
            /// 120 x
            /// 121 y
            /// <summary>
            /// 122 z
            /// </summary>
            public const int ez = 122;

            /// <summary>
            /// 小写e-小写z
            /// </summary>
            public static int[] eAlphaBet = new int[] { ea, ez };
        }
    }
}