namespace IdentityService.Exceptions
{
    public class UserAlreadyExistsException : AppException
    {
        public UserAlreadyExistsException()
            : base("User Already exists..",StatusCodes.Status409Conflict)
        {

        }
    }
}
