using System;

namespace MonadiCSharp.MaybeImplementation
{
    internal sealed class Just<TValue> : IMaybe<TValue>, IEquatable<Just<TValue>>
    {
        private readonly TValue value;

        internal Just(TValue value)
        {
            this.value = Ensure.NotNull(() => value);
        }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise)
        {
            return Ensure.NotNull(() => onValue).Invoke(value);
        }

        #region Equality members and IEquatable implementation...
        public bool Equals(Just<TValue> other)
        {
            return other != null && value.Equals(other.value);
        }

        public bool Equals(IMaybe<TValue> other)
        {
            return Equals(other as Just<TValue>);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Just<TValue>);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
        #endregion
    }
}
