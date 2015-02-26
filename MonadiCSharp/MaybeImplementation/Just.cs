using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return f(value);
        }
    }
}
