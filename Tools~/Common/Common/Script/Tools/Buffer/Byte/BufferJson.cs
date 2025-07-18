#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace AIO
{
    /// <summary>
    /// JSON数据缓冲区
    /// </summary>
    public partial class JsonBuffer : BufferByte, IDataJson
    {
        /// <inheritdoc/>
        public JsonBuffer() { }

        /// <inheritdoc/>
        public JsonBuffer(byte[] bytes, int index = 0, int count = 0) : base(bytes, index, count) { }

        /// <inheritdoc/>
        public JsonBuffer(int capacity) : base(capacity) { }

        /// <inheritdoc/>
        public JsonBuffer(string path) : base(path) { }

        #region Dictionary

        /// <inheritdoc />
        void IWriteDataJson.WriteDictionary<TK, TV>(IDictionary<TK, TV> values, bool reverse) { WriteString(AHelper.Json.Serialize(values), reverse); }

        /// <inheritdoc />
        Dictionary<TK, TV> IReadDataJson.ReadDictionary<TK, TV>(bool reverse) { return AHelper.Json.Deserialize<Dictionary<TK, TV>>(ReadString(reverse)); }

        #endregion

        /// <inheritdoc />
        List<T> IReadDataJson.ReadList<T>(bool reverse) { return AHelper.Json.Deserialize<List<T>>(ReadString(reverse)); }

        /// <inheritdoc />
        T[] IReadDataJson.ReadArray<T>(bool reverse) { return AHelper.Json.Deserialize<T[]>(ReadString(reverse)); }

        /// <inheritdoc />
        T IReadDataJson.ReadData<T>(bool reverse) { return AHelper.Json.Deserialize<T>(ReadString(reverse)); }

        /// <inheritdoc />
        void IWriteDataJson.WriteData<T>(T val, bool reverse) { WriteString(AHelper.Json.Serialize(val), reverse); }

        /// <inheritdoc />
        void IWriteDataJson.WriteList<T>(IEnumerable<T> values, bool reverse) { WriteString(AHelper.Json.Serialize(values), reverse); }

        /// <inheritdoc />
        void IWriteDataJson.WriteArray<T>(T[] values, bool reverse) { WriteString(AHelper.Json.Serialize(values), reverse); }

        #region

        /// <inheritdoc />
        void IReadDataJson.ReadData<TK, TV>(out IDictionary<TK, TV> value, bool reverse)
            => value = ((IReadDataJson)this).ReadDictionary<TK, TV>(reverse);

        /// <inheritdoc />
        void IReadDataJson.ReadData<T>(out IEnumerable<T> value, bool reverse)
            => value = ((IReadDataJson)this).ReadList<T>(reverse);

        /// <inheritdoc />
        void IReadDataJson.ReadData<T>(out T[] value, bool reverse)
            => value = ((IReadDataJson)this).ReadArray<T>(reverse);

        /// <inheritdoc />
        void IReadDataJson.ReadData<T>(out T value, bool reverse)
            => value = ((IReadDataJson)this).ReadData<T>(reverse);

        /// <inheritdoc />
        void IWriteDataJson.WriteData<TK, TV>(IDictionary<TK, TV> values, bool reverse)
            => ((IWriteDataJson)this).WriteDictionary(values, reverse);

        /// <inheritdoc />
        void IWriteDataJson.WriteData<T>(IEnumerable<T> values, bool reverse)
            => ((IWriteDataJson)this).WriteList(values, reverse);

        /// <inheritdoc />
        void IWriteDataJson.WriteData<T>(T[] values, bool reverse)
            => ((IWriteDataJson)this).WriteArray(values, reverse);

        #endregion
    }
}