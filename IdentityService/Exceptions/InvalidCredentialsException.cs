namespace IdentityService.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException() :
            base("Invalid email and password...", StatusCodes.Status401Unauthorized)
        {
        }
    }
}
