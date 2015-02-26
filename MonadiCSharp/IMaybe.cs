using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadiCSharp
{
    public interface IMaybe<TValue>
    {
        IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f);
    }
}
