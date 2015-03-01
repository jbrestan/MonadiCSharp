using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp
{
    public static class Maybe
    {
        public static IMaybe<TValue> Just<TValue>(TValue value)
        {
            return new Just<TValue>(value);
        }

        public static IMaybe<TValue> Nothing<TValue>()
        {
            return new Nothing<TValue>();
        }
    }
}
