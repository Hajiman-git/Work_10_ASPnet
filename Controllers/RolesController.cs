using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.Role;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var role = await _service.GetByIdAsync(id, ct);
            if (role is null)
                return NotFound();

            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(
            [FromBody] CreateRoleDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var role = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = role.IdRole }, role);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleDto>> Update(
            int id,
            [FromBody] UpdateRoleDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var role = await _service.UpdateAsync(id, dto, ct);
                if (role is null)
                    return NotFound();

                return Ok(role);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<RoleDto>> Patch(
            int id,
            [FromBody] PatchRoleDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var role = await _service.PatchAsync(id, dto, ct);
                if (role is null)
                    return NotFound();

                return Ok(role);
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