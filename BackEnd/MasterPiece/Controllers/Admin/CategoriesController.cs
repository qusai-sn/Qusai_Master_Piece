using MasterPiece.DTO;
using MasterPiece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MasterPiece.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryTypeController : ControllerBase
    {
        private readonly ICategoryTypeService _service;

        public CategoryTypeController(ICategoryTypeService service)
        {
            _service = service;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("categories")]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto categoryDto)
        {
            var created = await _service.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = created.CategoryId }, created);
        }

        [HttpPut("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CreateCategoryDto categoryDto)
        {
            var updated = await _service.UpdateCategoryAsync(id, categoryDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("categories/{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var types = await _service.GetAllTypesAsync();
            return Ok(types);
        }

        [HttpGet("types/{id}")]
        public async Task<ActionResult<TypeDto>> GetType(int id)
        {
            var type = await _service.GetTypeByIdAsync(id);
            if (type == null) return NotFound();
            return Ok(type);
        }

        [HttpPost("types")]
        public async Task<ActionResult<TypeDto>> CreateType(CreateTypeDto typeDto)
        {
            var created = await _service.CreateTypeAsync(typeDto);
            return CreatedAtAction(nameof(GetType), new { id = created.TypeId }, created);
        }

        [HttpPut("types/{id}")]
        public async Task<ActionResult<TypeDto>> UpdateType(int id, CreateTypeDto typeDto)
        {
            var updated = await _service.UpdateTypeAsync(id, typeDto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("types/{id}")]
        public async Task<ActionResult> DeleteType(int id)
        {
            var result = await _service.DeleteTypeAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

