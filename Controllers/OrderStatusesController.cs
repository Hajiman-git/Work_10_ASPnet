using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.OrderStatus;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/orderstatuses")]
    public class OrderStatusesController : ControllerBase
    {
        private readonly IOrderStatusService _service;

        public OrderStatusesController(IOrderStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderStatusDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var status = await _service.GetByIdAsync(id, ct);
            if (status is null)
                return NotFound();

            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<OrderStatusDto>> Create(
            [FromBody] CreateOrderStatusDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var status = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = status.IdStatus }, status);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderStatusDto>> Update(
            int id,
            [FromBody] UpdateOrderStatusDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var status = await _service.UpdateAsync(id, dto, ct);
                if (status is null)
                    return NotFound();

                return Ok(status);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<OrderStatusDto>> Patch(
            int id,
            [FromBody] PatchOrderStatusDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var status = await _service.PatchAsync(id, dto, ct);
                if (status is null)
                    return NotFound();

                return Ok(status);
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