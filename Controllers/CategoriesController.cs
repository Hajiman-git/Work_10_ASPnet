using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.Category;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var category = await _service.GetByIdAsync(id, ct);
            if (category is null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(
            [FromBody] CreateCategoryDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var category = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = category.IdCategory }, category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Update(
            int id,
            [FromBody] UpdateCategoryDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var category = await _service.UpdateAsync(id, dto, ct);
                if (category is null)
                    return NotFound();

                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CategoryDto>> Patch(
            int id,
            [FromBody] PatchCategoryDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var category = await _service.PatchAsync(id, dto, ct);
                if (category is null)
                    return NotFound();

                return Ok(category);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}