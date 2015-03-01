using System;
using System.Linq.Expressions;

namespace MonadiCSharp
{
    internal static class Ensure
    {
        /// <summary>
        /// Evaluates the <paramref name="parameterExpression"/>, throws an <see cref="ArgumentNullException"/>
        /// in case it evaluated to null. Returns the expression result value otherwise.
        /// </summary>
        /// <typeparam name="TValue">Type of the provided expression.</typeparam>
        /// <param name="parameterExpression">A <see cref="MemberExpression"/> to access the guarded value.</param>
        /// <returns>Returns the result of the provided expression. Throws if it evaluates to null.</returns>
        internal static TValue NotNull<TValue>(Expression<Func<TValue>> parameterExpression)
        {
            var possibleNull = parameterExpression.Compile().Invoke();

            if (possibleNull != null) 
            {
                return possibleNull;
            }

            var paramName = GetVariableName(parameterExpression);
            throw new ArgumentNullException(paramName);
        }

        private static string GetVariableName<TValue>(Expression<Func<TValue>> parameterExpression)
        {
            // Let it throw in case of invalid cast. The expression is supplied anyway.
            var body = (MemberExpression) parameterExpression.Body;
            return body.Member.Name;
        }
    }
}
