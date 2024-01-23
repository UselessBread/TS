using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class VerbalizedException : Exception
    {
        public readonly Exception? _innerException;
        public readonly ErrorCodes _errorCode;
        public readonly string _message;

        public VerbalizedException(Exception? innerException, ErrorCodes errorCode, string message) 
            : base(message, innerException)
        { 
            _innerException = innerException;
            _errorCode = errorCode;
            _message = message;
        }

        public ExceptionReturnMessage ToExceptionMessage()
        {
            return new ExceptionReturnMessage(_errorCode, _message);
        }
    }
}
