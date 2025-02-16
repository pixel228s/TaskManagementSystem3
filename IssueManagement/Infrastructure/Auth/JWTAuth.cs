using IssueManagement.Application.Users.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IssueManagement.Infrastructure.Auth
{
    public static class JWTAuth
    {
        public static string GenerateJwt(UserResponseModel user, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
               new(ClaimTypes.Name, user.UserName),
               new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration["Authentication:Issuer"],
                audience: _configuration["Authentication:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signInCredentials
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return tokenToReturn;
        }
    }
}
