#region

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    [StructLayout(LayoutKind.Auto)]
    public abstract class OperationAction : IOperationAction
    {
        private IEnumerator _CO;
        public  byte        Progress   { get; protected set; }
        public  bool        IsDone     { get; protected set; }
        public  bool        IsRunning  => !IsDone;
        public  bool        IsValidate { get; protected set; }

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
            if (IsDone || !IsValidate) return;
            CreateSync();
            IsDone = true;
            InvokeOnCompleted();
        }

        #region IDisposable

        void IDisposable.Dispose()
        {
            Dispose();
        }

        #endregion

        #region INotifyCompletion

        /// <inheritdoc />
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            if (IsDone)
                continuation?.Invoke();
            else
                Completed += continuation;
        }

        #endregion

        #endregion

        public event Action Completed;

        protected abstract TaskAwaiter CreateAsync();
        protected abstract IEnumerator CreateCoroutine();
        protected abstract void        CreateSync();
        protected virtual  void        OnReset()     { }
        protected virtual  void        OnDispose()   { }
        protected virtual  void        OnCompleted() { }

        public TaskAwaiter GetAwaiter()
        {
            if (IsDone || !IsValidate) return Task.CompletedTask.GetAwaiter();
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

        public sealed override string ToString()
        {
            return string.Empty;
        }

        public sealed override bool Equals(object obj)
        {
            return false;
        }

        public sealed override int GetHashCode()
        {
            return 0;
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

        public static implicit operator Action(OperationAction operationAction)
        {
            return operationAction.Completed;
        }

        public static implicit operator TaskAwaiter(OperationAction operationAction)
        {
            return operationAction.GetAwaiter();
        }

        #endregion

        #region AssetSystem.IHandle<TObject>

        /// <inheritdoc />
        TaskAwaiter IOperationAction.GetAwaiter()
        {
            return GetAwaiter();
        }

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