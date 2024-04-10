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
    ///     异步函数处理器
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public abstract class OperationGenerics<TObject> : IOperation<TObject>
    {
#pragma warning disable  CS1591
        private IEnumerator _Coroutine;
        public  byte        Progress   { get; protected set; }
        public  bool        IsDone     { get; protected set; }
        public  TObject     Result     { get; protected set; }
        public  bool        IsValidate { get; protected set; }
        public  bool        IsRunning  => !IsDone;

        protected IEnumerator Coroutine
        {
            get
            {
                if (_Coroutine is null) _Coroutine = CreateCoroutine();
                return _Coroutine;
            }
        }

        public event Action<TObject>            Completed;
        protected abstract IEnumerator          CreateCoroutine();
        protected abstract TaskAwaiter<TObject> CreateAsync();
        protected abstract void                 CreateSync();
        protected virtual  void                 OnReset()   { }
        protected virtual  void                 OnDispose() { }

        public TObject Invoke()
        {
            if (!IsValidate)
            {
                InvokeOnCompleted();
                return Result;
            }

            if (IsDone)
            {
                InvokeOnCompleted();
                return Result;
            }

            CreateSync();
            IsDone = true;
            InvokeOnCompleted();
            return Result;
        }

        public void Reset()
        {
            OnReset();
            Progress  = 0;
            IsDone    = false;
            Completed = null;
            _Coroutine.Reset();
        }

        public TaskAwaiter<TObject> GetAwaiter()
        {
            TaskAwaiter<TObject> Awaiter;
            if (!IsValidate) Awaiter = Task.FromException<TObject>(new Exception("Operation is not valid")).GetAwaiter();
            else if (IsDone) Awaiter = Task.FromResult(Result).GetAwaiter();
            else Awaiter             = CreateAsync();

            Awaiter.OnCompleted(InvokeOnCompleted);
            return Awaiter;
        }

        public void Dispose()
        {
            OnDispose();
            _Coroutine = null;
            Completed  = null;
        }

        /// <returns>
        ///  <see langword="true" /> if the operation was successfully advanced to the next element;
        /// </returns>
        public bool MoveNext()
        {
            if (IsValidate && !IsDone) return Coroutine.MoveNext();
            InvokeOnCompleted();
            return false;
        }

        protected void InvokeOnCompleted()
        {
            IsDone     = true;
            Progress   = 100;
            _Coroutine = null;
            if (Completed == null) return;
            Completed.Invoke(Result);
            Completed = null;
        }

        public sealed override string ToString()         => string.Empty;
        public sealed override bool   Equals(object obj) => false;
        public sealed override int    GetHashCode()      => 0;

        #region Constructor

        protected OperationGenerics()
        {
            IsValidate = true;
            IsDone     = false;
            Progress   = 0;
        }

        protected OperationGenerics(Action<TObject> onCompleted)
        {
            IsValidate = true;
            IsDone     = false;
            Progress   = 0;
            Completed  = onCompleted;
        }

        #endregion

        #region operator implicit

        /// <summary>
        ///    Implicit conversion from <see cref="OperationGenerics{TObject}" /> to <see cref="TObject" />
        /// </summary>
        /// <param name="operationGenerics"><see cref="OperationGenerics{TObject}" /></param>
        /// <returns><see cref="TObject" /></returns>
        public static implicit operator TObject(OperationGenerics<TObject> operationGenerics) => operationGenerics.Result;

        /// <summary>
        ///   Implicit conversion from <see cref="OperationGenerics{TObject}" /> to <see cref="TaskAwaiter{TObject}" />
        /// </summary>
        /// <param name="operationGenerics"> <see cref="OperationGenerics{TObject}" /></param>
        /// <returns><see cref="TaskAwaiter{TObject}" /></returns>
        public static implicit operator TaskAwaiter<TObject>(OperationGenerics<TObject> operationGenerics) => operationGenerics.GetAwaiter();

        #endregion

        #region ITask<TObject>

        TaskAwaiter<TObject> ITask<TObject>.GetAwaiter() => GetAwaiter();

        #endregion

        #region IOperationBase

        byte IOperationBase.Progress   => Progress;
        bool IOperationBase.IsDone     => IsDone;
        bool IOperationBase.IsValidate => IsValidate;
        bool IOperationBase.IsRunning  => IsRunning;

        #endregion

        #region IEnumerator

        void IEnumerator.  Reset()    => Reset();
        bool IEnumerator.  MoveNext() => MoveNext();
        object IEnumerator.Current    => Coroutine.Current;

        #endregion

        #region IDisposable

        void IDisposable.Dispose() => Dispose();

        #endregion

        #region IOperation<TObject>

        TObject IOperation<TObject>.Invoke() => Invoke();
        TObject IOperation<TObject>.Result   => Result;

        event Action<TObject> IOperation<TObject>.Completed
        {
            add => Completed += value;
            remove => Completed -= value;
        }

        #endregion

        #region IOperation

        object IOperation.Invoke() => Invoke();
        object IOperation.Result   => Result;

        event Action<object> IOperation.Completed
        {
            add => Completed += value as Action<TObject>;
            remove => Completed -= value as Action<TObject>;
        }

        TaskAwaiter<object> ITaskObject.GetAwaiter() => GetAwaiter().To<TaskAwaiter<object>>();

        #endregion
    }
}