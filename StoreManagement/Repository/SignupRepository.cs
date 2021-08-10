using Dapper;
using Microsoft.Extensions.Configuration;
using StoreManagement.Models;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository
{
    public class SignupRepository : ISignupRepository
    {
        private readonly IConfiguration _configuration;
        IUserRepository _uRepo;
        public SignupRepository(IConfiguration configuration, IUserRepository uRepo)
        {
            _configuration = configuration;
            _uRepo = uRepo;
        }
        public async Task<bool> SignupNewUser(string email, string password, string fName, string lName)
        {
            var emailRegistered = await _uRepo.EmailRegisteredBefore(email);
            if (emailRegistered)
            {
                return false;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var sql = "INSERT INTO UserAccount(email, passwordHash, fName, lName) Values(@email, @passwordHash, @fName, @lName);";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { email=email, passwordHash=passwordHash, fName=fName, lName=lName });
                if(affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
