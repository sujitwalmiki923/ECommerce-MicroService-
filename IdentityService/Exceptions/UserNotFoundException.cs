namespace IdentityService.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException():
            base("User Not Found.",StatusCodes.Status404NotFound)
        {
        }
    }
}
