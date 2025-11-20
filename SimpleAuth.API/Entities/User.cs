namespace SimpleAuth.API.Entities
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
