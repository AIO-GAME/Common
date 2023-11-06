/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2023-11-06
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Threading;

namespace AIO
{
    /// <summary>
    /// Disposable lock class performs exit action on dispose operation.
    /// </summary>
    public class LockDisposable : IDisposable
    {
        private readonly Action _exitLock;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exitLock">退出锁定函数</param>
        public LockDisposable(Action exitLock)
        {
            _exitLock = exitLock;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _exitLock();
        }
    }

    /// <summary>
    /// Read lock class enters read lock on construction and performs exit read lock on dispose.
    /// </summary>
    public class LockRead : LockDisposable
    {
        /// <inheritdoc />
        public LockRead(ReaderWriterLockSlim locker) : base(locker.ExitReadLock)
        {
            locker.EnterReadLock();
        }
    }

    /// <summary>
    /// Write lock class enters write lock on construction and performs exit write lock on dispose.
    /// </summary>
    public class LockWrite : LockDisposable
    {
        /// <inheritdoc />
        public LockWrite(ReaderWriterLockSlim locker) : base(locker.ExitWriteLock)
        {
            locker.EnterWriteLock();
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static partial class ReaderWriterLockSlimExtend
    {
        /// <summary>
        /// Write lock class enters write lock on construction and performs exit write lock on dispose.
        /// </summary>
        public static LockWrite LockWrite(this ReaderWriterLockSlim slim)
        {
            return new LockWrite(slim);
        }

        /// <summary>
        /// Read lock class enters read lock on construction and performs exit read lock on dispose.
        /// </summary>
        public static LockRead LockRead(this ReaderWriterLockSlim slim)
        {
            return new LockRead(slim);
        }
    }
}