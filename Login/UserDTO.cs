using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blazor_Server_App_Login.Login
{
    public class UserDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre Completo es requerido")]
        public string? FullName { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public virtual UserRolDTO? IdRolUsuarioNavigation { get; set; }

    }
}
