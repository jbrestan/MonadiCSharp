using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadiCSharp
{
    public static class MaybeExtensions
    {
        public static IMaybe<TValue> ToMaybe<TValue>(this TValue value)
        {
            return value != null
                ? Maybe.Just(value)
                : Maybe.Nothing<TValue>();
        }
    }
}
