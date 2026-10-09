using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.Cart;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/carts")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _service;

        public CartsController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var cart = await _service.GetByIdAsync(id, ct);
            if (cart is null)
                return NotFound();

            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> Create(
            [FromBody] CreateCartDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var cart = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = cart.IdCart }, cart);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CartDto>> Update(
            int id,
            [FromBody] UpdateCartDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var cart = await _service.UpdateAsync(id, dto, ct);
                if (cart is null)
                    return NotFound();

                return Ok(cart);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartDto>> Patch(
            int id,
            [FromBody] PatchCartDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var cart = await _service.PatchAsync(id, dto, ct);
                if (cart is null)
                    return NotFound();

                return Ok(cart);
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