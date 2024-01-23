using Common.Constants;

namespace Common.Exceptions
{
    public class EntityNotFoundException : VerbalizedException
    {
        public EntityNotFoundException(Exception? innerException, string message) :
            base(innerException, ErrorCodes.AuthException, message)
        { }
        public EntityNotFoundException(string message) :
            base(null, ErrorCodes.AuthException, message)
        { }
    }
}
