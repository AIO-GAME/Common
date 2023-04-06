/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


namespace AIO
{
    using System;

    ///<summary>
    /// 字符缓存
    /// 建议使用 StringBuilder
    ///</summary>
    public class BufferChar
    {
        ///<summary> 容量 </summary>
        public const int CAPACITY = 32;

        ///<summary> 空 </summary>
        public const string NULL = "null";

        ///<summary> 字符数组 </summary>
        private char[] array;

        ///<summary> 首位:下一个写入位置 </summary>
        private int top;

        ///<summary> 游标:下一个读取位置 </summary>
        private int offset;

        ///<summary> 构建一个默认的字符缓冲对象 </summary>
        public BufferChar() : this(CAPACITY) { }

        ///<summary> 构建一个指定容量的字符缓冲对象 </summary>
        public BufferChar(int capacity)
        {
            if (capacity < 1) throw new SystemException(typeof(BufferChar).Name + " <init>, invalid capatity:" + capacity);
            this.array = new char[capacity];
            this.top = 0;
            this.offset = 0;
        }

        ///<summary> 通过一个字符数组构建一个字符缓冲对象 </summary>
        public BufferChar(char[] chars)
        {
            if (chars == null)
                throw new SystemException(typeof(BufferChar).Name + " <init>, null data");
            this.array = chars;
            this.top = chars.Length;
            this.offset = 0;
        }

        ///<summary> 通过一个字符数组，offset开始数量为len的区域构建一个字符缓冲 </summary>
        public BufferChar(char[] chars, int offset, int len)
        {
            if (chars == null)
                throw new SystemException(typeof(BufferChar).Name + " <init>, null data");
            if ((offset < 0) || (offset > chars.Length))
                throw new SystemException(typeof(BufferChar).Name + " <init>, invalid index:" + offset);
            if ((len < 0) || (chars.Length < offset + len))
                throw new SystemException(typeof(BufferChar).Name + " <init>, invalid length:" + len);
            this.array = chars;
            this.top = (offset + len);
            this.offset = offset;
        }

        ///<summary> 通过一个字符串构建一个字符缓冲对象 </summary>
        public BufferChar(string str)
        {
            if (str == null)
                throw new SystemException(typeof(BufferChar).Name + " <init>, null str");
            int i = str.Length;
            this.array = new char[i + CAPACITY];
            str.CopyTo(0, this.array, 0, i);
            //str.GetChars(0,i,this.array,0);
            this.top = i;
            this.offset = 0;
        }

        ///<summary> 容量 </summary>
        public int Capacity => array.Length;

        ///<summary> 设置容量 </summary>
        protected virtual void SetCapacity(int value)
        {
            int i = this.array.Length;
            if (value <= i) return;
            for (; i < value; i = (i << 1) + 1)
                ;
            char[] chars = new char[i];
            System.Array.Copy(this.array, 0, chars, 0, this.top);
            this.array = chars;
        }

        ///<summary> 首位 </summary>
        public virtual int Top
        {
            get => top;
            set
            {
                if (value < offset) throw new SystemException(typeof(BufferChar).Name + " setTop, invalid top:" + value);
                if (value > array.Length) SetCapacity(value);
                top = value;
            }
        }

        ///<summary> 游标 </summary>
        public virtual int Offset
        {
            get => offset;
            set
            {
                if ((value < 0) || (value > top))
                    throw new SystemException(typeof(BufferChar).Name + " setOffset, invalid offset:" + value);
                offset = value;
            }
        }

        ///<summary> 长度 </summary>
        public virtual int Length => top - offset;

        ///<summary> 获取字符数组 </summary>
        public virtual char[] Array => array;

        ///<summary> 读取一个字符 </summary>
        public char Read()
        {
            return this.array[this.offset++];
        }

        ///<summary> 读取指定下标字符 </summary>
        public char Read(int index)
        {
            return this.array[index];
        }

        ///<summary> 写入一个字符 </summary>
        public void Write(char ch)
        {
            this.array[this.top++] = ch;
        }

        ///<summary> 将字符写到指定下标 </summary>
        public void Write(char ch, int index)
        {
            this.array[index] = ch;
        }

        ///<summary> 读取len个字符到数组chars中，从下标index开始 </summary>
        public void Read(char[] chars, int index, int len)
        {
            System.Array.Copy(this.array, this.offset, chars, index, len);
            this.offset += len;
        }

        ///<summary> 写入一个字符数组，冲下标offset开始，数量len </summary>
        public void Write(char[] chars, int offset, int len)
        {
            if (this.array.Length < this.top + len) this.SetCapacity(this.top + len);
            System.Array.Copy(chars, offset, this.array, this.top, len);
            this.top += len;
        }

        ///<summary> 附加一个对象 </summary>
        public BufferChar Append(object obj)
        {
            return Append((obj != null) ? obj.ToString() : null);
        }

