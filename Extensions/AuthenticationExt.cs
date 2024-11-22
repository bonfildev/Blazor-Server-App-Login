using Blazor_Server_App_Login.Login;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blazor_Server_App_Login.Extensions
{
    public class AuthenticationExt : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private ClaimsPrincipal _principal = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationExt(ISessionStorageService sessionStorage)
        {
            _sessionStorageService = sessionStorage;
        }

        public async Task UdateAuthState(SessionState? sessionState)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sessionState != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, sessionState.Name),
                    new Claim(ClaimTypes.Email, sessionState.email),
                    new Claim(ClaimTypes.Role, sessionState.Profile),
                },"JwtAuth"));
                await _sessionStorageService.SaveStorage("sessionUser", sessionState);
            }
            else
            {
                claimsPrincipal = _principal;
                await _sessionStorageService.RemoveItemAsync("sessionUser");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        /// <summary>
        /// Retrieve the information of the user authenticated
        /// </summary>
        /// <returns></returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var sessionUser = await _sessionStorageService.ObtenerStorage<SessionState>("sessionUser");

            if (sessionUser == null)
            {
                return await Task.FromResult(new AuthenticationState(_principal));
                
            }
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>{
                    new Claim(ClaimTypes.Name, sessionUser.Name),
                    new Claim(ClaimTypes.Email, sessionUser.email),
                    new Claim(ClaimTypes.Role, sessionUser.Profile),
                }, "JwtAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
