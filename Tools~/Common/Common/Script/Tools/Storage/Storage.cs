using System;

namespace AIO
{
    /// <summary>
    /// 数据存储
    /// </summary>
    public abstract partial class Storage :
        IDisposable,
        IDeserialize,
        ISerialize,
        IReset
    {
        /// <summary>
        /// 字节数据
        /// </summary>
        protected BufferByte Buffer { get; private set; }

        /// <summary>
        /// 数据有效长度 需要调动序列化 Serialize
        /// </summary>
        public byte[] Data => Buffer.ToArray();

        /// <summary>
        /// 数据存储
        /// </summary>
        protected Storage()
        {
            Collection = Pool.AList<IBinData>.New();
            Buffer = new BufferByte();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize()
        {
            OnDeserialize(Buffer);
            foreach (var item in Collection) item.Deserialize(Buffer);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize()
        {
            Buffer.Clear();
            OnSerialize(Buffer);
            foreach (var item in Collection) item.Serialize(Buffer);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务
        /// </summary>
        public void Dispose()
        {
            foreach (var item in Collection) item.Dispose();
            Collection.Free();
            Buffer.Dispose();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="buffer">读取接口 如果长度等于0则说明没有数据</param>
        protected virtual void OnDeserialize(IReadData buffer)
        {
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="buffer">存储接口</param>
        protected virtual void OnSerialize(IWriteData buffer)
        {
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        protected virtual void OnReset()
        {
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        public void Reset()
        {
            Collection.Clear();
            OnReset();
            Serialize();
        }
    }
}