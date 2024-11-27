namespace Blazor_Server_App_Login.Models;

public partial class UsersLogin
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? RepeatPassword { get; set; }

    public virtual ICollection<sDigito> SuperDigitos { get; set; } = new List<sDigito>();
}
