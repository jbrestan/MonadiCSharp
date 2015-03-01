using System;

namespace MonadiCSharp.MaybeImplementation
{
    internal class Nothing<TValue> : IMaybe<TValue>
    {
        public IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f)
        {
            return new Nothing<TResult>();
        }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise)
        {
            return Ensure.NotNull(() => otherwise).Invoke();
        }

        public bool Equals(IMaybe<TValue> other)
        {
            return other is Nothing<TValue>;
        }
    }
}
