namespace IdentityService.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        // Foreign key to the User entity

        //Mistake 1 : Same type of PK and FK
        public int UserId { get; set; } 


        public User User { get; set; } = null!;

        public string Token { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        // Mistake 2 (Use Null Coalescing) : Nullable DateTime for RevokedAt
        public DateTime? RevokedAt { get; set; }

        public bool IsRevoked { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public bool IsActive =>  !IsRevoked && !IsExpired;

    }
}
