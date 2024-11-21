using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazor_Server_App_Login.Login;
using Blazor_Server_App_Login.Data;

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
        public async Task<IActionResult> Login([FromBody] UserLogin login)
        {
            SessionState sessionState = new SessionState();

            if (login.email != null)
            {
                if (login.password != null)
                {
                    var user = await _context.UserLogins.FindAsync(login.email, login.password);
                    if (user != null)
                    {
                        sessionState.Name = "Admin";
                        sessionState.email = login.email;
                        sessionState.Profile = "Admin";
                    }
                }
            }


            return StatusCode(StatusCodes.Status200OK, sessionState);
        }
    }
}
