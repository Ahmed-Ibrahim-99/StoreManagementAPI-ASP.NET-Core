using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Models;
using StoreManagement.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _cRepo;
        public CategoriesController(ICategoryRepository cRepo)
        {
            _cRepo = cRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var objList = await _cRepo.GetCategories();
            return Ok(objList);
        }

        [HttpGet("byId/{cId}")]
        public async Task<IActionResult> GetCategory(int cId)
        {
            var obj = await _cRepo.GetCategory(cId);
            if (obj == null)
            {
                return NotFound();
            }
            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            var exists = await _cRepo.CategoryExistsByObj(category);
            if (exists)
            {
                return BadRequest("Duplication Not Allowed");
            }
            var success = await _cRepo.CreateCategory(category);
            if (success)
            {
                return StatusCode(StatusCodes.Status201Created, "Row Created Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            if (id != category.categoryId)
            {
                return BadRequest();
            }
            var exists = await _cRepo.CategoryExistsById(id);
            if (exists == false)
            {
                return NotFound();
            }
            var success = await _cRepo.UpdateCategory(category);
            if (success)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
