using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BSS.DishDepot.Domain.Interfaces
{
    public interface ITokenService
    {
        string GetToken(List<Claim> claims);

        bool TryParseToken(string token, [NotNullWhen(true)] out JwtSecurityToken? jwtToken);
    }
}
