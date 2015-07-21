using System;

namespace MonadiCSharp
{
    public static class MaybeExtensions
    {
        public static IMaybe<TResult> Bind<TValue, TResult>(this IMaybe<TValue> maybe, Func<TValue, IMaybe<TResult>> f) =>
            Ensure.NotNull(() => maybe).Match(f, Maybe.Nothing<TResult>);
        

        public static IMaybe<TValue> ToMaybe<TValue>(this TValue value) =>
            value != null
                ? Maybe.Just(value)
                : Maybe.Nothing<TValue>();

        public static IMaybe<TValue> Unwrap<TValue>(this IMaybe<IMaybe<TValue>> doubleMaybe) =>
            Ensure.NotNull(() => doubleMaybe)
                .Match(innerMaybe => innerMaybe, Maybe.Nothing<TValue>);

        public static TValue GetValueOrDefault<TValue>(this IMaybe<TValue> maybe) =>
            maybe.GetValueOrDefault(default(TValue));

        public static TValue GetValueOrDefault<TValue>(this IMaybe<TValue> maybe, TValue defaultValue) =>
            Ensure.NotNull(() => maybe)
                .Match(value => value, () => defaultValue);
    }
}
