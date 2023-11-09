using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AIO.Net
{
    /// <summary>
    /// Dynamic byte buffer
    /// </summary>
    internal class Buffer
    {
        private byte[] _arrays;
        private int _count;
        private int _offset;

        /// <summary>
        /// Is the buffer empty?
        /// </summary>
        public bool IsEmpty => (_arrays == null) || (_count == 0);

        /// <summary>
        /// Bytes memory buffer
        /// </summary>
        public byte[] Arrays => _arrays;

        /// <summary>
        /// Bytes memory buffer capacity
        /// </summary>
        public int Capacity => _arrays.Length;

        /// <summary>
        /// Bytes memory buffer size
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Bytes memory buffer offset
        /// </summary>
        public int Offset => _offset;

        /// <summary>
        /// Buffer indexer operator
        /// </summary>
        public byte this[long index] => _arrays[index];

        /// <summary>
        /// Initialize a new expandable buffer with zero capacity
        /// </summary>
        public Buffer()
        {
            _arrays = Array.Empty<byte>();
            _count = 0;
            _offset = 0;
        }

        /// <summary>
        /// Initialize a new expandable buffer with the given capacity
        /// </summary>
        public Buffer(long capacity)
        {
            _arrays = new byte[capacity];
            _count = 0;
            _offset = 0;
        }

        /// <summary>
        /// Initialize a new expandable buffer with the given data
        /// </summary>
        public Buffer(byte[] arrays)
        {
            _arrays = arrays;
            _count = arrays.Length;
            _offset = 0;
        }

        #region Memory buffer methods

        /// <summary>
        /// Get a string from the current buffer
        /// </summary>
        public override string ToString()
        {
            return ExtractString(0, _count);
        }

        // Clear the current buffer and its offset
        public void Clear()
        {
            _count = 0;
            _offset = 0;
        }

        /// <summary>
        /// Extract the string from buffer of the given offset and size
        /// </summary>
        public string ExtractString(long offset, long size)
        {
            Debug.Assert(((offset + size) <= Count), "Invalid offset & size!");
            if ((offset + size) > Count)
                throw new ArgumentException("Invalid offset & size!", nameof(offset));

            return Encoding.UTF8.GetString(_arrays, (int)offset, (int)size);
        }

        /// <summary>
        /// Remove the buffer of the given offset and size
        /// </summary>
        public void Remove(int offset, int size)
        {
            Debug.Assert(((offset + size) <= Count), "Invalid offset & size!");
            if ((offset + size) > Count)
                throw new ArgumentException("Invalid offset & size!", nameof(offset));

            Array.Copy(_arrays, offset + size, _arrays, offset, _count - size - offset);
            _count -= size;
            if (_offset >= (offset + size))
                _offset -= size;
            else if (_offset >= offset)
            {
                _offset -= _offset - offset;
                if (_offset > Count)
                    _offset = Count;
            }
        }

        /// <summary>
        /// Reserve the buffer of the given capacity
        /// </summary>
        public void Reserve(long capacity)
        {
            Debug.Assert((capacity >= 0), "Invalid reserve capacity!");
            if (capacity < 0)
                throw new ArgumentException("Invalid reserve capacity!", nameof(capacity));

            if (capacity > Capacity)
            {
                byte[] data = new byte[Math.Max(capacity, 2 * Capacity)];
                Array.Copy(_arrays, 0, data, 0, _count);
                _arrays = data;
            }
        }

        // Resize the current buffer
        public void Resize(int size)
        {
            Reserve(size);
            _count = size;
            if (_offset > _count)
                _offset = _count;
        }

        // Shift the current buffer offset
        public void Shift(int offset)
        {
            _offset += offset;
        }

        // Unshift the current buffer offset
        public void Unshift(int offset)
        {
            _offset -= offset;
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
            Reserve(_count + 1);
            _arrays[_count] = value;
            _count += 1;
            return 1;
        }

        /// <summary>
        /// Append the given buffer
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(byte[] buffer)
        {
            Reserve(_count + buffer.Length);
            Array.Copy(buffer, 0, _arrays, _count, buffer.Length);
            _count += buffer.Length;
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
            Reserve(_count + size);
            Array.Copy(buffer, offset, _arrays, _count, size);
            _count += size;
            return size;
        }

        /// <summary>
        /// Append the given span of bytes
        /// </summary>
        /// <param name="buffer">Buffer to append as a span of bytes</param>
        /// <returns>Count of append bytes</returns>
        public long Write(ICollection<byte> buffer)
        {
            Reserve(_count + buffer.Count);
            buffer.CopyTo(_arrays.ToArray(), (int)_count);
            _count += buffer.Count;
            return buffer.Count;
        }

        /// <summary>
        /// Append the given buffer
        /// </summary>
        /// <param name="buffer">Buffer to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(Buffer buffer) => Write(buffer.Arrays);

        /// <summary>
        /// Append the given text in UTF-8 encoding
        /// </summary>
        /// <param name="text">Text to append</param>
        /// <returns>Count of append bytes</returns>
        public long Write(string text)
        {
            var length = Encoding.UTF8.GetMaxByteCount(text.Length);
            Reserve(_count + length);
            var result = Encoding.UTF8.GetBytes(text, 0, text.Length, _arrays, (int)_count);
            _count += result;
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
            Reserve(_count + length);
            var result = Encoding.UTF8.GetBytes(text.ToArray(), 0, text.Count, _arrays, (int)_count);
            _count += result;
            return result;
        }

        #endregion
    }
}