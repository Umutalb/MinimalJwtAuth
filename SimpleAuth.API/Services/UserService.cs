using SimpleAuth.API.Entities;
using SimpleAuth.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace SimpleAuth.API.Services
{
    public class UserService : IUserService
    {
        // In-memory user list
        private readonly List<User> _users = new();

        // Finding users by email
        public User? GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public User Register(string email, string password)
        {
            // If the same email exists, error
            if (GetByEmail(email) != null)
                throw new Exception("Email is already registered.");

            // Create a new user
            var user = new User
                {
                Email = email,
                PasswordHash = HashPassword(password)
                };

            // Add to list
            _users.Add(user);

            return user;
        }

        // Login verification
        public User? Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
                return null;

            // Is the password correct?
            if (user.PasswordHash != HashPassword(password))
                return null;

            return user;
        }

        // Password hashing (simple SHA256)
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
