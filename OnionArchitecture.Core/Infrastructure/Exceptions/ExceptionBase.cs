
using System;

namespace OnionArchitecture.Core.Infrastructure.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase(string message) :
            base(message)
        {
        }

        protected ExceptionBase(string message, Exception inner) :
            base(message, inner)
        {
        }
    }
}
