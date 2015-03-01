using System;

namespace MonadiCSharp
{
    public interface IMaybe<TValue> : IEquatable<IMaybe<TValue>>
    {
        TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise);
    }
}
