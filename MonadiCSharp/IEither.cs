using System;
using System.Collections.Generic;

namespace MonadiCSharp
{
    public interface IEither<TLeft, TRight> : IEquatable<IEither<TLeft, TRight>>, IEnumerable<TRight>
    {
        TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight);
    }
}