        ///<summary> 附加一个字符串 </summary>
        public BufferChar Append(string str)
        {
            if (str == null) str = "null";
            int len = str.Length;
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.SetCapacity(this.top + len);
            str.CopyTo(0, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary> 附加一个字符串,从字符串start索引开始添加len个字符 </summary>
        public BufferChar Append(string str, int start, int len)
        {
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.SetCapacity(this.top + len);
            str.CopyTo(start, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary> 附加一个字符串,从字符串start索引开始添加len个字符 </summary>
        public BufferChar Append(string str, int start)
        {
            int len = str.Length - start;
            if (len <= 0) return this;
            if (this.array.Length < this.top + len) this.SetCapacity(this.top + len);
            str.CopyTo(start, this.array, this.top, len);
            this.top += len;
            return this;
        }

        ///<summary> 附加一组字符 </summary>
        public BufferChar Append(char[] chars)
        {
            if (chars == null) return this.Append("null");
            return Append(chars, 0, chars.Length);
        }

        ///<summary> 附加一组字符，冲指定下标offset开始，附加len数量个字符 </summary>
        public BufferChar Append(char[] chars, int offset, int len)
        {
            if (chars == null) return this.Append("null");
            this.Write(chars, offset, len);
            return this;
        }

        ///<summary> 附加一个boolean </summary>
        public BufferChar Append(bool bo)
        {
            int i = this.top;
            if (bo)
            {
                if (this.array.Length < i + 4) this.SetCapacity(i + CAPACITY);
                this.array[i] = 't';
                this.array[i + 1] = 'r';
                this.array[i + 2] = 'u';
                this.array[i + 3] = 'e';
                this.top += 4;
            }
            else
            {
                if (this.array.Length < i + 5) this.SetCapacity(i + CAPACITY);
                this.array[i] = 'f';
                this.array[i + 1] = 'a';
                this.array[i + 2] = 'l';
                this.array[i + 3] = 's';
                this.array[i + 4] = 'e';
                this.top += 5;
            }
            return this;
        }

        ///<summary> 附加一个char </summary>
        public BufferChar Append(char chr)
        {
            if (this.array.Length < this.top + 1) this.SetCapacity(this.top + CAPACITY);
            this.array[this.top++] = chr;
            return this;
        }

        ///<summary> 附加一个int </summary>
        public BufferChar Append(int number)
        {
            if (number == int.MinValue)
            {
                // 这个转正数超过最大值，所以要单独处理
                Append("-2147483648");
                return this;
            }
            int i = this.top, j = 0, k = 0, l;
            if (number < 0)
            {
                number = -number;
                for (l = number; (l /= 10) > 0; ++k)
                    ;
                j = k + 2;
                if (this.array.Length < i + j) this.SetCapacity(i + j);
                this.array[i++] = '-';
            }
            else
            {
                for (l = number; (l /= 10) > 0; ++k)
                    ;
                j = k + 1;
                if (this.array.Length < i + j) this.SetCapacity(i + j);
            }
            while (k >= 0)
            {
                this.array[(i + k)] = (char)('0' + number % 10);
                number /= 10;
                --k;
            }
            this.top += j;
            return this;
        }

        ///<summary> 附加一个long </summary>
        public BufferChar Append(long number)
        {
            if (number == long.MinValue)
            {
                Append("-9223372036854775808");
                return this;
            }
            int i = this.top, j = 0, k = 0;
            long l;
            if (number < 0L)
            {
                number = -number;
                for (l = number; (l /= 10L) > 0L; ++k)
                    ;
                j = k + 2;
                if (this.array.Length < i + j) this.SetCapacity(i + j);
                this.array[i++] = '-';
            }
            else
            {
                for (l = number; (l /= 10L) > 0L; ++k)
                    ;
                j = k + 1;
                if (this.array.Length < i + j) this.SetCapacity(i + j);
            }
            while (k >= 0)
            {
                this.array[(i + k)] = (char)(int)('0' + number % 10L);
                number /= 10L;
                --k;
            }
            this.top += j;
            return this;
        }

        ///<summary> 附加一个float </summary>
        public BufferChar Append(float number)
        {
            return Append(number.ToString());
        }

        ///<summary> 附加一个double </summary>
        public BufferChar Append(double number)
        {
            return Append(number.ToString());
        }

        ///<summary> 转换为字符数组 </summary>
        public char[] ToArray()
        {
            char[] chars = new char[this.top - this.offset];
            System.Array.Copy(this.array, this.offset, chars, 0, chars.Length);
            return chars;
        }

        ///<summary> 清除 </summary>
        public void Clear()
        {
            this.top = 0;
            this.offset = 0;
        }

        ///<summary> 获取字符串 </summary>
        public string GetString()
        {
            return new String(this.array, this.offset, this.top - this.offset);
        }

        ///<summary> 获取字符串 </summary>
        public string GetString(int sindex, int len)
        {
            if (len > this.top - this.offset) len = this.top - this.offset;
            return new String(this.array, this.offset + sindex, len);
        }

        ///<summary> hash码 </summary>
        public override int GetHashCode()
        {
            int code = 0;
            for (int i = this.offset; i < this.top; ++i)
                code = 31 * code + this.array[i];
            return code;
        }

        ///<summary> 比对方法 </summary>
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is BufferChar)) return false;
            BufferChar charBuffer = (BufferChar)obj;
            if (charBuffer.top != top) return false;
            if (charBuffer.offset != offset) return false;
            for (int i = top - 1; i >= 0; --i)
            {
                if (charBuffer.array[i] != array[i]) return false;
            }
            return true;
        }

        ///<summary> 信息 </summary>
        public override string ToString()
        {
            return string.Concat(base.ToString(), '[', top, ',', offset, ',', array.Length, ']');
        }
    }
}
