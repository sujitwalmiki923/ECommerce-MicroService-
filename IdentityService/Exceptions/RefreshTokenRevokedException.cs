namespace IdentityService.Exceptions
{
    public class RefreshTokenRevokedException : AppException
    {
        public RefreshTokenRevokedException() :
            base("Refresh Roken Revoked...",StatusCodes.Status401Unauthorized)
        { }
    }
}
