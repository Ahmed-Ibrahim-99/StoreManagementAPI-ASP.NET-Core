using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreManagement.Helpers;
using StoreManagement.Models;
using StoreManagement.Models.Dtos;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _lRepo;
        private readonly IUserRepository _uRepo;
        private readonly AppSettings _appSettings; 
        public LoginController(ILoginRepository lRepo, IUserRepository uRepo, IOptions<AppSettings> appSettings)
        {
            _lRepo = lRepo;
            _uRepo = uRepo;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto data)
        {
            string email = data.email;
            string password = data.password;

            var success = await _lRepo.CheckCredentials(email, password);
            if(success)
            {
                var user = await _uRepo.GetUserByEmail(email);
                var token = generateJwtToken(user);

                return Ok(new AuthenticationDto(user, token));
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Wrong Email/Password");
            }
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
