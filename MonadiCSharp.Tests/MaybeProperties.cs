using System;
using System.Diagnostics.CodeAnalysis;
using FsCheck;
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

        [Property]
        public bool JustShouldBeMatchedByOnValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().Match(wrapped => wrapped == o.Item, () => false);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void NothingShouldBeMatchedByOtherwise()
        {
            object o = null;
            Assert.True(o.ToMaybe().Match(_ => false, () => true));
        }
        
        [Fact]
        public void NothingIsEqualToNothing()
        {
            Assert.True(Maybe.Nothing<object>().Equals(Maybe.Nothing<object>()));
        }

        [Property]
        public bool JustIsEqualToJustIfTheirValuesAreEqual(NonNull<object> o)
        {
            return o.Item.ToMaybe().Equals(o.Item.ToMaybe());
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool JustIsNotEqualToNothing(NonNull<IMaybe<object>> just)
        {
            return !just.Item.Equals(Maybe.Nothing<object>());
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool UnwrapJustShouldReturnInnerMaybe(NonNull<IMaybe<object>> innerMaybe)
        {
            var doubleMaybe = innerMaybe.Item.ToMaybe();
            return doubleMaybe.Unwrap().Equals(innerMaybe.Item);
        }

        [Fact]
        public bool UnwrapNothingShouldReturnNothing()
        {
            var doubleMaybe = Maybe.Nothing<IMaybe<object>>();
            return doubleMaybe.Unwrap().Equals(Maybe.Nothing<object>());
        }
    }
}
