using Dapper;
using Microsoft.Extensions.Configuration;
using StoreManagement.Models.Enums;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfiguration _configuration;

        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateLog(LogEnums.TableEnum table, LogEnums.ActionEnum action, int userId)
        {
            var sql = "INSERT INTO Log(logDescription, userId) Values(@logDescription, @userId);";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var logDescription = "";
                if(action == LogEnums.ActionEnum.Create)
                {
                    logDescription += "INSERT into ";
                }
                else if (action == LogEnums.ActionEnum.Delete)
                {
                    logDescription += "DELETE from ";
                }
                else if (action == LogEnums.ActionEnum.Read)
                {
                    logDescription += "SELECT from ";
                }
                else if (action == LogEnums.ActionEnum.Update)
                {
                    logDescription += "UPDATE in ";
                }

                if(table == LogEnums.TableEnum.Category)
                {
                    logDescription += "Category table";
                }
                else if (table == LogEnums.TableEnum.Product)
                {
                    logDescription += "Products table";
                }

                var affectedRows = await connection.ExecuteAsync(sql, new { logDescription = logDescription, userId = userId });
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
