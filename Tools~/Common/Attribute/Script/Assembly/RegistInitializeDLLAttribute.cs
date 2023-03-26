namespace AIO
{
    using System;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// 注册 DLL
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class RegistInitializeDLLAttribute : Attribute
    {
        internal const BindingFlags FLAGS = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        private Type typeName { get; }

        private string methodName { get; }

        private BindingFlags flagsValue { get; }

        /// <summary>
        /// 注册DLL
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="name">名称</param>
        public RegistInitializeDLLAttribute(Type type, string name)
        {
            typeName = type;
            methodName = name;
            flagsValue = FLAGS;
        }

        /// <summary>
        /// 实现
        /// </summary>
        public void Invoke()
        {
            var method = typeName.GetMethod(methodName, flagsValue);
            if (method == null)
            {
                Console.WriteLine(" {0} -> {1} {2} = null", typeName.FullName, methodName, flagsValue);
            }
            else method.Invoke(null, null);
        }
    }
}