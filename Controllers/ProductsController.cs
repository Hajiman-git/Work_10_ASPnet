using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using work_10.Authorization;
using work_10.Dtos.Product;
using work_10.Dtos.StockReservation;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStockReservationService _reservationService;

        public ProductsController(
            IProductService productService,
            IStockReservationService reservationService)
        {
            _productService = productService;
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _productService.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var product = await _productService.GetByIdAsync(id, ct);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize(Policy = Policies.AdminOnly)]
        public async Task<ActionResult<ProductDto>> Create(
            [FromBody] CreateProductDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var product = await _productService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = product.IdProduct }, product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> Update(
            int id,
            [FromBody] UpdateProductDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var product = await _productService.UpdateAsync(id, dto, ct);
                if (product is null)
                    return NotFound();

                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<ProductDto>> Patch(
            int id,
            [FromBody] PatchProductDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var product = await _productService.PatchAsync(id, dto, ct);
                if (product is null)
                    return NotFound();

                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var deleted = await _productService.DeleteAsync(id, ct);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id:int}/reserve")]
        public async Task<ActionResult<StockReservationDto>> Reserve(
            int id,
            [FromBody] ReserveStockDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var reservation = await _reservationService.ReserveAsync(id, dto, ct);
                return StatusCode(StatusCodes.Status201Created, reservation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id:int}/release")]
        public async Task<IActionResult> Release(
            int id,
            [FromQuery] int customerId,
            CancellationToken ct = default)
        {
            var released = await _reservationService.ReleaseAsync(id, customerId, ct);
            if (!released)
                return NotFound();

            return NoContent();
        }
    }
}