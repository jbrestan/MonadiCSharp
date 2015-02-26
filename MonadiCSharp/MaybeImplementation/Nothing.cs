﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadiCSharp.MaybeImplementation
{
    internal class Nothing<TValue> : IMaybe<TValue>
    {
        internal Nothing() { }

        public IMaybe<TResult> Bind<TResult>(Func<TValue, IMaybe<TResult>> f)
        {
            return new Nothing<TResult>();
        }
    }
}
