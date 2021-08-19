using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Models;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static StoreManagement.Models.Enums.LogEnums;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _pRepo;
        private ILogRepository _lRepo;
        
        public ProductsController(IProductRepository pRepo, ILogRepository lRepo)
        {
            _pRepo = pRepo;
            _lRepo = lRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var objList = await _pRepo.GetProducts();
            var userId = (int) HttpContext.Items["UserId"];
            await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Read, userId);
            return Ok(objList);
        }

        [HttpGet("byCat/{cName}")]
        public async Task<IActionResult> GetProductsByCategory(string cName)
        {
            var objList = await _pRepo.GetProductsInCategory(cName);
            var userId = (int)HttpContext.Items["UserId"];
            await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Read, userId);
            return Ok(objList);
        }

        [HttpGet("byId/{pId}")]
        public async Task<IActionResult> GetProduct(int pId)
        {
            var obj = await _pRepo.GetProduct(pId);
            var userId = (int)HttpContext.Items["UserId"];
            await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Read, userId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var exists = await _pRepo.ProductExistsByObj(product);
            if(exists)
            {
                return BadRequest("Duplication Not Allowed");
            }
            var success = await _pRepo.CreateProduct(product);
            if(success)
            {
                var userId = (int)HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Create, userId);
                return StatusCode(StatusCodes.Status201Created, "Row Created Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if(product == null)
            {
                return BadRequest();
            }
            if(id != product.productId)
            {
                return BadRequest();
            }
            var exists = await _pRepo.ProductExistsById(id);
            if(exists == false)
            {
                return NotFound();
            }
            var success = await _pRepo.UpdateProduct(product);
            if(success)
            {
                var userId = (int) HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Update, userId);
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var exists = await _pRepo.ProductExistsById(id);
            if (exists == false)
            {
                return NotFound();
            }
            var success = await _pRepo.DeleteProduct(id);
            if (success)
            {
                var userId = (int)HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Product, ActionEnum.Delete, userId);
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
