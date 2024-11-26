using Blazor_Server_App_Login.Models;
using System.Linq.Expressions;

namespace Blazor_Server_App_Login.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Task<List<UsersModel>> Lista();
        Task<UsersModel> Obtener(Expression<Func<UsersModel, bool>> filtro = null);
        Task<UsersModel> Crear(UsersModel entidad);
        Task<bool> Editar(UsersModel entidad);
        Task<bool> Eliminar(UsersModel entidad);
        Task<IQueryable<UsersModel>> Consultar(Expression<Func<UsersModel, bool>> filtro = null);
    }
}

