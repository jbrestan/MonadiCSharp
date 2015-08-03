using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.EitherImplementation
{
    using static Ensure;

    internal sealed class Left<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly TLeft value;

        public Left(TLeft value)
        {
            this.value = value;
        }

        public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
        {
            return NotNull(() => onLeft).Invoke(value);
        }

        #region Equality members and IEquatable implementation...
        public bool Equals(Left<TLeft, TRight> other) =>
            other != null && ((value == null && other.value == null) || value.Equals(other.value));

        public bool Equals(IEither<TLeft, TRight> other) =>
            Equals(other as Left<TLeft, TRight>);

        public override bool Equals(object obj) =>
            Equals(obj as Left<TLeft, TRight>);

        public override int GetHashCode() => value?.GetHashCode() ?? 0;
        #endregion


        #region IEnumerable implementation...
        public IEnumerator<TRight> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        public override string ToString() => $"Left [{value}]";
    }
}