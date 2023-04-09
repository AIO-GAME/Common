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
        IReset,
        ISave,
        ILoad
    {
        /// <summary>
        /// 保存读取路径
        /// </summary>
        private readonly string Path;

        /// <summary>
        /// 字节数据
        /// </summary>
        private readonly BufferByte Buffer;

        /// <summary>
        /// 数据存储
        /// </summary>
        /// <param name="path">存储读取路径</param>
        protected Storage(in string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
            Path = path;
            Collection = Pool.List<IBinData>.New();
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
            Serialize();
            Save();
            foreach (var item in Collection) item.Dispose();
            Collection.Free();
            Buffer.Dispose();
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

        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save()
        {
            if (Buffer.Count == 0) return;
            Utils.IO.Write(Path, Buffer, 0, Buffer.WriteOffset, false);
        }

        /// <summary>
        /// 加载
        /// </summary>
        public void Load()
        {
            Buffer.Clear();
            if (File.Exists(Path)) Buffer.Write(Utils.IO.Read(Path));
        }
    }
}