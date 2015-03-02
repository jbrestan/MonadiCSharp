using System;
using System.Diagnostics.CodeAnalysis;
using FsCheck;
using FsCheck.Xunit;
using MonadiCSharp.MaybeImplementation;
using Xunit;

namespace MonadiCSharp.Tests
{
    public class MaybeExtensionsProperties
    {
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

        [Property]
        public bool ToMaybeReturnsJustWhenSuppliedWithNotNull(NonNull<object> o)
        {
            return o.Item.ToMaybe() is Just<object>;
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void ToMaybeReturnsNothingWhenSuppliedWithNull()
        {
            object o = null;
            Assert.IsType<Nothing<object>>(o.ToMaybe());
        }
        [Fact]
        public void UnwrapNothingShouldReturnNothing()
        {
            var doubleMaybe = Maybe.Nothing<IMaybe<object>>();
            Assert.Equal(doubleMaybe.Unwrap(), Maybe.Nothing<object>());
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool UnwrapJustShouldReturnInnerMaybe(IMaybe<object> innerMaybe)
        {
            var doubleMaybe = innerMaybe.ToMaybe();
            return doubleMaybe.Unwrap().Equals(innerMaybe);
        }

        [Fact]
        public void NothingGetValueOrDefaultReturnsDefaultTypeValue()
        {
            Assert.Equal(null, Maybe.Nothing<object>().GetValueOrDefault());
        }

        [Property]
        public bool JustGetValueOrDefaultReturnsTheValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().GetValueOrDefault() == o.Item;
        }

        [Fact]
        public void NothingGetValueOrDefaultWithSuppliedValueReturnsTheSuppliedValue()
        {
            var expected = new object();
            Assert.Equal(expected, Maybe.Nothing<object>().GetValueOrDefault(expected));
        }

        [Property]
        public bool JustGetValueOrDefaultWithSuppliedValueReturnsJustTheValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().GetValueOrDefault(new object()) == o.Item;
        }
    }
}
