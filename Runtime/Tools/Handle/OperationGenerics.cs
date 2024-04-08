using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AIO
{
    [StructLayout(LayoutKind.Auto)]
    public abstract class OperationGenerics<TObject> : IOperation<TObject>, IOperation
    {
        public event Action<TObject> Completed;
        public  byte                 Progress   { get; protected set; }
        public  bool                 IsDone     { get; protected set; }
        public  TObject              Result     { get; protected set; }
        public  bool                 IsValidate { get; protected set; }
        public  bool                 IsRunning  => !IsDone;
        private IEnumerator          _Coroutine;

        protected abstract IEnumerator          CreateCoroutine();
        protected abstract TaskAwaiter<TObject> CreateAsync();
        protected abstract void                 CreateSync();
        protected virtual  void                 OnReset()   { }
        protected virtual  void                 OnDispose() { }


        protected IEnumerator Coroutine
        {
            get
            {
                if (_Coroutine is null) _Coroutine = CreateCoroutine();
                return _Coroutine;
            }
        }

        public void Reset()
        {
            OnReset();
            Progress  = 0;
            IsDone    = false;
            Completed = null;
            _Coroutine.Reset();
        }

        public TObject Invoke()
        {
            if (!IsValidate) return Result;
            if (IsDone) return Result;

            CreateSync();
            IsDone = true;
            InvokeOnCompleted();
            return Result;
        }

        public TaskAwaiter<TObject> GetAwaiter()
        {
            TaskAwaiter<TObject> Awaiter;
            if (IsDone) Awaiter = Task.FromResult(Result).GetAwaiter();
            else if (IsValidate) return CreateAsync();
            else Awaiter = Task.FromResult(Result).GetAwaiter();
            Awaiter.OnCompleted(InvokeOnCompleted);
            return Awaiter;
        }

        public void Dispose()
        {
            OnDispose();
            _Coroutine = null;
            Completed  = null;
        }

        public bool MoveNext()
        {
            if (!IsValidate || IsDone) return false;
            return Coroutine.MoveNext();
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

        public static implicit operator TObject(OperationGenerics<TObject>              operationGenerics) => operationGenerics.Result;
        public static implicit operator TaskAwaiter<TObject>(OperationGenerics<TObject> operationGenerics) => operationGenerics.GetAwaiter();

        #endregion

        #region IHandle<TObject>

        TaskAwaiter<TObject> IOperation<TObject>.GetAwaiter() => GetAwaiter();
        TObject IOperation<TObject>.             Result       => Result;

        event Action<TObject> IOperation<TObject>.Completed
        {
            add => Completed += value;
            remove => Completed -= value;
        }

        #endregion

        #region IOperationBase

        byte IOperationBase.Progress   => Progress;
        bool IOperationBase.IsDone     => IsDone;
        bool IOperationBase.IsValidate => IsValidate;
        bool IOperationBase.IsRunning  => IsRunning;

        #endregion

        #region IDisposable

        void IDisposable.Dispose() => Dispose();

        #endregion

        #region IEnumerator

        void IEnumerator.  Reset()    => Reset();
        bool IEnumerator.  MoveNext() => MoveNext();
        object IEnumerator.Current    => Coroutine.Current;

        #endregion

        #region IOperation

        object IOperation.Invoke() => Invoke();
        object IOperation.Result   => Result;

        event Action<object> IOperation.Completed
        {
            add => Completed += value as Action<TObject>;
            remove => Completed -= value as Action<TObject>;
        }

        TaskAwaiter<object> IOperation.GetAwaiter()
        {
            TaskAwaiter<object> Awaiter;
            if (IsDone) Awaiter = Task.FromResult<object>(Result).GetAwaiter();
            else if (IsValidate) return CreateAsync().To<TaskAwaiter<object>>();
            else Awaiter = Task.FromResult<object>(Result).GetAwaiter();
            Awaiter.OnCompleted(InvokeOnCompleted);
            return Awaiter;
        }

        #endregion

        public sealed override string ToString()         => string.Empty;
        public sealed override bool   Equals(object obj) => false;
        public sealed override int    GetHashCode()      => 0;
    }
}