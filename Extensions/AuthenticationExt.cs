using Blazor_Server_App_Login.Data;
using Blazor_Server_App_Login.Login;
using Blazor_Server_App_Login.Pages;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blazor_Server_App_Login.Extensions
{
    public class AuthenticationExt : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly ApplicationDbContext _context;
        private ClaimsPrincipal _principal = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationExt(ISessionStorageService sessionStorage, ApplicationDbContext context)
        {
            _sessionStorageService = sessionStorage;
            _context = context;
        }
        private int GetUserID(string email)
        {
            if (email != null)
            {
                var user = _context.UsersLogin.Where(a => a.Email.Equals(email)).FirstOrDefault();
                //var user = await _context.UserLogins.FindAsync(login.email, login.password);
                if (user != null)
                {
                    return user.Id;
                    //return Int16.Parse(user.Id.ToString());
                }
                
            }
            return 0;
        }
        public async Task UdateAuthState(SessionState? sessionState)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sessionState != null)
            {
                string? userid = GetUserID(sessionState.email).ToString();
                sessionState.ID = Int16.Parse(userid);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, sessionState.ID.ToString()),
                    new Claim(ClaimTypes.Email, sessionState.email),
                    new Claim(ClaimTypes.Role, sessionState.Profile),
                    new Claim("UserID", sessionState.ID.ToString()),
                }, "JwtAuth"));
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
                    new Claim(ClaimTypes.Name, sessionUser.ID.ToString()),
                    new Claim(ClaimTypes.Email, sessionUser.email),
                    new Claim(ClaimTypes.Role, sessionUser.Profile),
                    new Claim("UserID", sessionUser.ID.ToString()),
                }, "JwtAuth"));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}
