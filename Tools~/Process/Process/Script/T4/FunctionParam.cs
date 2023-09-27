#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS0109 // 
namespace AIO
{
    public class FunctionParam
    {
        /// <summary>
        /// 参数修饰符
        /// </summary>
        public ParamModifier Modifier { get; set; } = ParamModifier.None;

        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否可选参数
        /// </summary>
        public bool IsParams { get; set; } = false;

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数输出
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 注释描述
        /// </summary>
        public string Comments { get; set; }

        public FunctionParam()
        {
        }

        public FunctionParam(FunctionParam param)
        {
            Type = param.Type;
            Name = param.Name;
            Output = param.Output;
            IsParams = param.IsParams;
            Comments = param.Comments;
        }

        public FunctionParam(string type, string name)
        {
            Type = type;
            Name = name;
            Output = Name;
        }

        public FunctionParam(string type, string name, string output)
        {
            Type = type;
            Name = name;
            Output = output;
        }
    }
}