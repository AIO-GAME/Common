#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 数据序列化
    /// </summary>
    public interface IDefinitionData
        : IDisposable,
          IBinDeserialize,
          IDefinitionSerialize,
          IReset,
          ICloneable { }
}