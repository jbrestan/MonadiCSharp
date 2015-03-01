using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.MaybeImplementation
{
    internal sealed class Nothing<TValue> : IMaybe<TValue>, IEquatable<Nothing<TValue>>
    {
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
            return Equals(other as Nothing<TValue>);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Nothing<TValue>);
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
