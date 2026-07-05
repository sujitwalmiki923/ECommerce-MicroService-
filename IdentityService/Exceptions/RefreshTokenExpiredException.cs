namespace IdentityService.Exceptions
{
    public class RefreshTokenExpiredException : AppException
    {
        public RefreshTokenExpiredException() :
            base("Refresh Token Expired...",StatusCodes.Status401Unauthorized)
        { }
    }
}
