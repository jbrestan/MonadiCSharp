using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonadiCSharp
{
    public static class MaybeLinqExtensions
    {
        public static IMaybe<TResult> Select<TValue, TResult>(
            this IMaybe<TValue> maybe, Func<TValue, TResult> selector) =>
            maybe.Bind(value => Ensure.NotNull(() => selector).Invoke(value).ToMaybe());

        public static IMaybe<TValue> Where<TValue>(this IMaybe<TValue> maybe, Func<TValue, bool> predicate) =>
            maybe.Bind(
                val => Ensure.NotNull(() => predicate).Invoke(val)
                    ? Maybe.Just(val)
                    : Maybe.Nothing<TValue>());

        public static IMaybe<TResult> SelectMany<TValue, TResult>(
            this IMaybe<TValue> maybe, Func<TValue, IMaybe<TResult>> selector) =>
            maybe.Bind(
                outerVal => Ensure.NotNull(() => selector).Invoke(outerVal).Bind(
                    innerVal => innerVal.ToMaybe()));

        public static IMaybe<TResult> SelectMany<TOuterValue, TInnerValue, TResult>(
            this IMaybe<TOuterValue> maybe,
            Func<TOuterValue, IMaybe<TInnerValue>> innerSelector,
            Func<TOuterValue, TInnerValue, TResult> resultSelector) =>
            maybe.Bind(
                outerValue => Ensure.NotNull(() => innerSelector).Invoke(outerValue).Bind(
                    innerValue => Ensure.NotNull(() => resultSelector).Invoke(outerValue, innerValue).ToMaybe()));
    }
}
