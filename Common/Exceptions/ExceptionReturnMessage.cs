using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ExceptionReturnMessage
    {
        public string Message { get; private set; }
        public ErrorCodes ErrorCode { get; private set; }

        public ExceptionReturnMessage(ErrorCodes errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public int GetHttpStatusCode() => ErrorCode switch
        {
            ErrorCodes.AuthException => 401,
            ErrorCodes.EntityNotFoundException => 404,
            _ => 500
        };
    }
}
