using Blazor_Server_App_Login.Login;

namespace Blazor_Server_App_Login.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDTO<List<UserDTO>>> Lista();
        Task<ResponseDTO<UserDTO>> IniciarSesion(string correo, string clave);
        Task<ResponseDTO<UserDTO>> Crear(UserDTO entidad);
        Task<bool> Editar(UserDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
