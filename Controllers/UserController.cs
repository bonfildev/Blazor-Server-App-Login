using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blazor_Server_App_Login.Login;
using Blazor_Server_App_Login.Data;
using Microsoft.EntityFrameworkCore;
using Blazor_Server_App_Login.Models;
using System;
using Blazor_Server_App_Login.Pages;

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

        [HttpPost]
        [Route("SDigito")]
        public async Task<IActionResult> SDigito([FromBody] sDigito sd)
        {
            if (ModelState.IsValid)
            {
                long aux = CalcSuperDigit(sd.Number);
                sd.Result = Int16.Parse(aux.ToString());
                sd.email = null;
                sd.Date = System.DateTime.Now;
                _context.Add(sd);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        public long CalcSuperDigit(long val)
        {
            long suma = 0;
            if (val.ToString().Length == 1)
            {
                return val;
            }
            else
            {
                foreach (char c in val.ToString())
                {
                    string aux= c.ToString();
                    suma += Int16.Parse(aux);
                }
                return CalcSuperDigit(suma);
            }
        }
        [HttpPost]
        [Route("BorrarSDigito")]
        public async Task<IActionResult> BorrarSDigito([FromBody] int id)
        {
            if (ModelState.IsValid)
            {
                var persons = _context.SDigito.Where(a => a.UserId.Equals(id)).ToList();
                if (persons != null)
                {
                    foreach(sDigito s in persons)
                    {
                        _context.SDigito.Remove(s);
                    }
                }
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
