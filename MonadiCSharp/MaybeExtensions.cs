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

        public static IMaybe<TValue> Unwrap<TValue>(this IMaybe<IMaybe<TValue>> doubleMaybe)
        {
            return Ensure.NotNull(() => doubleMaybe)
                .Match(innerMaybe => innerMaybe,
                       Maybe.Nothing<TValue>);
        }
    }
}
