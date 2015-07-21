using FsCheck;
using FsCheck.Fluent;

namespace MonadiCSharp.Tests
{
    internal class JustArbitrary : Arbitrary<IMaybe<object>>
    {
        public static Arbitrary<IMaybe<object>> Maybe() => new JustArbitrary();

        public override Gen<IMaybe<object>> Generator
        {
            get
            {
                return from o in Any.OfType<object>()
                    where o != null
                    select o.ToMaybe();
            }
        }
    }
}