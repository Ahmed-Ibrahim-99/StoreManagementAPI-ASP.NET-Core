using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Models.Dtos;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginRepository _lRepo;
        public LoginController(ILoginRepository lRepo)
        {
            _lRepo = lRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto data)
        {
            string email = data.email;
            string password = data.password;

            var success = await _lRepo.CheckCredentials(email, password);
            if(success)
            {
                return Ok("Login Successful");
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Wrong Email/Password");
            }
        }
    }
}
