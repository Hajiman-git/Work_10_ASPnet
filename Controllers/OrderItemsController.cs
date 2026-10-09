using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.OrderItem;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/orderitems")]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemService _service;

        public OrderItemsController(IOrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderItemDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var item = await _service.GetByIdAsync(id, ct);
            if (item is null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItemDto>> Create(
            [FromBody] CreateOrderItemDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var item = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = item.IdOrderItem }, item);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderItemDto>> Update(
            int id,
            [FromBody] UpdateOrderItemDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var item = await _service.UpdateAsync(id, dto, ct);
                if (item is null)
                    return NotFound();

                return Ok(item);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<OrderItemDto>> Patch(
            int id,
            [FromBody] PatchOrderItemDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var item = await _service.PatchAsync(id, dto, ct);
                if (item is null)
                    return NotFound();

                return Ok(item);
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