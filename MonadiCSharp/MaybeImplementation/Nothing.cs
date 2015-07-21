using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.MaybeImplementation
{
    using static Ensure;

    internal sealed class Nothing<TValue> : IMaybe<TValue>, IEquatable<Nothing<TValue>>
    {
        internal static Nothing<TValue> Value { get; } = new Nothing<TValue>();

        private Nothing() { }

        public TResult Match<TResult>(Func<TValue, TResult> onValue, Func<TResult> otherwise) =>
            NotNull(() => otherwise).Invoke();

        #region Equality members and IEquatable implementation...
        public bool Equals(Nothing<TValue> other) => true;

        public bool Equals(IMaybe<TValue> other) => other is Nothing<TValue>;

        public override bool Equals(object obj) => obj is Nothing<TValue>;

        public override int GetHashCode() => 0;
        #endregion

        #region IEnumerable implementation...
        public IEnumerator<TValue> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
        public override string ToString() => "Nothing";
    }
}
