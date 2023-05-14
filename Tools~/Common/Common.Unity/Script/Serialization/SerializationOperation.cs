using UnityObject = UnityEngine.Object;

namespace AIO.Unity
{
    using System.Collections.Generic;

    /// <summary>
    /// 序列化可选参数
    /// </summary>
    public class SerializationOperation
    {
        /// <summary>
        /// 序列化可选参数
        /// </summary>
        public SerializationOperation()
        {
            objectReferences = new List<UnityObject>();
            serializer = new fsSerializer();
            serializer.AddConverter(new UnityObjectConverter());
            serializer.AddConverter(new RayConverter());
            serializer.AddConverter(new Ray2DConverter());
            serializer.AddConverter(new NamespaceConverter());
            serializer.AddConverter(new LooseAssemblyNameConverter());
            serializer.Context.Set(objectReferences);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public fsSerializer serializer { get; private set; }

        /// <summary>
        /// 引用对象列表
        /// </summary>
        public List<UnityObject> objectReferences { get; private set; }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            objectReferences.Clear();
        }
    }
}
