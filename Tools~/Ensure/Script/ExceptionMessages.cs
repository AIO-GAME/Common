namespace AIO
{
    /// <summary>
    /// 异常消息
    /// </summary>
    public static class ExceptionMessages
    {
        /// <summary>
        /// 在期望为空值的情况下实际上有一个值分配给它时，显示此错误消息。
        /// </summary>
        public const string Common_IsNull_Failed = "Value must be null.";

        /// <summary>
        /// 在期望具有非空值的情况下实际上为 null 时，显示此错误消息。
        /// </summary>
        public const string Common_IsNotNull_Failed = "Value cannot be null.";

        /// <summary>
        /// 当预期求值为 true 的表达式实际上求值为 false 时，显示此错误消息。
        /// </summary>
        public const string Booleans_IsTrueFailed = "Expected an expression that evaluates to true.";

        /// <summary>
        /// 当预期求值为 false 的表达式实际上求值为 true 时，显示此错误消息。
        /// </summary>
        public const string Booleans_IsFalseFailed = "Expected an expression that evaluates to false.";

        /// <summary>
        /// 当谓词（返回 true/false 值的函数）未匹配集合中的任何元素时，显示此错误消息。
        /// </summary>
        public const string Collections_Any_Failed = "The predicate did not match any elements.";

        /// <summary>
        /// 当在集合中未找到指定的键时，显示此错误消息。
        /// </summary>
        public const string Collections_ContainsKey_Failed = "{1} '{0}' was not found.";

        /// <summary>
        /// 当期望集合至少有一个项目，但为空时，显示此错误消息。
        /// </summary>
        public const string Collections_HasItemsFailed = "Empty collection is not allowed.";

        /// <summary>
        /// 当不允许集合包含任何 null 项，但找到一个或多个 null 项时，显示此错误消息。
        /// </summary>
        public const string Collections_HasNoNullItemFailed = "Collection with null items is not allowed.";

        /// <summary>
        /// 当集合的大小与预期大小不匹配时，显示此错误消息。
        /// </summary>
        public const string Collections_SizeIs_Failed = "Expected size '{0}' but found '{1}'.";

        /// <summary>
        /// 当一个值不等于预期值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是预期值。
        /// </summary>
        public const string Comp_Is_Failed = "Value '{0}' is not '{1}'.";

        /// <summary>
        /// 当一个值等于预期值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是预期值。
        /// </summary>
        public const string Comp_IsNot_Failed = "Value '{0}' is '{1}', which was not expected.";

        /// <summary>
        /// 当一个值大于或等于限制值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是限制值。
        /// </summary>
        public const string Comp_IsNotLt = "Value '{0}' is not lower than limit '{1}'.";

        /// <summary>
        /// 当一个值大于限制值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是限制值。
        /// </summary>
        public const string Comp_IsNotLte = "Value '{0}' is not lower than or equal to limit '{1}'.";

        /// <summary>
        /// 当一个值小于或等于限制值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是限制值。
        /// </summary>
        public const string Comp_IsNotGt = "Value '{0}' is not greater than limit '{1}'.";

        /// <summary>
        /// 当一个值小于限制值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是限制值。
        /// </summary>
        public const string Comp_IsNotGte = "Value '{0}' is not greater than or equal to limit '{1}'.";

        /// <summary>
        /// 当一个值小于最小值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是最小值。
        /// </summary>
        public const string Comp_IsNotInRange_ToLow = "Value '{0}' is < min '{1}'.";

        /// <summary>
        /// 当一个值大于最大值时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际值，第二个参数是最大值。
        /// </summary>
        public const string Comp_IsNotInRange_ToHigh = "Value '{0}' is > max '{1}'.";

        /// <summary>
        /// 不允许使用空 GUID。
        /// </summary>
        public const string Guids_IsNotEmpty_Failed = "An empty GUID is not allowed.";

        /// <summary>
        /// 当一个字符串不等于预期字符串时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际字符串，第二个参数是预期字符串。
        /// </summary>
        public const string Strings_IsEqualTo_Failed = "Value '{0}' is not '{1}'.";

        /// <summary>
        /// 当一个字符串等于预期字符串时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际字符串，第二个参数是预期字符串。
        /// </summary>
        public const string Strings_IsNotEqualTo_Failed = "Value '{0}' is '{1}', which was not expected.";

        /// <summary>
        /// 当字符串长度与预期长度不匹配时，将会显示此错误消息。该消息包含两个参数：第一个参数是预期长度，第二个参数是实际长度。
        /// </summary>
        public const string Strings_SizeIs_Failed = "Expected length '{0}' but got '{1}'.";

        /// <summary>
        /// 不允许字符串为空、 null 或仅由空格组成。
        /// </summary>
        public const string Strings_IsNotNullOrWhiteSpace_Failed = "The string can't be left empty, null or consist of only whitespaces.";

        /// <summary>
        /// 不允许字符串为 null 或为空。
        /// </summary>
        public const string Strings_IsNotNullOrEmpty_Failed = "The string can't be null or empty.";

        /// <summary>
        /// 字符串长度不够，必须在 {0} 和 {1} 之间，但实际长度为 {2}。
        /// </summary>
        public const string Strings_HasLengthBetween_Failed_ToShort = "The string is not long enough. Must be between '{0}' and '{1}' but was '{2}' characters long.";

        /// <summary>
        /// 字符串过长，必须在 {0} 和 {1} 之间，但实际长度为 {2}。
        /// </summary>
        public const string Strings_HasLengthBetween_Failed_ToLong = "The string is too long. Must be between '{0}' and  '{1}'. Must be between '{0}' and '{1}' but was '{2}' characters long.";

        /// <summary>
        /// 当一个字符串不匹配预期的正则表达式时，将会显示此错误消息。该消息包含两个参数：第一个参数是实际字符串，第二个参数是预期正则表达式。
        /// </summary>
        public const string Strings_Matches_Failed = "Value '{0}' does not match '{1}'";

        /// <summary>
        /// 不允许使用空字符串。
        /// </summary>
        public const string Strings_IsNotEmpty_Failed = "Empty String is not allowed.";

        /// <summary>
        /// 当一个字符串不是有效的 GUID 时，将会显示此错误消息。该消息包含一个参数：字符串。
        /// </summary>
        public const string Strings_IsGuid_Failed = "Value '{0}' is not a valid GUID.";

        /// <summary>
        /// 当期望的类型与实际类型不匹配时，将会显示此错误消息。该消息包含两个参数：第一个参数是期望类型，第二个参数是实际类型。
        /// </summary>
        public const string Types_IsOfType_Failed = "Expected a '{0}' but got '{1}'.";

        /// <summary>
        /// 当类型未定义指定的属性时，将会显示此错误消息。该消息包含两个参数：第一个参数是类型名称，第二个参数是属性名称。
        /// </summary>
        public const string Reflection_HasAttribute_Failed = "Type '{0}' does not define the [{1}] attribute.";

        /// <summary>
        /// 当类型没有接受指定参数的构造函数时，将会显示此错误消息。该消息包含两个参数：第一个参数是类型名称，第二个参数是参数列表。
        /// </summary>
        public const string Reflection_HasConstructor_Failed = "Type '{0}' does not provide a constructor accepting ({1}).";

        /// <summary>
        /// 当类型没有公共接受指定参数的构造函数时，将会显示此错误消息。该消息包含两个参数：第一个参数是类型名称，第二个参数是参数列表。
        /// </summary>
        public const string Reflection_HasPublicConstructor_Failed = "Type '{0}' does not provide a public constructor accepting ({1}).";

        /// <summary>
        /// 参数期望值不是默认值。
        /// </summary>
        public const string ValueTypes_IsNotDefault_Failed = "The param was expected to not be of default value.";
    }
}