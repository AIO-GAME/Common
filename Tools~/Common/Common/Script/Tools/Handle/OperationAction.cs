#region

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    /// <summary>
    /// 异步函数处理器
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public abstract class OperationAction : IOperationAction
    {
#pragma warning disable  CS1591
        private IEnumerator _CO;

        public byte Progress   { get; protected set; }
        public bool IsDone     { get; protected set; }
        public bool IsRunning  => !IsDone;
        public bool IsValidate { get; protected set; }

        protected IEnumerator CO
        {
            get
            {
                if (_CO is null) _CO = CreateCoroutine();
                return _CO;
            }
        }

        #region IOperationAction Members

        public void Invoke()
        {
            if (!IsValidate || IsDone)
            {
                InvokeOnCompleted();
                return;
            }

            CreateSync();
            IsDone = true;
            InvokeOnCompleted();
        }

        #region IDisposable

        void IDisposable.Dispose() => Dispose();

        #endregion

        #region INotifyCompletion

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (IsDone) continuation?.Invoke();
            else Completed += continuation;
        }

        #endregion

        #region ITask

        TaskAwaiter ITask.GetAwaiter() => GetAwaiter();

        #endregion

        #region IOperationAction

        event Action IOperationAction.Completed
        {
            add => Completed += value;
            remove => Completed -= value;
        }

        #endregion

        #endregion

        public event Action            Completed;
        protected abstract TaskAwaiter CreateAsync();
        protected abstract IEnumerator CreateCoroutine();
        protected abstract void        CreateSync();
        protected virtual  void        OnReset()     { }
        protected virtual  void        OnDispose()   { }
        protected virtual  void        OnCompleted() { }

        public TaskAwaiter GetAwaiter()
        {
            if (IsDone || !IsValidate)
            {
                InvokeOnCompleted();
                return Task.CompletedTask.GetAwaiter();
            }

            return CreateAsync();
        }

        public void Reset()
        {
            OnReset();
            Progress  = 0;
            IsDone    = false;
            Completed = null;
            CO.Reset();
        }

        /// <returns>
        ///  <see langword="true" /> if the operation was successfully advanced to the next element;
        /// </returns>
        public bool MoveNext()
        {
            if (IsDone || !IsValidate) return false;
            return CO.MoveNext();
        }

        public void Dispose()
        {
            Completed = null;
            OnDispose();
        }

        protected void InvokeOnCompleted()
        {
            Progress = 100;
            IsDone   = true;
            OnCompleted();
            if (Completed == null) return;
            Completed.Invoke();
            Completed = null;
        }

        public sealed override string ToString()         => string.Empty;
        public sealed override bool   Equals(object obj) => false;
        public sealed override int    GetHashCode()      => 0;

        #region Constructor

        protected OperationAction()
        {
            IsValidate = true;
            IsDone     = false;
            Progress   = 0;
        }

        protected OperationAction(Action completed) : this() => Completed = completed;

        #endregion

        #region operator implicit

        /// <summary>
        ///   Implicit conversion from <see cref="OperationAction" /> to <see cref="Action" />
        /// </summary>
        /// <param name="operationAction"><see cref="OperationAction" /></param>
        /// <returns><see cref="Action" /></returns>
        public static implicit operator Action(OperationAction operationAction) => operationAction.Completed;

        /// <summary>
        ///    Implicit conversion from <see cref="OperationAction" /> to <see cref="TaskAwaiter" />
        /// </summary>
        /// <param name="operationAction"><see cref="OperationAction" /></param>
        /// <returns><see cref="TaskAwaiter" /></returns>
        public static implicit operator TaskAwaiter(OperationAction operationAction) => operationAction.GetAwaiter();

        #endregion

        #region IOperationBase

        byte IOperationBase.Progress   => Progress;
        bool IOperationBase.IsValidate => IsValidate;
        bool IOperationBase.IsDone     => IsDone;
        bool IOperationBase.IsRunning  => IsRunning;

        #endregion

        #region IEnumerator

        void IEnumerator.Reset()
        {
            Reset();
        }

        bool IEnumerator.MoveNext()
        {
            return MoveNext();
        }

        object IEnumerator.Current => CO.Current;

        #endregion
    }
}