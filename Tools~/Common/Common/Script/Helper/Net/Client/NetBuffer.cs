#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// Dynamic byte buffer
    /// </summary>
    internal class NetBuffer
    {
        /// <summary>
        /// Initialize a new expandable buffer with zero capacity
        /// </summary>
        public NetBuffer()
        {
            Arrays = Array.Empty<byte>();
            Count  = 0;
            Offset = 0;
        }

        /// <summary>
        /// Initialize a new expandable buffer with the given capacity
        /// </summary>
        public NetBuffer(long capacity)
        {
            Arrays = new byte[capacity];
            Count  = 0;
            Offset = 0;
        }

        /// <summary>
        /// Initialize a new expandable buffer with the given data
        /// </summary>
        public NetBuffer(byte[] arrays)
        {
            Arrays = arrays;
            Count  = arrays.Length;
            Offset = 0;
        }

        /// <summary>
        /// Is the buffer empty?
        /// </summary>
        public bool IsEmpty => Arrays == null || Count == 0;

        /// <summary>
        /// Bytes memory buffer
        /// </summary>
        public byte[] Arrays { get; private set; }

        /// <summary>
        /// Bytes memory buffer capacity
        /// </summary>
        public int Capacity => Arrays.Length;

        /// <summary>
        /// Bytes memory buffer size
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Bytes memory buffer offset
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// Buffer indexer operator
        /// </summary>
        public byte this[long index] => Arrays[index];

        #region Memory buffer methods

        /// <summary>
        /// Get a string from the current buffer
        /// </summary>
        public override string ToString()
        {
            return ExtractString(0, Count);
        }

        // Clear the current buffer and its offset
        public void Clear()
        {
            Count  = 0;
            Offset = 0;
        }

        /// <summary>
        /// Extract the string from buffer of the given offset and size
        /// </summary>
        public string ExtractString(long offset, long size)
        {
            Debug.Assert(offset + size <= Count, "Invalid offset & size!");
            if (offset + size > Count)
                throw new ArgumentException("Invalid offset & size!", nameof(offset));

            return Encoding.UTF8.GetString(Arrays, (int)offset, (int)size);
        }

        /// <summary>
        /// Remove the buffer of the given offset and size
        /// </summary>
        public void Remove(int offset, int size)
        {
            Debug.Assert(offset + size <= Count, "Invalid offset & size!");
            if (offset + size > Count)
                throw new ArgumentException("Invalid offset & size!", nameof(offset));

            Array.Copy(Arrays, offset + size, Arrays, offset, Count - size - offset);
            Count -= size;
            if (Offset >= offset + size)
            {
                Offset -= size;
            }
            else if (Offset >= offset)
            {
                Offset -= Offset - offset;
                if (Offset > Count)
                    Offset = Count;
            }
        }

        /// <summary>
        /// Reserve the buffer of the given capacity
        /// </summary>
        public void Reserve(long capacity)
        {
            Debug.Assert(capacity >= 0, "Invalid reserve capacity!");
            if (capacity < 0)
                throw new ArgumentException("Invalid reserve capacity!", nameof(capacity));

            if (capacity > Capacity)
            {
                var data = new byte[Math.Max(capacity, 2 * Capacity)];
                Array.Copy(Arrays, 0, data, 0, Count);
                Arrays = data;
            }
        }

        // Resize the current buffer
        public void Resize(int size)
        {
            Reserve(size);
            Count = size;
            if (Offset > Count)
                Offset = Count;
        }

        // Shift the current buffer offset
        public void Shift(int offset)
        {
            Offset += offset;
        }

        // Unshift the current buffer offset
        public void Unshift(int offset)
        {
            Offset -= offset;
        }

        #endregion

        #region Buffer I/O methods

        /// <summary>
        /// Append the single byte
        /// </summary>
        /// <param name="value">Byte value to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(byte value)
        {
            Reserve(Count + 1);
            Arrays[Count] =  value;
            Count         += 1;
            return 1;
        }

        /// <summary>
        /// Append the given buffer
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(byte[] buffer)
        {
            Reserve(Count + buffer.Length);
            Array.Copy(buffer, 0, Arrays, Count, buffer.Length);
            Count += buffer.Length;
            return buffer.Length;
        }

        /// <summary>
        /// Append the given buffer fragment
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Count of append bytes</returns>
        public long Write(byte[] buffer, int offset, int size)
        {
            Reserve(Count + size);
            Array.Copy(buffer, offset, Arrays, Count, size);
            Count += size;
            return size;
        }

        /// <summary>
        /// Append the given span of bytes
        /// </summary>
        /// <param name="buffer">Buffer to append as a span of bytes</param>
        /// <returns>Count of append bytes</returns>
        public long Write(ICollection<byte> buffer)
        {
            Reserve(Count + buffer.Count);
            buffer.CopyTo(Arrays.ToArray(), Count);
            Count += buffer.Count;
            return buffer.Count;
        }

        /// <summary>
        /// Append the given buffer
        /// </summary>
        /// <param name="netBuffer">Buffer to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(NetBuffer netBuffer)
        {
            return Write(netBuffer.Arrays);
        }

        /// <summary>
        /// Append the given text in UTF-8 encoding
        /// </summary>
        /// <param name="text">Text to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(string text)
        {
            var length = Encoding.UTF8.GetMaxByteCount(text.Length);
            Reserve(Count + length);
            var result = Encoding.UTF8.GetBytes(text, 0, text.Length, Arrays, Count);
            Count += result;
            return result;
        }

        /// <summary>
        /// Append the given text in UTF-8 encoding
        /// </summary>
        /// <param name="text">Text to append as a span of characters</param>
        /// <returns>Count of append bytes</returns>
        public long Write(ICollection<char> text)
        {
            var length = Encoding.UTF8.GetMaxByteCount(text.Count);
            Reserve(Count + length);
            var result = Encoding.UTF8.GetBytes(text.ToArray(), 0, text.Count, Arrays, Count);
            Count += result;
            return result;
        }

        #endregion
    }
}