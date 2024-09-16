using BSS.DishDepot.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BSS.DishDepot.Presentation.Services
{
    public class TokenService : ITokenService
    {
        // TODO: Use IOptions pattern instead
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GetToken(List<Claim> claims)
        {
            var key = _config["Token:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config["Token:Audience"],
                Issuer = _config["Token:Issuer"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool TryParseToken(string token, [NotNullWhen(true)] out JwtSecurityToken? jwtToken)
        {
            jwtToken = null;

            var key = _config["Token:Key"];
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            if (validatedToken is null)
                return false;

            jwtToken = (JwtSecurityToken)validatedToken;
            return true;
        }
    }
}
