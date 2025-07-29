using Microsoft.AspNetCore.Mvc;
using RentCars.Api.DTOs.Pago;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagoResponse>>> GetAll()
            => Ok(await _pagoService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<PagoResponse>> Get(int id)
        {
            var pago = await _pagoService.GetByIdAsync(id);
            return pago == null ? NotFound() : Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult<PagoResponse>> Create(PagoCreateRequest request)
        {
            try
            {
                var creado = await _pagoService.CreateAsync(request);
                return CreatedAtAction(nameof(Get), new { id = creado.PagoId }, creado);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _pagoService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
