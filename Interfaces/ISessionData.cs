using Blazor_Server_App_Login.Login;
namespace Blazor_Server_App_Login.Interfaces
{
    public interface ISessionData
    {
        public SessionDTO sessionState { get; }
    }
}
