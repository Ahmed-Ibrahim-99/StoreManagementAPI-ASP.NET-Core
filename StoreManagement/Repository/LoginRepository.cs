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
    public class LoginRepository : ILoginRepository
    {
        private readonly IConfiguration _configration;
        public LoginRepository(IConfiguration configuration)
        {
            _configration = configuration;
        }
        public async Task<bool> CheckCredentials(string email, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var sql = "SELECT * FROM UserAccount WHERE email=@email";
            using(var connection = new SqlConnection(_configration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var results = await connection.QueryAsync<User>(sql, new { email = email, passwordHash = passwordHash });
                if(results.ToList<User>().Count > 0)
                {
                    if (BCrypt.Net.BCrypt.Verify(password,results.FirstOrDefault<User>().passwordHash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
