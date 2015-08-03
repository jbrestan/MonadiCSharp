using FsCheck;
using FsCheck.Fluent;

namespace MonadiCSharp.Tests.Arbitraries
{
    internal class EitherArbitrary : Arbitrary<IEither<object, object>>
    {
        public static Arbitrary<IEither<object, object>> Either() => new EitherArbitrary();

        public override Gen<IEither<object, object>> Generator
        {
            get
            {
                return Any.GeneratorIn(new RightArbitrary().Generator, new LeftArbitrary().Generator);
            }
        }
    }
}
