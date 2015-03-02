using System;
using FsCheck;
using FsCheck.Xunit;

namespace MonadiCSharp.Tests
{
    public class MaybeMonadLaws
    {
        [Property(Arbitrary = new [] {typeof(MaybeArbitrary)})]
        public bool LeftIdentity(NonNull<object> o, Func<object, IMaybe<object>> f)
        {
            return o.Item.ToMaybe().Bind(f).Equals(f(o.Item));
        }

        [Property(Arbitrary = new[] { typeof(MaybeArbitrary) })]
        public bool RightIdentity(IMaybe<object> m)
        {
            return m.Bind(MaybeExtensions.ToMaybe).Equals(m);
        }

        [Property(Arbitrary = new[] { typeof(MaybeArbitrary) })]
        public bool Associativity(NonNull<IMaybe<object>> m, Func<object, IMaybe<object>> f, Func<object, IMaybe<object>> g)
        {
            return m.Item.Bind(f).Bind(g).Equals(m.Item.Bind(v => f(v).Bind(g)));
        }
    }
}
