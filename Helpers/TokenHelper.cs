using Microsoft.IdentityModel.Tokens;
using MongoDBDemo.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MongoDBDemo.Helpers
{
    public class TokenHelper
    {
        IConfiguration _configuration = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();
        public string GenerateToken(User u)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secret"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Role","Admin"),
                new Claim("Email",u.email)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:issuer"],
                audience: _configuration["JWT:audience"],
                claims:claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
