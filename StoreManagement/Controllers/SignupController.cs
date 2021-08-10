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
    public class SignupController : ControllerBase
    {
        private ISignupRepository _sRepo;
        public SignupController(ISignupRepository sRepo)
        {
            _sRepo = sRepo;
        }

        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupDto data)
        {
            string email = data.email;
            string password = data.password;
            string fName = data.fName;
            string lName = data.lName;
            var success = await _sRepo.SignupNewUser(email, password, fName, lName);
            if(success)
            {
                return StatusCode(StatusCodes.Status201Created, "Signup Successful");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
