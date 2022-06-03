using Api.DataAccess;
using Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        public AccountController(DataContext _context)
        {
            this._context = _context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register (string UserName, string Password)
        {
            using var hmac =  new HMACSHA512();
            var user = new AppUser
            {
                UserName = UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password)),//GetBytes converts string into bytes
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
