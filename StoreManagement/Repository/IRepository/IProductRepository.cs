using StoreManagement.Models;
using StoreManagement.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<IEnumerable<ProductDto>> GetProductsInCategory(string cName);
        Task<ProductDto> GetProduct(int pId);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<bool> ProductExistsById(int id);
        Task<bool> ProductExistsByObj(Product product);
    }
}
