using System;
using System.Globalization;
using FsCheck.Xunit;
using Xunit;

namespace MonadiCSharp.Tests
{
    public class MaybeLinqExtensionsProperties
    {
        [Fact]
        public void MaybeCanBeTransformedByLinqQuerySyntax()
        {
            var result = from i in 1.ToMaybe()
                         select i;

            Assert.IsAssignableFrom<IMaybe<int>>(result);
        }

        [Property(Arbitrary = new[] { typeof(MaybeArbitrary) })]
        public bool SelectTransformsTheValue(object o, Func<object, object> f)
        {
            var actual = from val in o.ToMaybe()
                         select f(val);

            var expected = o != null ? f(o).ToMaybe() : Maybe.Nothing<object>();

            return actual.Equals(expected);
        }

        [Fact]
        public void SelectShouldThrowWhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                5.ToMaybe().Select<int, int>(null);
            });
        }

        [Fact]
        public void MaybeCanBeFilteredByLinqQuerySyntax()
        {
            var result = from i in 1.ToMaybe()
                         where i > 3
                         select i;

            Assert.IsAssignableFrom<IMaybe<int>>(result);
        }

        [Property]
        public bool WhereFiltersTheValue(int i, bool predicateResult)
        {
            Func<int, bool> predicate = val => predicateResult;
            var actual = from val in i.ToMaybe()
                         where predicate(val)
                         select val;

            var expected = predicateResult ? i.ToMaybe() : Maybe.Nothing<int>();

            return actual.Equals(expected);
        }

        [Fact]
        public void WhereShouldThrowWhenPredicateIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                5.ToMaybe().Where(null);
            });
        }

        [Fact]
        public void MaybeCanBeCombinedBySelectMany()
        {
            var result = from i in 1.ToMaybe()
                         from j in 2.ToMaybe()
                         select i + j;

            Assert.IsAssignableFrom<IMaybe<int>>(result);
        }
        
        [Fact]
        public void MaybeCanBeBoundBySelectMany()
        {
            var nestedMaybe = 1.ToMaybe().ToMaybe();

            var result = nestedMaybe.SelectMany(x => x);

            Assert.IsAssignableFrom<IMaybe<int>>(result);
        }
        
        [Property(Arbitrary = new [] {typeof(MaybeArbitrary)})]
        public bool SelectManyQueryFlattensTheValue(IMaybe<object> innerMaybe)
        {
            var nestedMaybe = innerMaybe.ToMaybe();

            var actual = from inner in nestedMaybe
                         from val in inner
                         select val;

            return actual.Equals(innerMaybe);
        }
        
        [Property(Arbitrary = new [] {typeof(MaybeArbitrary)})]
        public bool SelectManyMethodBindsTheFunction(IMaybe<object> innerMaybe)
        {
            var nestedMaybe = innerMaybe.ToMaybe();

            var actual = nestedMaybe.SelectMany(x => x);

            return actual.Equals(innerMaybe);
        }

        [Fact]
        public void SelectManyBindThrowsWhenSelectorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                1.ToMaybe().ToMaybe().SelectMany<IMaybe<int>, int>(null);
            });
        }

        [Property(Arbitrary = new [] {typeof(JustArbitrary)})]
        public void SelectManyCombineThrowsWhenInnerSelectorIsNull(
            IMaybe<object> maybe, Func<object ,object, object> resultSelector)   
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                maybe.SelectMany<object, object, object>(null, (a, b) => null);
            });
        }

        [Property(Arbitrary = new[] { typeof(JustArbitrary) })]
        public void SelectManyCombineThrowsWhenResultSelectorIsNull(
            IMaybe<object> maybe,  Func<object, object, object> resultSelector)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                maybe.SelectMany<object, object, object>(v => v.ToMaybe(), null);
            });
        }
    }
}