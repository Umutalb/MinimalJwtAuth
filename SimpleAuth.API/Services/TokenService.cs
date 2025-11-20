using Microsoft.IdentityModel.Tokens;
using SimpleAuth.API.Entities;
using SimpleAuth.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace SimpleAuth.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey = "THIS_IS_A_DEMO_SECRET_KEY_CHANGE_IT";
        private readonly int _expirationMinutes = 30;

        public AuthResponse CreateToken(User user)
        {
            // 1) Prepare the claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // 2) Secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            // 3) Signature algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4) Token duration
            var expiration = DateTime.UtcNow.AddMinutes(_expirationMinutes);

            // 5) Token creation
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            // 6) Convert the token to a string
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponse
            {
                Token = tokenString,
                Expiration = expiration
            };
        }
    }
}
