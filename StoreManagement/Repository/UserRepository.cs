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
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> EmailRegisteredBefore(string email)
        {
            var sql = "SELECT * FROM UserAccount WHERE email=@email;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var results = await connection.QueryAsync<User>(sql, new { email = email });
                if(results.ToList<User>().Count > 0)
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
