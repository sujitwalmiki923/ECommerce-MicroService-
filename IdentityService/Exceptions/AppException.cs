namespace IdentityService.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; }

        protected AppException(string message, int statuscode) : base(message)
        {
            StatusCode = statuscode;     
        }
    }
}
