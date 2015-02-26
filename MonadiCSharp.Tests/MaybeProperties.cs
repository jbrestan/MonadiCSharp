using System;
using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using Xunit;
using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp.Tests
{
    public class MaybeProperties
    {
        [Property]
        public bool ShouldBeJustFromNonNull(NonNull<object> o)
        {
            return Maybe.Just(o.Item) is Just<object>;
        }

        [Fact]
        public void ShouldThrowWhenJustCalledWithNull()
        {
            Assert.Throws<ArgumentNullException>(() => Maybe.Just<object>(null));
        }

        [Fact]
        public void ShouldConstructNothingFromNothing()
        {
            Assert.IsType<Nothing<object>>(Maybe.Nothing<object>());
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool JustShouldBindToSuppliedFunction(IMaybe<object> just)
        {
            var wasCalled = false;
            Func<object, IMaybe<object>> f =
                wrapped => { wasCalled = true; return Maybe.Just(wrapped); };
            just.Bind(f);
            return wasCalled;
        }

        [Property]
        public bool JustShouldUseTheWrappedObjectInBind(NonNull<object> o)
        {
            var usesTheSameObject = false;
            Func<object, IMaybe<object>> f =
                wrapped => { usesTheSameObject = o.Item == wrapped; return Maybe.Just(wrapped); };
            Maybe.Just(o.Item).Bind(f);
            return usesTheSameObject;
        }

        [Property]
        public void NothingShouldBindToNothing(Func<object, IMaybe<object>> f)
        {
            var nothing = Maybe.Nothing<object>();
            Assert.IsType<Nothing<object>>(nothing.Bind(f));
        }

        private class JustArbitrary : Arbitrary<IMaybe<object>>
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
}
