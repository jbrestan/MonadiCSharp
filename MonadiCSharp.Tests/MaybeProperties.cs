﻿using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using MonadiCSharp.MaybeImplementation;
using System.Collections.Generic;
using System.Linq;

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
        public void UnwrapNothingShouldReturnNothing()
        {
            var doubleMaybe = Maybe.Nothing<IMaybe<object>>();
            Assert.Equal(doubleMaybe.Unwrap(), Maybe.Nothing<object>());
        }

        [Fact]
        public void NothingIsEqualToNothingAsObject()
        {
            Assert.True(Maybe.Nothing<object>().Equals(Maybe.Nothing<object>() as object));
        }

        [Property]
        public bool JustIsEqualToJustAsObjectIfTheirValuesAreEqual(NonNull<object> o)
        {
            return o.Item.ToMaybe().Equals(o.Item.ToMaybe() as object);
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool JustIsNotEqualToNothingAsObject(NonNull<IMaybe<object>> just)
        {
            return !just.Item.Equals(Maybe.Nothing<object>() as object);
        }

        [Fact]
        public void NothingGetHashCodeReturnsZero()
        {
            Assert.Equal(0, Maybe.Nothing<int>().GetHashCode());
        }

        [Property]
        public bool JustGetHashCodeReturnsTheValueGetHashCode(NonNull<object> o)
        {
            return o.Item.GetHashCode() == o.Item.ToMaybe().GetHashCode();
        }

        [Fact]
        public void MaybeImplementsIEnumerable()
        {
            Assert.IsAssignableFrom<IEnumerable<object>>(Maybe.Nothing<object>());
        }

        [Fact]
        public void NothingBehavesLikeEmptyCollection()
        {
            Assert.Equal(Enumerable.Empty<object>(), Maybe.Nothing<object>());
        }

        [Property]
        public bool JustBehavesLikeSingletonCollectionOfItsValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().Single() == o.Item;
        }

        [Property]
        public bool JustGetValueOrDefaultReturnsTheValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().GetValueOrDefault() == o.Item;
        }

        [Fact]
        public void NothingGetValueOrDefaultReturnsDefaultTypeValue()
        {
            Assert.Equal(null, Maybe.Nothing<object>().GetValueOrDefault());
        }

        [Property]
        public bool JustGetValueOrDefaultWithSuppliedValueReturnsJustTheValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().GetValueOrDefault(new object()) == o.Item;
        }

        [Fact]
        public void NothingGetValueOrDefaultWithSuppliedValueReturnsTheSuppliedValue()
        {
            var expected = new object();
            Assert.Equal(expected, Maybe.Nothing<object>().GetValueOrDefault(expected));
        }
    }
}
