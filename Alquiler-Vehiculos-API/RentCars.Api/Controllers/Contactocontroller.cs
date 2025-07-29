using Microsoft.AspNetCore.Mvc;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoService _contactoService;

        public ContactoController(IContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetAll()
            => Ok(await _contactoService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> Get(int id)
        {
            var contacto = await _contactoService.GetByIdAsync(id);
            return contacto == null ? NotFound() : Ok(contacto);
        }

        [HttpPost]
        public async Task<ActionResult<Contacto>> Create(Contacto contacto)
        {
            var creado = await _contactoService.CreateAsync(contacto);
            return CreatedAtAction(nameof(Get), new { id = creado.ContactoId }, creado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _contactoService.DeleteAsync(id);
            return eliminado ? NoContent() : NotFound();
        }

        [HttpPut("responder/{id}")]
        public async Task<IActionResult> MarcarComoRespondido(int id)
        {
            var actualizado = await _contactoService.MarcarComoRespondidoAsync(id);
            return actualizado == null ? NotFound() : Ok(actualizado);
        }
    }
}
