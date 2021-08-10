﻿using Dapper;
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
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            var sql = "INSERT INTO Product(productName, productDescription, productPrice, categoryId) Values(@productName, @productDescription, @productPrice, @categoryId);";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, product);
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

        public async Task<bool> DeleteProduct(int id)
        {
            var sql = "DELETE FROM Product WHERE productId = @Id;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
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

        public async Task<Product> GetProduct(int pId)
        {
            var sql = "SELECT * FROM Product WHERE productId = @Id;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql, new { Id = pId });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var sql = "SELECT * FROM Product;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var products = await connection.QueryAsync<Product>(sql);
                return products;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsInCategory(string cName)
        {
            var sql = "SELECT [productId], [productName], [productDescription], [productPrice], [categoryId] FROM view1 WHERE categoryName = @cName;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var products = await connection.QueryAsync<Product>(sql, new { cName = cName });
                return products;
            }
        }

        public async Task<bool> ProductExistsById(int id)
        {
            var sql = "SELECT * FROM Product WHERE productId = @Id;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql, new { Id = id });
                if(result.ToList<Product>().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> ProductExistsByObj(Product product)
        {
            var sql = "SELECT * FROM Product WHERE productName = @productName;";
            using(var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Product>(sql, product);
                if(result.ToList<Product>().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var sql = "UPDATE Product SET productName = @productName, productPrice=@productPrice, productDescription = @productDescription, categoryId = @categoryId WHERE productId = @productId;";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, product);
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
