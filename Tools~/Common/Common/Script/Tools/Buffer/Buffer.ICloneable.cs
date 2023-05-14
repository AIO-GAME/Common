using System;

namespace AIO
{
	public partial class Buffer<T> : ICloneable
	{
		/// <inheritdoc/>
		public object Clone()
		{
			var clone = new Buffer<T>();
			clone.Arrays = new T[Arrays.Length];
			Arrays.CopyTo(clone.Arrays, 0);
			clone.ReadIndex = ReadIndex;
			clone.WriteIndex = WriteIndex;
			return clone;
		}
	}
}
