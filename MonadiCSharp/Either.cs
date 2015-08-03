using MonadiCSharp.EitherImplementation;

namespace MonadiCSharp
{
    public static class Either
    {
        public static IEither<TLeft, TRight> Left<TLeft, TRight>(TLeft value) => new Left<TLeft, TRight>(value);
        public static IEither<TLeft, TRight> Right<TLeft, TRight>(TRight value) => new Right<TLeft, TRight>(value);
    }
}
