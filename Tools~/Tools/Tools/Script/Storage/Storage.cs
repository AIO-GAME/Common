using System;
using System.IO;

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
        private readonly string Path;

        /// <summary>
        /// 数据存储
        /// </summary>
        /// <param name="path">存储读取路径</param>
        protected Storage(in string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            Path = path;
            Collection = Pool.List<IBinData>.New();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        public void Deserialize()
        {
            var buffer = !File.Exists(Path)
                ? new BufferByte()
                : new BufferByte(Utils.IO.Read(Path));
            OnDeserialize(buffer);
            foreach (var item in Collection) item.Deserialize(buffer);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        public void Serialize()
        {
            var buffer = new BufferByte();
            OnSerialize(buffer);
            foreach (var item in Collection) item.Serialize(buffer);
            if (buffer.Count == 0) return;
            Utils.IO.Write(Path, buffer, 0, buffer.WriteOffset, false);
        }

        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务
        /// </summary>
        public void Dispose()
        {
            Serialize();
            foreach (var item in Collection) item.Dispose();
            Collection.Free();
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="buffer">读取接口 如果长度等于0则说明没有数据</param>
        protected virtual void OnDeserialize(IReadIData buffer)
        {
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="buffer">存储接口</param>
        protected virtual void OnSerialize(IWriteIData buffer)
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