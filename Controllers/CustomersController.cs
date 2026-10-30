using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using work_10.Authorization;
using work_10.Dtos.Customer;
using work_10.Services;

namespace work_10.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var (items, total) = await _service.GetAllAsync(page, pageSize, ct);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetById(
            int id,
            CancellationToken ct = default)
        {
            var customer = await _service.GetByIdAsync(id, ct);
            if (customer is null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create(
            [FromBody] CreateCustomerDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var customer = await _service.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = customer.IdCustomer }, customer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.SameUserOrAdmin)]
        public async Task<ActionResult<CustomerDto>> Update(
            int id,
            [FromBody] UpdateCustomerDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var customer = await _service.UpdateAsync(id, dto, ct);
                if (customer is null)
                    return NotFound();

                return Ok(customer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("{id:int}")]
        [Authorize(Policy = Policies.SameUserOrAdmin)]
        public async Task<ActionResult<CustomerDto>> Patch(
            int id,
            [FromBody] PatchCustomerDto dto,
            CancellationToken ct = default)
        {
            try
            {
                var customer = await _service.PatchAsync(id, dto, ct);
                if (customer is null)
                    return NotFound();

                return Ok(customer);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = Policies.SameUserOrAdmin)]
        public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}