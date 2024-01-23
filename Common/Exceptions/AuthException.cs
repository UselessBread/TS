using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class AuthException : VerbalizedException
    {
        public AuthException(Exception? innerException, string message) :
            base(innerException, ErrorCodes.AuthException, message)
        { }
        public AuthException(string message) :
            base(null, ErrorCodes.AuthException, message)
        { }
    }
}
