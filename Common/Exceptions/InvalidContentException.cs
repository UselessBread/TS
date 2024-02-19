using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvalidContentException : VerbalizedException
    {
        public InvalidContentException(Exception? innerException, string message) :
            base(innerException, ErrorCodes.AuthException, message)
        { }
        public InvalidContentException(string message) :
            base(null, ErrorCodes.AuthException, message)
        { }
    }
}
