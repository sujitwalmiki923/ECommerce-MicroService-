using System.ComponentModel.DataAnnotations;

namespace IdentityService.DTOs
{
    public class RegisterRequests
    {
        
        public string FirstName { get; set; } = string.Empty;

        
        public string LastName { get; set; } = string.Empty;

        
        public string Email { get; set; } = string.Empty;

        
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";  //Setting User as Default Role
    }
}
