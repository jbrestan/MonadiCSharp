using FsCheck;
using FsCheck.Fluent;

namespace MonadiCSharp.Tests.Arbitraries
{
    using static Either;

    internal class RightArbitrary : Arbitrary<IEither<object, object>>
    {
        public static Arbitrary<IEither<object, object>> Either() => new RightArbitrary();

        public override Gen<IEither<object, object>> Generator
        {
            get
            {
                return from o in Any.OfType<object>()
                       select Right<object,object>(o);
            }
        }
    }
}