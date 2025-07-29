using Microsoft.AspNetCore.Mvc;
using RentCars.Api.DTOs.Empleado;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase

    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpleadoResponse>>> GetAll()
        {
            var empleados = await _empleadoService.GetAllAsync();

            return Ok(empleados.Select(e => new EmpleadoResponse
            {
                EmpleadoId = e.EmpleadoId,
                Nombre_Completo = e.Nombre_Completo,
                Cargo = e.Cargo,
                DNI = e.DNI,
                Telefono = e.Telefono,
                Email = e.Email,
                Sucursal = e.Sucursal,
                FechaAlta = e.FechaAlta,
                Activo = e.Activo
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmpleadoResponse>> GetById(int id)
        {
            var empleado = await _empleadoService.GetByIdAsync(id);
            if (empleado == null) return NotFound();

            return Ok(new EmpleadoResponse
            {
                EmpleadoId = empleado.EmpleadoId,
                Nombre_Completo = empleado.Nombre_Completo,
                Cargo = empleado.Cargo,
                DNI = empleado.DNI,
                Telefono = empleado.Telefono,
                Email = empleado.Email,
                Sucursal = empleado.Sucursal,
                FechaAlta = empleado.FechaAlta,
                Activo = empleado.Activo
            });
        }

        [HttpPost]
        public async Task<ActionResult<EmpleadoResponse>> Create(EmpleadoCreateRequest dto)
        {
            try
            {
                var nuevo = new Empleado
                {
                    Nombre_Completo = dto.Nombre_Completo,
                    Cargo = dto.Cargo,
                    DNI = dto.DNI,
                    Telefono = dto.Telefono,
                    Email = dto.Email,
                    Sucursal = dto.Sucursal,
                    FechaAlta = dto.FechaAlta,
                };

                var creado = await _empleadoService.CreateAsync(nuevo);

                return CreatedAtAction(nameof(GetById), new { id = creado.EmpleadoId }, new EmpleadoResponse
                {
                    Nombre_Completo = creado.Nombre_Completo,
                    Cargo = creado.Cargo,
                    DNI = creado.DNI,
                    Telefono = creado.Telefono,
                    Email = creado.Email,
                    Sucursal = creado.Sucursal,
                    FechaAlta = creado.FechaAlta,
                    Activo = creado.Activo
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoUpdateRequest dto)
        {
            var existente = await _empleadoService.GetByIdAsync(id);
            if (existente == null) return NotFound();

            if (!string.IsNullOrWhiteSpace(dto.Nombre_Completo))
                existente.Nombre_Completo = dto.Nombre_Completo;

            if (!string.IsNullOrWhiteSpace(dto.Cargo))
                existente.Cargo = dto.Cargo;

            if (!string.IsNullOrWhiteSpace(dto.DNI))
                existente.DNI = dto.DNI;

            if (!string.IsNullOrWhiteSpace(dto.Telefono))
                existente.Telefono = dto.Telefono;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                existente.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Sucursal))
                existente.Sucursal = dto.Sucursal;

            var actualizado = await _empleadoService.UpdateAsync(id, existente);

            return actualizado != null
                ? NoContent()
                : StatusCode(500, "No se pudo actualizar el empleado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _empleadoService.DeleteAsync(id);

            if (!eliminado)
                return NotFound(new { mensaje = "Empleado no encontrado" });

            return Ok(new { mensaje = "Empleado desactivado correctamente" });
        }
    }
}
