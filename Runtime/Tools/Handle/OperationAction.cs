using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AIO
{
    [StructLayout(LayoutKind.Auto)]
    public abstract partial class OperationAction : IOperationAction
    {
        public byte         Progress   { get; protected set; }
        public bool         IsDone     { get; protected set; }
        public bool         IsRunning  => !IsDone;
        public bool         IsValidate { get; protected set; }
        public event Action Completed;

        protected abstract TaskAwaiter CreateAsync();
        protected abstract IEnumerator CreateCoroutine();
        protected abstract void        CreateSync();
        protected virtual  void        OnReset()     { }
        protected virtual  void        OnDispose()   { }
        protected virtual  void        OnCompleted() { }

        private IEnumerator _CO;

        protected IEnumerator CO
        {
            get
            {
                if (_CO is null) _CO = CreateCoroutine();
                return _CO;
            }
        }

        public TaskAwaiter GetAwaiter()
        {
            if (IsDone || !IsValidate) return Task.CompletedTask.GetAwaiter();
            return CreateAsync();
        }


        public void Invoke()
        {
            if (IsDone || !IsValidate) return;
            CreateSync();
            IsDone = true;
            InvokeOnCompleted();
        }


        public void Reset()
        {
            OnReset();
            Progress  = 0;
            IsDone    = false;
            Completed = null;
            CO.Reset();
        }

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

        #region Constructor

        protected OperationAction()
        {
            IsValidate = true;
            IsDone     = false;
            Progress   = 0;
        }


        /// <inheritdoc />
        protected OperationAction(Action completed) : this()
        {
            Completed = completed;
        }

        #endregion

        #region operator implicit

        public static implicit operator Action(OperationAction      operationAction) => operationAction.Completed;
        public static implicit operator TaskAwaiter(OperationAction operationAction) => operationAction.GetAwaiter();

        #endregion

        #region AssetSystem.IHandle<TObject>

        /// <inheritdoc />
        TaskAwaiter IOperationAction.GetAwaiter() => GetAwaiter();

        /// <inheritdoc />
        event Action IOperationAction.Completed
        {
            add => Completed += value;
            remove => Completed -= value;
        }

        #endregion

        #region AssetSystem.IHandleBase

        byte IOperationBase.Progress   => Progress;
        bool IOperationBase.IsValidate => IsValidate;
        bool IOperationBase.IsDone     => IsDone;
        bool IOperationBase.IsRunning  => IsRunning;

        #endregion

        #region IDisposable

        void IDisposable.Dispose() => Dispose();

        #endregion

        #region IEnumerator

        void IEnumerator.  Reset()    => Reset();
        bool IEnumerator.  MoveNext() => MoveNext();
        object IEnumerator.Current    => CO.Current;

        #endregion

        #region INotifyCompletion

        /// <inheritdoc />
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (IsDone)
            {
                continuation?.Invoke();
            }
            else
            {
                Completed += continuation;
            }
        }

        #endregion

        public sealed override string ToString()         => string.Empty;
        public sealed override bool   Equals(object obj) => false;
        public sealed override int    GetHashCode()      => 0;
    }
}