using Api.DataAccess;
using Api.Entities;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<AppUser>> Register(Register register)
        {
            if (await UserExists(register.UserName))
            {
                return BadRequest();
            }
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),//GetBytes converts string into bytes
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        #region Private Helpers
        private async Task<bool> UserExists(string UserName)
        {
            var user = _context.Users.AnyAsync(x => x.UserName == UserName.ToLower());
            return await user;
        }
        #endregion
    }
}
