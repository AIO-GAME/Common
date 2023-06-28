namespace AIO.Unity
{
    using System.Collections.Generic;

    using UnityEngine;

    /// <inheritdoc/>
    public interface ISerializationDepender : ISerializationCallbackReceiver
    {
        /// <summary>
        /// 依赖集合
        /// </summary>
        IEnumerable<ISerializationDependency> DeserializationDependencies { get; }

        /// <summary>
        /// 在依赖反序列化之后
        /// </summary>
        void OnAfterDependenciesDeserialized();
    }
}
