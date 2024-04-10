#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

#endregion

namespace AIO
{
    [StructLayout(LayoutKind.Auto)]
    public abstract class OperationGenericsList<TObject> : OperationGenerics<TObject[]>, IOperationList<TObject>, IOperationList
    {
        public TObject this[int index] => IsValidate && IsDone && Result != null &&
                                          index >= 0 && index < Result.Length
            ? Result[index]
            : default;

        #region IOperationList<TObject> Members

        public int Count => IsValidate && IsDone && Result != null ? Result.Length : 0;

        #endregion

        #region IOperationList

        event Action<object[]> IOperationList.Completed
        {
            add => Completed += value as Action<TObject[]>;
            remove => Completed -= value as Action<TObject[]>;
        }

        object IOperationList.this[int index] => this[index];
        object[] IOperationList.Result => Result.To<object[]>();

        object[] IOperationList.Invoke()
        {
            return Invoke().To<object[]>();
        }

        TaskAwaiter<object[]> IOperationList.GetAwaiter()
        {
            return IsValidate
                ? CreateAsync().To<TaskAwaiter<object[]>>()
                : Task.FromResult<object[]>(null).GetAwaiter();
        }

        #endregion

        #region IOperationList<TObject>

        TObject IOperationList<TObject>.this[int index] => this[index];
        TObject[] IOperationList<TObject>.Result => Result;

        TObject[] IOperationList<TObject>.Invoke()
        {
            return Invoke();
        }

        TaskAwaiter<TObject[]> IOperationList<TObject>.GetAwaiter()
        {
            return IsValidate
                ? CreateAsync()
                : Task.FromResult<TObject[]>(null).GetAwaiter();
        }

        #endregion


        #region IEnumerator<TObject>

        IEnumerator<TObject> IOperationList<TObject>.GetEnumerator()
        {
            if (!IsValidate || !IsDone || Result is null || Result.Length == 0) yield break;
            foreach (var obj in Result) yield return obj;
        }

        IEnumerator<object> IOperationList.GetEnumerator()
        {
            if (!IsValidate || !IsDone || Result is null || Result.Length == 0) yield break;
            foreach (var obj in Result) yield return obj;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (!IsValidate || !IsDone || Result is null || Result.Length == 0) yield break;
            foreach (var obj in Result) yield return obj;
        }

        #endregion

        #region Constructor

        protected OperationGenericsList() { }

        protected OperationGenericsList(Action<TObject[]> onCompleted)
            : base(onCompleted) { }

        #endregion
    }
}