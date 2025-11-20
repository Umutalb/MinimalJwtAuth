namespace SimpleAuth.API.Models
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
