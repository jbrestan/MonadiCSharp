using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.MaybeImplementation
{
    internal sealed class Just<TValue> : IMaybe<TValue>, IEquatable<Just<TValue>>
    {
        private readonly TValue value;

        internal Just(TValue value)
        {
            this.value = Ensure.NotNull(() => value);
        }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise) =>
            Ensure.NotNull(() => onValue).Invoke(value);
        

        #region Equality members and IEquatable implementation...
        public bool Equals(Just<TValue> other) =>
            other != null && value.Equals(other.value);

        public bool Equals(IMaybe<TValue> other) =>
            Equals(other as Just<TValue>);

        public override bool Equals(object obj) =>
            Equals(obj as Just<TValue>);

        public override int GetHashCode() => value.GetHashCode();
        #endregion

        #region IEnumerable implementation...
        public IEnumerator<TValue> GetEnumerator()
        {
            yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
