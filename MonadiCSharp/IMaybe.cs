using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadiCSharp
{
    public interface IMaybe<TValue> : IEquatable<IMaybe<TValue>>
    {
        IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f);

        TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise);
    }
}
