using System;

namespace OnionArchitecture.Core.Infrastructure.Exceptions
{
    public class BusinessException : ExceptionBase
    {
        public BusinessException(string message) :
            base(message)
        {
        }

        public BusinessException(string message, Exception inner) :
            base(message, inner)
        {
        }
    }
}
