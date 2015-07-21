using System;

namespace MonadiCSharp
{
    using static Ensure;
    using static Maybe;
    
    public static class MaybeLinqExtensions
    {
        public static IMaybe<TResult> Select<TValue, TResult>(
            this IMaybe<TValue> maybe, Func<TValue, TResult> selector) =>
            NotNull(() => maybe).Bind(value => NotNull(() => selector).Invoke(value).ToMaybe());

        public static IMaybe<TValue> Where<TValue>(this IMaybe<TValue> maybe, Func<TValue, bool> predicate) =>
            NotNull(() => maybe).Bind(
                val => NotNull(() => predicate).Invoke(val)
                    ? Just(val)
                    : Nothing<TValue>());

        public static IMaybe<TResult> SelectMany<TValue, TResult>(
            this IMaybe<TValue> maybe, Func<TValue, IMaybe<TResult>> selector) =>
            NotNull(() => maybe).Bind(
                outerVal => NotNull(() => selector).Invoke(outerVal).Bind(
                    innerVal => innerVal.ToMaybe()));

        public static IMaybe<TResult> SelectMany<TOuterValue, TInnerValue, TResult>(
            this IMaybe<TOuterValue> maybe,
            Func<TOuterValue, IMaybe<TInnerValue>> innerSelector,
            Func<TOuterValue, TInnerValue, TResult> resultSelector) =>
            NotNull(() => maybe).Bind(
                outerValue => NotNull(() => innerSelector).Invoke(outerValue).Bind(
                    innerValue => NotNull(() => resultSelector).Invoke(outerValue, innerValue).ToMaybe()));
    }
}
