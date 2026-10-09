using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.Order;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var order = await _service.GetByIdAsync(id, ct);
            if (order is null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(
            [FromBody] CreateOrderDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var order = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = order.IdOrder }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderDto>> Update(
            int id,
            [FromBody] UpdateOrderDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var order = await _service.UpdateAsync(id, dto, ct);
                if (order is null)
                    return NotFound();

                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<OrderDto>> Patch(
            int id,
            [FromBody] PatchOrderDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var order = await _service.PatchAsync(id, dto, ct);
                if (order is null)
                    return NotFound();

                return Ok(order);
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