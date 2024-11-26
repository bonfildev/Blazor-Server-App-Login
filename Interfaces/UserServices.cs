using Blazor_Server_App_Login.Login;

namespace Blazor_Server_App_Login.Interfaces
{
    public class UserServices : IUserService
    {
        private readonly HttpClient _http;
        public UserServices(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<UserDTO>> Crear(UserDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<UserDTO>>();
            return response!;
        }

        public async Task<bool> Editar(UserDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/usuario/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<UserDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<UserDTO>> IniciarSesion(string correo, string clave)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<UserDTO>>("https://localhost:7146/api/Usuario/IniciarSesion?correo={correo}&clave={clave}");
            return result!;
        }

        public async Task<ResponseDTO<List<UserDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<UserDTO>>>("api/usuario/Lista");
            return result!;
        }
    }
}
