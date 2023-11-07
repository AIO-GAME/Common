/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2022-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Runtime.CompilerServices;

namespace AIO
{
    /* 常用元字符
      .                             匹配除换行符以外的任意字符。
      \w                            匹配字母或数字或下划线或汉字。
      \s                            匹配任意的空白符。
      \d                            匹配数字。
      \b                            匹配单词的开始或结束。
      [ck]                          匹配包含括号内元素的字符
      ^                             匹配行的开始。
      $                             匹配行的结束。
      \                             对下一个字符转义。比如$是个特殊的字符。要匹配$的话就得用\$
    |*|                             分支条件，如：x|y匹配 x 或 y。

      反义元字符
      \W                            匹配任意不是字母，数字，下划线，汉字的字符。
      \S                            匹配任意不是空白符的字符。等价于 [^ \f\n\r\t\v]。
      \D                            匹配任意非数字的字符。等价于 [^0-9]。
      \B                            匹配不是单词开头或结束的位置。
      [^CK]                         匹配除了CK以外的任意字符。

      特殊元字符
      \f                            匹配一个换页符。等价于 \x0c 和 \cL。
      \n                            匹配一个换行符。等价于 \x0a 和 \cJ。
      \r                            匹配一个回车符。等价于 \x0d 和 \cM。
      \t                            匹配一个制表符。等价于 \x09 和 \cI。
      \v                            匹配一个垂直制表符。等价于 \x0b 和 \cK。

      限定符
      *                             匹配前面的子表达式零次或多次。
      +                             匹配前面的子表达式一次或多次。
      ?                             匹配前面的子表达式零次或一次。
      {n}                           n 是一个非负整数。匹配确定的 n 次。
      {n,}                          n 是一个非负整数。至少匹配n 次。
      {n,m}                         m 和 n 均为非负整数，其中n <= m。最少匹配 n 次且最多匹配 m 次。

      懒惰限定符
      *?                            重复任意次，但尽可能少重复。如 "acbacb"  正则  "a.*?b" 只会取到第一个"acb" 原本可以全部取到但加了限定符后，只会匹配尽可能少的字符 ，而"acbacb"最少字符的结果就是"acb" 。
      +?                            重复1次或更多次，但尽可能少重复。与上面一样，只是至少要重复1次。
      ??                            重复0次或1次，但尽可能少重复。如 "aaacb" 正则 "a.??b" 只会取到最后的三个字符"acb"。
      {n,m}?                        重复n到m次，但尽可能少重复。如 "aaaaaaaa"  正则 "a{0,m}" 因为最少是0次所以取到结果为空。
      {n,}?                         重复n次以上，但尽可能少重复。如 "aaaaaaa"  正则 "a{1,}" 最少是1次所以取到结果为 "a"。

      捕获分组
      (exp)                         匹配exp,并捕获文本到自动命名的组里。
      (?<name>exp)                  匹配exp,并捕获文本到名称为name的组里。
      (?:exp)                       匹配exp,不捕获匹配的文本，也不给此分组分配组号以下为零宽断言。
      (?=exp)                       匹配exp前面的位置。如 "How are you doing" 正则"(?<txt>.+(?=ing))" 这里取ing前所有的字符，并定义了一个捕获分组名字为 "txt" 而"txt"这个组里的值为"How are you do";
      (?<=exp)                      匹配exp后面的位置。如 "How are you doing" 正则"(?<txt>(?<=How).+)" 这里取"How"之后所有的字符，并定义了一个捕获分组名字为 "txt" 而"txt"这个组里的值为" are you doing";
      (?!exp)                       匹配后面跟的不是exp的位置。如 "123abc" 正则 "\d{3}(?!\d)"匹配3位数字后非数字的结果
      (?<!exp)                      匹配前面不是exp的位置。如 "abc123 " 正则 "(?<![0-9])123" 匹配"123"前面是非数字的结果也可写成"(?!<\d)123"

      常用方法
      IsMatch(String, String)       指示 Regex 构造函数中指定的正则表达式在指定的输入字符串中是否找到了匹配项。
      Match(String, String)         在指定的输入字符串中搜索 Regex 构造函数中指定的正则表达式的第一个匹配项。
      Matches(String, String)       在指定的输入字符串中搜索正则表达式的所有匹配项。
      Replace(String, String)       在指定的输入字符串内，使用指定的替换字符串替换与某个正则表达式模式匹配的所有字符串。
      Split(String, String)         在由 Regex 构造函数指定的正则表达式模式所定义的位置，拆分指定的输入字符串。
    */

    public static partial class ExtendString
    {
        /// <summary>
        /// 是否为单词开始
        /// </summary>
        public static bool IsWordBeginning(this string str, in int index)
        {
            if (string.IsNullOrEmpty(str) || index < 0 || index >= str.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range or string is empty");
            }

            var previous = index > 0 ? str[index - 1] : (char?)null;
            var current = str[index];
            var next = index < str.Length - 1 ? str[index + 1] : (char?)null;

            return previous.IsWordBeginning(current, next);
        }

        /// <summary>
        /// 判断是否为Null
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}