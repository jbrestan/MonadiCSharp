using FsCheck;
using FsCheck.Fluent;

namespace MonadiCSharp.Tests.Arbitraries
{
    using static Either;

    internal class LeftArbitrary : Arbitrary<IEither<object, object>>
    {
        public static Arbitrary<IEither<object, object>> Either() => new LeftArbitrary();

        public override Gen<IEither<object, object>> Generator
        {
            get
            {
                return from o in Any.OfType<object>()
                       select Left<object, object>(o);
            }
        }
    }
}
