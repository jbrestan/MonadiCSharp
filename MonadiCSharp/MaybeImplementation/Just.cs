using System;

namespace MonadiCSharp.MaybeImplementation
{
    internal class Just<TValue> : IMaybe<TValue>
    {
        private readonly TValue value;

        internal Just(TValue value)
        {
            this.value = Ensure.NotNull(() => value);
        }

        public IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f)
        {
            return Ensure.NotNull(() => f).Invoke(value);
        }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise)
        {
            return Ensure.NotNull(() => onValue).Invoke(value);
        }

        public bool Equals(IMaybe<TValue> other)
        {
            var asJust = other as Just<TValue>;
            return asJust != null && value.Equals(asJust.value);
        }
    }
}
