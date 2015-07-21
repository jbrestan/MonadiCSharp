using System;
using System.Diagnostics.CodeAnalysis;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp.Tests
{
    using static Assert;
    using static Maybe;

    public class MaybeExtensionsProperties
    {
        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool JustShouldBindToSuppliedFunction(IMaybe<object> just)
        {
            var wasCalled = false;
            Func<object, IMaybe<object>> f =
                wrapped => { wasCalled = true; return Just(wrapped); };
            just.Bind(f);
            return wasCalled;
        }

        [Property]
        public bool JustShouldUseTheWrappedObjectInBind(NonNull<object> o)
        {
            var usesTheSameObject = false;
            Func<object, IMaybe<object>> f =
                wrapped => { usesTheSameObject = o.Item == wrapped; return Just(wrapped); };
            Just(o.Item).Bind(f);
            return usesTheSameObject;
        }

        [Property]
        public void NothingShouldBindToNothing(Func<object, IMaybe<object>> f)
        {
            var nothing = Nothing<object>();
            IsType<Nothing<object>>(nothing.Bind(f));
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
            IsType<Nothing<object>>(o.ToMaybe());
        }
        [Fact]
        public void UnwrapNothingShouldReturnNothing()
        {
            var doubleMaybe = Nothing<IMaybe<object>>();
            Equal(doubleMaybe.Unwrap(), Nothing<object>());
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
            Equal(null, Nothing<object>().GetValueOrDefault());
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
            Equal(expected, Nothing<object>().GetValueOrDefault(expected));
        }

        [Property]
        public bool JustGetValueOrDefaultWithSuppliedValueReturnsJustTheValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().GetValueOrDefault(new object()) == o.Item;
        }
    }
}
