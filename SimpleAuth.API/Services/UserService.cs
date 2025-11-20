using SimpleAuth.API.Entities;
using SimpleAuth.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace SimpleAuth.API.Services
{
    public class UserService : IUserService
    {
        // In-memory kullanıcı listesi
        private readonly List<User> _users = new();

        // Email’e göre kullanıcı bulma
        public User? GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public User Register(string email, string password)
        {
            // Aynı email varsa hata
            if (GetByEmail(email) != null)
                throw new Exception("Email is already registered.");

            // Yeni kullanıcı oluştur
            var user = new User
                {
                Email = email,
                PasswordHash = HashPassword(password)
                };

            // Listeye ekle
            _users.Add(user);

            return user;
        }

        // Login doğrulama
        public User? Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
                return null;

            // Şifre doğru mu?
            if (user.PasswordHash != HashPassword(password))
                return null;

            return user;
        }

        // Şifre hash’leme (basit SHA256)
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
