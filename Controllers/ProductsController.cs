using Microsoft.AspNetCore.Mvc;
using ProductManagement.DTOs;
using ProductManagement.Services;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        public ProductsController(IProductService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _svc.GetAllAsync(HttpContext.RequestAborted);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _svc.GetByIdAsync(id, HttpContext.RequestAborted);
            if (p == null) return Ok(p);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _svc.CreateAsync(dto, HttpContext.RequestAborted);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (id != dto.Id) return BadRequest("Id mismatch");
            try
            {
                await _svc.UpdateAsync(dto, HttpContext.RequestAborted);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _svc.DeleteAsync(id, HttpContext.RequestAborted);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}