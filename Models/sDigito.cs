using System.ComponentModel.DataAnnotations.Schema;

namespace Blazor_Server_App_Login.Models
{
    public class sDigito
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public long Number { get; set; }

        public int? Result { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string? email { get; set; }

        public virtual UsersLogin? User { get; set; }
    }
}
