using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class InvelidDataException : VerbalizedException
    {
        public InvelidDataException(Exception? innerException, string message) :
            base(innerException, ErrorCodes.AuthException, message)
        { }
        public InvelidDataException(string message) :
            base(null, ErrorCodes.AuthException, message)
        { }
    }
}
