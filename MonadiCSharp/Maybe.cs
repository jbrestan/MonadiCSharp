using MonadiCSharp.MaybeImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadiCSharp
{
    public static class Maybe
    {
        public static IMaybe<TValue> Just<TValue>(TValue value)
        {
            return new Just<TValue>(value);
        }

        public static IMaybe<TValue> Nothing<TValue>()
        {
            return new Nothing<TValue>();
        }
    }
}
