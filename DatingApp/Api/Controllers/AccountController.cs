using Api.DataAccess;
using Api.Entities;
using Api.Interfaces;
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
        private readonly ITokenServices token;
        public AccountController(DataContext _context, ITokenServices token)
        {
            this._context = _context;
            this.token = token;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(Register register)
        {
            if (await UserExists(register.UserName))
            {
                return BadRequest("UserName is taken.");
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

        /// <summary>
        /// Dafault credential : UserName : guptanikhil801, Password: DatingApp1
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(Login login)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == login.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid User");
            }
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (user.PasswordHash[i] != computedHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            return new UserToken
            {
                UserName = user.UserName,
                Token = token.CreateToken(user)
            };
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
