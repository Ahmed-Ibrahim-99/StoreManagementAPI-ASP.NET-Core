using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Helpers;
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
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _cRepo;
        private ILogRepository _lRepo;
        public CategoriesController(ICategoryRepository cRepo, ILogRepository lRepo)
        {
            _cRepo = cRepo;
            _lRepo = lRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var objList = await _cRepo.GetCategories();
            var userId = (int)HttpContext.Items["UserId"];
            await _lRepo.CreateLog(TableEnum.Category, ActionEnum.Read, userId);
            return Ok(objList);
        }

        [HttpGet("byId/{cId}")]
        public async Task<IActionResult> GetCategory(int cId)
        {
            var obj = await _cRepo.GetCategory(cId);
            var userId = (int)HttpContext.Items["UserId"];
            await _lRepo.CreateLog(TableEnum.Category, ActionEnum.Read, userId);
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
                var userId = (int)HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Category, ActionEnum.Create, userId);
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
                var userId = (int)HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Category, ActionEnum.Update, userId);
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var exists = await _cRepo.CategoryExistsById(id);
            if (exists == false)
            {
                return NotFound();
            }
            var success = await _cRepo.DeleteCategory(id);
            if (success)
            {
                var userId = (int)HttpContext.Items["UserId"];
                await _lRepo.CreateLog(TableEnum.Category, ActionEnum.Delete, userId);
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
