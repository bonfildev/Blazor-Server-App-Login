namespace Blazor_Server_App_Login.Login
{
    public class UserRolDTO
    {        public int IdRolUsuario { get; set; }

        public string? Descripcion { get; set; }

        public override bool Equals(object o)
        {
            var other = o as UserRolDTO;
            return other?.IdRolUsuario == IdRolUsuario;
        }
        public override int GetHashCode() => IdRolUsuario.GetHashCode();
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
