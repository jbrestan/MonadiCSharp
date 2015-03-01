using System;

namespace MonadiCSharp.MaybeImplementation
{
    internal sealed class Nothing<TValue> : IMaybe<TValue>, IEquatable<Nothing<TValue>>
    {

        public IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f)
        {
            return new Nothing<TResult>();
        }

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
    }
}
