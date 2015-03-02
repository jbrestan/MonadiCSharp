using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.MaybeImplementation
{
    internal sealed class Nothing<TValue> : IMaybe<TValue>, IEquatable<Nothing<TValue>>
    {
        internal static readonly Nothing<TValue> Value = new Nothing<TValue>();

        private Nothing() { }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise)
        {
            return Ensure.NotNull(() => otherwise).Invoke();
        }

        #region Equality members and IEquatable implementation...
        public bool Equals(Nothing<TValue> other)
        {
            return true;
        }

        public bool Equals(IMaybe<TValue> other)
        {
            return ReferenceEquals(this, other);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj);
        }

        public override int GetHashCode()
        {
            return 0;
        }
        #endregion

        #region IEnumerable implementation...
        public IEnumerator<TValue> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
