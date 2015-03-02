using System;
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
        [Fact]
        public void ShouldConstructNothingFromNothing()
        {
            Assert.IsType<Nothing<object>>(Maybe.Nothing<object>());
        }

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
        public bool JustShouldBeMatchedByOnValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().Match(wrapped => wrapped == o.Item, () => false);
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
    }
}
