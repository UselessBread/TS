using Common.Constants;

namespace Common.Exceptions
{
    public class BadRequestException : VerbalizedException
    {
        public BadRequestException(Exception? innerException, string message) :
            base(innerException, ErrorCodes.AuthException, message)
        { }
        public BadRequestException(string message) :
            base(null, ErrorCodes.AuthException, message)
        { }
    }
}
