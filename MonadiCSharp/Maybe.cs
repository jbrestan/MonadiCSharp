using MonadiCSharp.MaybeImplementation;

namespace MonadiCSharp
{
    public static class Maybe
    {
        public static IMaybe<TValue> Just<TValue>(TValue value) => new Just<TValue>(value);
        
        public static IMaybe<TValue> Nothing<TValue>() => MaybeImplementation.Nothing<TValue>.Value;
    }
}
