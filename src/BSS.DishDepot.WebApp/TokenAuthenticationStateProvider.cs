using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BSS.DishDepot.WebApp
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public TokenAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _sessionStorageService.GetItemAsync<string>("access_token");
            if (string.IsNullOrEmpty(token))
                return new AuthenticationState(_anonymous);

            var securityToken = new JwtSecurityToken(token);
            var identity = new ClaimsIdentity(securityToken.Claims);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public void AuthenticateUser(string token)
        {
            var securityToken = new JwtSecurityToken(token);
            var identity = new ClaimsIdentity(securityToken.Claims);
            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);
            NotifyAuthenticationStateChanged(Task.FromResult(state));
        }
    }
}
