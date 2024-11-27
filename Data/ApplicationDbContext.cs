using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Blazor_Server_App_Login.Models;

namespace Blazor_Server_App_Login.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UsersLogin> UsersLogin { get; set; } = default!;
        public DbSet<sDigito> SDigito { get; set; } = default!;

    }
    
}
