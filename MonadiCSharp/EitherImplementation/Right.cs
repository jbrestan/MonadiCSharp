using System;
using System.Collections;
using System.Collections.Generic;

namespace MonadiCSharp.EitherImplementation
{
    using static Ensure;

    internal sealed class Right<TLeft, TRight> : IEither<TLeft, TRight>
    {
        private readonly TRight value;

        public Right(TRight value)
        {
            this.value = value;
        }

        public TResult Match<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight)
        {
            return NotNull(() => onRight).Invoke(value);
        }

        #region Equality members and IEquatable implementation...
        public bool Equals(Right<TLeft, TRight> other) =>
            other != null && ((value == null && other.value == null) || value.Equals(other.value));

        public bool Equals(IEither<TLeft, TRight> other) =>
            Equals(other as Right<TLeft, TRight>);

        public override bool Equals(object obj) =>
            Equals(obj as Right<TLeft, TRight>);
        
        public override int GetHashCode() => value?.GetHashCode() ?? 0;
        #endregion


        #region IEnumerable implementation...
        public IEnumerator<TRight> GetEnumerator()
        {
            yield return value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        public override string ToString() => $"Right [{value}]";
    }
}
