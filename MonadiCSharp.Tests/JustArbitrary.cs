using FsCheck;
using FsCheck.Fluent;
using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp.Tests
{
    internal class JustArbitrary : Arbitrary<IMaybe<object>>
    {
        public static Arbitrary<IMaybe<object>> Maybe()
        {
            return new JustArbitrary();
        }

        public override Gen<IMaybe<object>> Generator
        {
            get
            {
                return from o in Any.OfType<object>()
                    where o != null
                    select new Just<object>(o) as IMaybe<object>;
            }
        }
    }
}