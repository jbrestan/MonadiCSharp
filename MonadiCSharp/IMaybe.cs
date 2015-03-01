using System;

namespace MonadiCSharp
{
    public interface IMaybe<TValue> : IEquatable<IMaybe<TValue>>
    {
        IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f);

        TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise);
    }
}
