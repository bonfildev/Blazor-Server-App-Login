using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazor_Server_App_Login.Login;
using Blazor_Server_App_Login.Data;
using Microsoft.EntityFrameworkCore;
using Blazor_Server_App_Login.Models;

namespace Blazor_Server_App_Login.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsersLogin login)
        {
            SessionState sessionState = new SessionState();

            if (login.Email != null)
            {
                if (login.Password != null)
                {
                    var user = await _context.UsersLogin.Where(a => a.Email.Equals(login.Email) && a.Password.Equals(login.Password)).FirstOrDefaultAsync();
                    //var user = await _context.UserLogins.FindAsync(login.email, login.password);
                    if (user != null)
                    {
                        sessionState.Name = "Admin";
                        sessionState.email = login.Email;
                        sessionState.Profile = "Admin";
                    }
                }
            }
            return StatusCode(StatusCodes.Status200OK, sessionState);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UsersLogin register)
        {

            if (register.Email != null && register.Password != null )
            {
                if (ModelState.IsValid)
                {
                    _context.Add(register);
                    await _context.SaveChangesAsync();
                    return StatusCode(StatusCodes.Status200OK);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
