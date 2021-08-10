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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConfiguration _configuration;
        public CategoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateCategory(Category category)
        {
            var sql = "INSERT INTO Category(categoryName, categoryDescription) Values(@categoryName, @categoryDescription);";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, category);
                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var sql = "DELETE FROM Category WHERE categoryId = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                if (affectedRows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<Category> GetCategory(int pId)
        {
            var sql = "SELECT * FROM Category WHERE categoryId = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Id = pId });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var sql = "SELECT * FROM Category;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var categories = await connection.QueryAsync<Category>(sql);
                return categories;
            }
        }

        public async Task<bool> CategoryExistsById(int id)
        {
            var sql = "SELECT * FROM Category WHERE categoryId = @Id;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, new { Id = id });
                if (result.ToList<Category>().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> CategoryExistsByObj(Category category)
        {
            var sql = "SELECT * FROM Category WHERE categoryName = @categoryName;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Category>(sql, category);
                if (result.ToList<Category>().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var sql = "UPDATE Category SET categoryName = @categoryName, categoryDescription = @categoryDescription WHERE categoryId = @categoryId;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, category);
                if (affectedRows > 0)
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
