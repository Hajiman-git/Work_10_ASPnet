using Microsoft.AspNetCore.Mvc;
using work_10.Dtos.StockReservation;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/stockreservations")]
    public class StockReservationsController : ControllerBase
    {
        private readonly IStockReservationService _service;

        public StockReservationsController(IStockReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockReservationDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockReservationDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var reservation = await _service.GetByIdAsync(id, ct);
            if (reservation is null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpGet("by-product/{productId:int}")]
        public async Task<ActionResult<IEnumerable<StockReservationDto>>> GetByProductId(
            int productId,
            CancellationToken ct = default)
        {
            var reservations = await _service.GetByProductIdAsync(productId, ct);
            return Ok(reservations);
        }
    }
}