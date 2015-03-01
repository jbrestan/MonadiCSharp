using System;
using System.Collections.Generic;

namespace MonadiCSharp
{
    public interface IMaybe<TValue> : IEquatable<IMaybe<TValue>>, IEnumerable<TValue>
    {
        TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise);
    }
}
