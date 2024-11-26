namespace Blazor_Server_App_Login.Models
{
    public class RolUsuario
    {
        public int IdRolUsuario { get; set; }

        public string? Descripcion { get; set; }

        public bool? Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public virtual ICollection<UsersModel> Usuarios { get; } = new List<UsersModel>();
    }
}
