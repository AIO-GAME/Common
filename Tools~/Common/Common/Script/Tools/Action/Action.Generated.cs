using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AIO
{
    #region ActionAsync

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public partial class ActionAsync : IEnumerator, IDisposable
    {
        private Action action;
        
        /// <inheritdoc />
        internal ActionAsync(Action action)
        {
            this.action = action;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke();
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1> : IEnumerator, IDisposable
    {
        private Action<T1> action;
        private readonly T1 t1;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1> action, T1 t1)
        {
            this.action = action;
            this.t1 = t1;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2> : IEnumerator, IDisposable
    {
        private Action<T1, T2> action;
        private readonly T1 t1;
        private readonly T2 t2;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2> action, T1 t1, T2 t2)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4, T5>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4, T5> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4, T5> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        private readonly T5 t5;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4, t5);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4, t5); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4, T5, T6>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4, T5, T6> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4, T5, T6> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        private readonly T5 t5;
        private readonly T6 t6;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4, T5, T6> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
            this.t6 = t6;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4, t5, t6);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4, t5, t6); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4, T5, T6, T7>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4, T5, T6, T7> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4, T5, T6, T7> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        private readonly T5 t5;
        private readonly T6 t6;
        private readonly T7 t7;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
            this.t6 = t6;
            this.t7 = t7;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4, t5, t6, t7);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4, t5, t6, t7); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4, T5, T6, T7, T8> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        private readonly T5 t5;
        private readonly T6 t6;
        private readonly T7 t7;
        private readonly T8 t8;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
            this.t6 = t6;
            this.t7 = t7;
            this.t8 = t8;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8); }).GetAwaiter();
    }

    #endregion

    #region ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>

    /// <summary>
    /// Action to enumerator or async task.
    /// </summary>
    public class ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> : IEnumerator, IDisposable
    {
        private Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action;
        private readonly T1 t1;
        private readonly T2 t2;
        private readonly T3 t3;
        private readonly T4 t4;
        private readonly T5 t5;
        private readonly T6 t6;
        private readonly T7 t7;
        private readonly T8 t8;
        private readonly T9 t9;
        
        /// <inheritdoc />
        internal ActionAsync(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            this.action = action;
            this.t1 = t1;
            this.t2 = t2;
            this.t3 = t3;
            this.t4 = t4;
            this.t5 = t5;
            this.t6 = t6;
            this.t7 = t7;
            this.t8 = t8;
            this.t9 = t9;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9);
            return false;
        }

        /// <inheritdoc />
        public void Reset() { }

        /// <inheritdoc />
        public object Current => null;

        /// <inheritdoc />
        public void Dispose() => action = null;

        /// <summary>
        /// Get the awaiter.
        /// </summary>
        public TaskAwaiter GetAwaiter() => Task.Factory.StartNew(() => { action?.Invoke(t1, t2, t3, t4, t5, t6, t7, t8, t9); }).GetAwaiter();
    }

    #endregion

    partial class ActionAsync
    {

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync"/> class.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync Create(Action action) => new ActionAsync(action);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1> Create<T1>(Action<T1> action, T1 t1) => new ActionAsync<T1>(action, t1);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2> Create<T1, T2>(Action<T1, T2> action, T1 t1, T2 t2) => new ActionAsync<T1, T2>(action, t1, t2);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3> Create<T1, T2, T3>(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3) => new ActionAsync<T1, T2, T3>(action, t1, t2, t3);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4> Create<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4) => new ActionAsync<T1, T2, T3, T4>(action, t1, t2, t3, t4);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4, T5}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) => new ActionAsync<T1, T2, T3, T4, T5>(action, t1, t2, t3, t4, t5);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4, T5, T6}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) => new ActionAsync<T1, T2, T3, T4, T5, T6>(action, t1, t2, t3, t4, t5, t6);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4, T5, T6, T7}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) => new ActionAsync<T1, T2, T3, T4, T5, T6, T7>(action, t1, t2, t3, t4, t5, t6, t7);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4, T5, T6, T7, T8}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) => new ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8>(action, t1, t2, t3, t4, t5, t6, t7, t8);

        /// <summary>
        /// Create a new instance of the <see cref="ActionAsync{T1, T2, T3, T4, T5, T6, T7, T8, T9}"/> class.
        /// </summary>
        /// <typeparam name="T1">The type of the 1th parameter.</typeparam>
        /// <typeparam name="T2">The type of the 2th parameter.</typeparam>
        /// <typeparam name="T3">The type of the 3th parameter.</typeparam>
        /// <typeparam name="T4">The type of the 4th parameter.</typeparam>
        /// <typeparam name="T5">The type of the 5th parameter.</typeparam>
        /// <typeparam name="T6">The type of the 6th parameter.</typeparam>
        /// <typeparam name="T7">The type of the 7th parameter.</typeparam>
        /// <typeparam name="T8">The type of the 8th parameter.</typeparam>
        /// <typeparam name="T9">The type of the 9th parameter.</typeparam>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A new instance of the <see cref="ActionAsync{T}"/> class.</returns>
        public static ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) => new ActionAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(action, t1, t2, t3, t4, t5, t6, t7, t8, t9);
    }
}