using FsCheck;
using FsCheck.Fluent;

namespace MonadiCSharp.Tests
{
    internal class MaybeArbitrary : Arbitrary<IMaybe<object>>
    {
        public static Arbitrary<IMaybe<object>> Maybe() => new MaybeArbitrary();

        public override Gen<IMaybe<object>> Generator
        {
            get
            {
                return from o in Any.OfType<object>()
                       select o.ToMaybe();
            }
        }
    }
}
