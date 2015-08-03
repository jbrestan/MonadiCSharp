using System;
using System.Collections.Generic;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using MonadiCSharp.EitherImplementation;

namespace MonadiCSharp.Tests
{
    using Arbitraries;
    using static Assert;
    using static Either;

    public class EitherProperties
    {
        [Property]
        public bool RightConstructsRightChoice(object o) =>
            Right<object, object>(o) is Right<object, object>;
        
        [Property]
        public bool LeftConstructsLeftChoice(object o) =>
            Left<object, object>(o) is Left<object, object>;

        [Property(Arbitrary = new[] { typeof(RightArbitrary) })]
        public bool RightShouldBeMatchedByOnRight(IEither<object, object> right) =>
            right.Match(_ => false, _ => true);
        
        [Property(Arbitrary = new[] { typeof(RightArbitrary) })]
        public void MatchShouldThrowWhenOnRightIsNull(IEither<object, object> right)
        {
            Throws<ArgumentNullException>(() => right.Match(_ => false, null));
        }

        [Property(Arbitrary = new[] { typeof(LeftArbitrary) })]
        public bool LeftShouldBeMatchedByOnLeft(IEither<object, object> left) =>
            left.Match(_ => true, _ => false);

        [Property(Arbitrary = new[] { typeof(LeftArbitrary) })]
        public void MatchShouldThrowWhenOnLeftIsNull(IEither<object, object> left)
        {
            Throws<ArgumentNullException>(() => left.Match(null, _ => false));
        }

        [Property]
        public bool RightEqualsRightIfTheirValuesAreEqual(object o) =>
            Right<object, object>(o).Equals(Right<object, object>(o));

        [Property]
        public bool LeftEqualsLeftIfTheirValuesAreEqual(object o) =>
            Left<object, object>(o).Equals(Left<object, object>(o));

        [Property]
        public bool RightDoesNotEqualLeft(object o) =>
            !Right<object, object>(o).Equals(Left<object, object>(o));

        [Property]
        public bool LeftDoesNotEqualRight(object o) =>
            !Left<object, object>(o).Equals(Right<object, object>(o));

        [Property]
        public bool RightEqualsRightAsObjectIfTheirValuesAreEqual(object o) =>
            Right<object, object>(o).Equals((object)Right<object, object>(o));

        [Property]
        public bool LeftEqualsLeftAsObjectIfTheirValuesAreEqual(object o) =>
            Left<object, object>(o).Equals((object)Left<object, object>(o));

        [Property]
        public bool LeftDoesNotEqualRightAsObject(object o) =>
            !Left<object, object>(o).Equals((object)Right<object, object>(o));

        [Property]
        public bool RightDoesNotEqualLeftAsObject(object o) =>
            !Right<object, object>(o).Equals((object)Left<object, object>(o));

        [Property]
        public bool RightReturnTheSameHashCodeAsItsValueIfNotNull(NonNull<object> o) =>
            Right<object, object>(o.Item).GetHashCode() == o.Item.GetHashCode();

        [Fact]
        public bool RightReturnZeroHashCodeIfValueIsNull() =>
            Right<object, object>(null).GetHashCode() == 0;

        [Property]
        public bool LeftReturnTheSameHashCodeAsItsValueIfNotNull(NonNull<object> o) =>
            Left<object, object>(o.Item).GetHashCode() == o.Item.GetHashCode();

        [Fact]
        public bool LeftReturnZeroHashCodeIfValueIsNull() =>
            Left<object, object>(null).GetHashCode() == 0;

        [Fact]
        public void EitherImplementsIEnumerableOfRight()
        {
            IsAssignableFrom<IEnumerable<string>>(Right<int, string>(""));
        }

        [Property]
        public bool RightBehavesLikeSingletonSequenceOfItsValue(object o) =>
            o == Right<object, object>(o).Single();

        [Property(Arbitrary = new[] { typeof(LeftArbitrary) })]
        public void LeftBehavesLikeEmptySequence(IEither<object,object> left) =>
            Equal(Enumerable.Empty<object>(), left);
    }
}
