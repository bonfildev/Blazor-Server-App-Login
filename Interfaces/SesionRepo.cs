using Blazor_Server_App_Login.Login;
using System.Security.Claims;

namespace Blazor_Server_App_Login.Interfaces
{
    public class SesionRepo : ISessionData
    {
        public SessionDTO sessionState => GetSession();
        private readonly ClaimsIdentity _settings;
        public SesionRepo() 
        {
            
        }
		public SessionDTO GetSession()
        {
            SessionDTO session = new SessionDTO();
            //session.Name=_settings.HttpCon
            return session;
        }
    }
}
