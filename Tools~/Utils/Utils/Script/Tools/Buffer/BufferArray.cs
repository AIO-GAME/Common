﻿/*|============================================|*|
|*|Author:        |*|XiNan                     |*|
|*|Date:          |*|2020-05-10                |*|
|*|E-Mail:        |*|1398581458@qq.com         |*|
|*|=============================================*/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AIO
{
    /// <summary>
    /// 接口方法
    /// </summary>
    public partial class BufferArray<T> :
        IDisposable,
        ICloneable,
        IReadOnlyList<T>,
        IList<T>
    {
        /// <summary>
        /// 清除 当前清空中重置标识下标 不重写数据
        /// </summary>
        public void Clear()
        {
            Offset = 0;
        }

        /// <summary>
        /// 转化为数组
        /// </summary>
        public T[] ToArray()
        {
            T[] Temp = new T[Offset];
            System.Array.Copy(Array, 0, Temp, 0, Offset);
            return Temp;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Array = null;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        public void CopyTo(T[] array, int index)
        {
            if (index > Offset || index < 0) return;
            int Len = array.Length > Offset ? Offset : array.Length;
            System.Array.Copy(Array, index, array, 0, Len);
        }

        /// <summary>
        /// 克隆
        /// </summary>
        public object Clone()
        {
            return new BufferArray<T>(ToArray(), Capacity);
        }

        /// <summary>
        /// 迭代
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Offset; i++)
                yield return Array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Offset; i++)
                yield return Array[i];
        }

        /// <summary>
        /// 输出
        /// </summary>
        public override string ToString()
        {
            var builder = new StringBuilder().Append('[');
            for (int i = 0; i < Offset; i++)
                builder.Append(Array[i].ToString()).Append(',');
            return builder.Append(']').ToString().Replace(",]", "]");
        }

        /// <summary>
        /// 反转
        /// </summary>
        public void Reverse()
        {
            int right;
            T t;
            for (int i = 0; i < Math.Ceiling(Offset / 2f); i++)
            {
                right = Offset - 1 - i;
                t = Array[i];
                Array[i] = Array[right];
                Array[right] = t;
            }
        }

        /// <summary>
        /// 判断是否有相同元素
        /// </summary>
        public bool Contains(T Obj)
        {
            return (IndexOf(Obj, 0) >= 0);
        }

        /// <summary>
        /// 交换数组元素位置
        /// </summary>
        public void Swap(int A, int B)
        {
            T temp = Array[A];
            Array[A] = Array[B];
            Array[B] = temp;
        }
    }

    /// <summary>
    /// 属性 私有方法
    /// </summary>
    public partial class BufferArray<T>
    {
        /// <summary>
        /// 容量
        /// </summary>
        protected const int CAPACITY = 32;

        /// <summary>
        /// 当前数组
        /// </summary>
        protected T[] Array { get; set; }

        /// <summary>
        /// 数据写入下标
        /// </summary>
        public int Offset { get; protected set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 当前容器数量
        /// </summary>
        public int Count
        {
            get { return Offset; }
        }

        public bool IsReadOnly { get; private set; }

        public T this[int index]
        {
            get => Get(index);
            set => Set(value, index);
        }

        public T this[long index]
        {
            get => Get(index);
            set => Set(value, index);
        }

        public BufferArray(int Capacity)
        {
            this.Capacity = Capacity;
            Array = new T[Capacity];
        }

        public BufferArray() : this(CAPACITY)
        {
        }

        public BufferArray(T[] array, int Capacity = CAPACITY) : this(Capacity)
        {
            Add(array);
        }

        ~BufferArray()
        {
            Array = null;
            Offset = Capacity;
            IsReadOnly = false;
        }

        /// <summary>
        /// 扩大容量
        /// </summary>
        private void UpStepCapacity()
        {
            Capacity <<= 1;
            T[] Temp = new T[Capacity];
            System.Array.Copy(Array, 0, Temp, 0, Array.Length);
            Array = Temp;
        }

        /// <summary>
        /// 修改元素
        /// </summary>
        private void Set(T Obj, int Index)
        {
            if (Index > Offset || Index < 0)
                throw new IndexOutOfRangeException();
            else Array[Index] = Obj;
        }

        /// <summary>
        /// 获取元素
        /// </summary>
        private T Get(int Index)
        {
            if (Index > Offset || Index < 0)
                throw new IndexOutOfRangeException();
            else return Array[Index];
        }

        /// <summary>
        /// 修改元素
        /// </summary>
        private void Set(T Obj, long Index)
        {
            if (Index > Offset || Index < 0)
                throw new IndexOutOfRangeException();
            else Array[Index] = Obj;
        }

        /// <summary>
        /// 获取元素
        /// </summary>
        private T Get(long Index)
        {
            if (Index > Offset || Index < 0)
                throw new IndexOutOfRangeException();
            else return Array[Index];
        }
    }

    /// <summary>
    /// 基础方法
    /// </summary>
    public partial class BufferArray<T>
    {
        #region Get

        /// <summary>
        /// 获取最后一个元素
        /// </summary>
        public T GetLast()
        {
            return Get(Offset - 1);
        }

        /// <summary>
        /// 获取第一个元素
        /// </summary>
        public T GetFirst()
        {
            return Get(0);
        }

        #endregion

        #region Add

        /// <summary>
        /// 添加
        /// </summary>
        public void Add(T Obj)
        {
            if (Offset >= Capacity) UpStepCapacity();
            Array[Offset++] = Obj;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void Add(T Obj, int Count)
        {
            for (int i = 0; i < Count; i++)
                Add(Obj);
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void Add(params T[] Objs)
        {
            // 如果为负数 则说明剩余长度不够 如果为正数 说明长度足以容纳
            if (Capacity - Offset - Objs.Length < 0) UpStepCapacity();
            foreach (var item in Objs)
                Array[Offset++] = item;
        }

        /// <summary>
        /// 添加
        /// </summary>
        public void Add<R>(R Objs) where R : IList<T>, IEnumerable<T>
        {
            // 如果为负数 则说明剩余长度不够 如果为正数 说明长度足以容纳
            if (Objs == null) throw new ArgumentNullException($"Buffer Array Insert Obj is null!");

            if (Capacity - Offset - Objs.Count < 0) UpStepCapacity();
            foreach (var item in Objs)
                Array[Offset++] = item;
        }

        public void AddFirst(T Obj)
        {
            Insert(0, Obj);
        }

        #endregion

        #region Insert

        /// <summary>
        /// 插入
        /// </summary>
        public void Insert(int Index, T Obj)
        {
            if (Index > Offset || Index < 0) throw new IndexOutOfRangeException($"Buffer Array Insert Index Out of!");
            if (Offset >= Capacity) UpStepCapacity();
            System.Array.Copy(Array, Index, Array, Index + 1, Offset++);
            Array[Index] = Obj;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public void Insert(int Index, params T[] Obj)
        {
            if (Index > Offset || Index < 0) throw new IndexOutOfRangeException($"Buffer Array Insert Index Out of!");
            if (Offset + Obj.Length >= Capacity) UpStepCapacity();
            System.Array.Copy(Array, Index, Array, Index + Obj.Length, Offset++);
            foreach (var item in Obj) Array[Index++] = item;
            Offset += Obj.Length - 1;
        }

        /// <summary>
        /// 插入
        /// </summary>
        public void Insert<R>(int Index, R Obj) where R : IList<T>, IEnumerable<T>
        {
            if (Index > Offset || Index < 0) throw new IndexOutOfRangeException($"Buffer Array Insert Index Out of!");
            if (Obj == null) throw new ArgumentNullException($"Buffer Array Insert Obj is null!");

            if (Offset + Obj.Count >= Capacity) UpStepCapacity();
            System.Array.Copy(Array, Index, Array, Index + Obj.Count, Offset++);
            foreach (var item in Obj)
                Array[Index++] = item;
            Offset += Obj.Count - 1;
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// 查找指定元素在第几位
        /// </summary>
        public int IndexOf(T Obj)
        {
            return IndexOf(Obj, 0);
        }

        /// <summary>
        /// 获取指定值下标 从元素第几个开始查找
        /// </summary>
        private int IndexOf(T Obj, int Offset)
        {
            if (Obj == null) throw new ArgumentNullException($"Buffer Array IndexOf Obj is null!");
            for (int i = Offset; i < this.Offset; i++)
            {
                if (Obj.Equals(Array[i]))
                    return i;
            }

            if (this.Offset == 0) return -1;
            throw new IndexOutOfRangeException($"Buffer Array IndexOf Index Out of!");
        }

        #endregion

        #region Remove

        /// <summary>
        /// 移除指定元素
        /// </summary>
        public bool Remove(T Obj)
        {
            if (Obj == null) throw new IndexOutOfRangeException($"Buffer Array Remove Index Out of!");
            int i = IndexOf(Obj, 0);
            if (i < 0) return false;
            RemoveAt(i);
            return true;
        }

        /// <summary>
        /// 移除指定下标元素
        /// </summary>
        public T RemoveAt(int Index)
        {
            if (Index >= Offset || Index < 0) return default;
            T localObject = this[Offset]; //获取本地变量
            System.Array.Copy(Array, Index + 1, Array, Index, Offset-- - Index);
            return localObject;
        }

        /// <summary>
        /// 移除指定下标元素
        /// </summary>
        void IList<T>.RemoveAt(int Index)
        {
            if (Index >= Offset || Index < 0) return;
            System.Array.Copy(Array, Index + 1, Array, Index, Offset-- - Index);
        }

        /// <summary>
        /// 移除第一个
        /// </summary>
        public T RemoveFirst()
        {
            return RemoveAt(0);
        }

        /// <summary>
        /// 移除最后一个
        /// </summary>
        public T RemoveLast()
        {
            T localObject = this[--Offset];
            Array[Offset] = default;
            return localObject;
        }

        #endregion
    }
}