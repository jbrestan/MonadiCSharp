using System;

namespace MonadiCSharp
{
    using static Ensure;
    using static Maybe;

    public static class MaybeExtensions
    {
        public static IMaybe<TResult> Bind<TValue, TResult>(this IMaybe<TValue> maybe, Func<TValue, IMaybe<TResult>> f) =>
            NotNull(() => maybe).Match(f, Nothing<TResult>);
        
        public static IMaybe<TValue> ToMaybe<TValue>(this TValue value) =>
            value != null ? Just(value) : Nothing<TValue>();

        public static IMaybe<TValue> Unwrap<TValue>(this IMaybe<IMaybe<TValue>> doubleMaybe) =>
            NotNull(() => doubleMaybe).Match(innerMaybe => innerMaybe, Nothing<TValue>);

        public static TValue GetValueOrDefault<TValue>(this IMaybe<TValue> maybe) =>
            NotNull(() => maybe).GetValueOrDefault(default(TValue));

        public static TValue GetValueOrDefault<TValue>(this IMaybe<TValue> maybe, TValue defaultValue) =>
            NotNull(() => maybe).Match(value => value, () => defaultValue);
    }
}
