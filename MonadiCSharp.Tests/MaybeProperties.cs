using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp.Tests
{
    using Arbitraries;
    using static Assert;
    using static Maybe;

    public class MaybeProperties
    {
        [Fact]
        public void ShouldConstructNothingFromNothing()
        {
            IsType<Nothing<object>>(Nothing<object>());
        }

        [Property]
        public bool ShouldBeJustFromNonNull(NonNull<object> o)
        {
            return Just(o.Item) is Just<object>;
        }

        [Fact]
        public void ShouldThrowWhenJustCalledWithNull()
        {
            Throws<ArgumentNullException>(() => Just<object>(null));
        }

        [Fact]
        [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
        public void NothingShouldBeMatchedByOtherwise()
        {
            object o = null;
            True(o.ToMaybe().Match(_ => false, () => true));
        }
        
        [Fact]
        public void NothingIsEqualToNothing()
        {
            True(Nothing<object>().Equals(Nothing<object>()));
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
        public bool JustIsNotEqualToNothing(IMaybe<object> just)
        {
            return !just.Equals(Nothing<object>());
        }

        [Fact]
        public void NothingIsEqualToNothingAsObject()
        {
            True(Nothing<object>().Equals(Nothing<object>() as object));
        }

        [Property]
        public bool JustIsEqualToJustAsObjectIfTheirValuesAreEqual(NonNull<object> o)
        {
            return o.Item.ToMaybe().Equals(o.Item.ToMaybe() as object);
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public bool JustIsNotEqualToNothingAsObject(IMaybe<object> just)
        {
            return !just.Equals(Nothing<object>() as object);
        }

        [Fact]
        public void NothingGetHashCodeReturnsZero()
        {
            Equal(0, Nothing<int>().GetHashCode());
        }

        [Property]
        public bool JustGetHashCodeReturnsTheValueGetHashCode(NonNull<object> o)
        {
            return o.Item.GetHashCode() == o.Item.ToMaybe().GetHashCode();
        }

        [Fact]
        public void MaybeImplementsIEnumerable()
        {
            IsAssignableFrom<IEnumerable<object>>(Nothing<object>());
        }

        [Fact]
        public void NothingBehavesLikeEmptyCollection()
        {
            Equal(Enumerable.Empty<object>(), Nothing<object>());
        }

        [Property]
        public bool JustBehavesLikeSingletonCollectionOfItsValue(NonNull<object> o)
        {
            return o.Item.ToMaybe().Single() == o.Item;
        }
    }
}
