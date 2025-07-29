using Microsoft.AspNetCore.Mvc;
using RentCars.Api.DTOs.Vehiculo;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;

        public VehiculosController(IVehiculoService vehiculoService)
        {
            _vehiculoService = vehiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoListItem>>> GetAll()
        {
            var vehiculos = await _vehiculoService.GetAllAsync();

            var result = vehiculos.Select(v => new VehiculoListItem
            {
                VehiculoId = v.VehiculoId,
                Marca = v.Marca,
                Modelo = v.Modelo,
                Placa = v.Placa,
                Estado = v.Estado,
                Color = v.Color,
                PrecioPorDia = v.PrecioPorDia
            });

            return Ok(result);
        }

        [HttpGet("listado")]
        public async Task<IActionResult> GetListadoDetallado()
        {
            var vehiculos = await _vehiculoService.GetAllAsync(); // o similar si usás EF directamente

            var dtoList = vehiculos.Select(v => new VehiculoListadoDTO
            {
                VehiculoId = v.VehiculoId,
                Marca = v.Marca,
                Modelo = v.Modelo,
                Anio = v.Anio,
                Placa = v.Placa,
                Tipo = v.Tipo,
                Color = v.Color,
                PrecioPorDia = v.PrecioPorDia,
                Estado = v.Estado
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehiculoResponse>> GetById(int id)
        {
            var vehiculo = await _vehiculoService.GetByIdAsync(id);
            if (vehiculo == null) return NotFound();

            var result = new VehiculoResponse
            {
                VehiculoId = vehiculo.VehiculoId,
                Marca = vehiculo.Marca,
                Modelo = vehiculo.Modelo,
                Anio = vehiculo.Anio,
                Placa = vehiculo.Placa,
                Color = vehiculo.Color,
                Tipo = vehiculo.Tipo,
                PrecioPorDia = vehiculo.PrecioPorDia,
                Estado = vehiculo.Estado
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<VehiculoResponse>> Create([FromBody] VehiculoCreateRequest dto)
        {
            try
            {
                var nuevo = new Vehiculo
                {
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Anio = dto.Anio,
                    Placa = dto.Placa,
                    Color = dto.Color,
                    Tipo = dto.Tipo,
                    PrecioPorDia = dto.PrecioPorDia,
                    Estado = dto.Estado
                };

                var creado = await _vehiculoService.CreateAsync(nuevo);

                var result = new VehiculoResponse
                {
                    VehiculoId = creado.VehiculoId,
                    Marca = creado.Marca,
                    Modelo = creado.Modelo,
                    Anio = creado.Anio,
                    Placa = creado.Placa,
                    Color = creado.Color,
                    Tipo = creado.Tipo,
                    PrecioPorDia = creado.PrecioPorDia,
                    Estado = creado.Estado
                };

                return CreatedAtAction(nameof(GetById), new { id = result.VehiculoId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehiculoResponse>> Update(int id, [FromBody] VehiculoUpdateRequest dto)
        {
            var update = new Vehiculo
            {
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Anio = dto.Anio,
                Placa = dto.Placa,
                Color = dto.Color,
                Tipo = dto.Tipo,
                PrecioPorDia = dto.PrecioPorDia,
                Estado = dto.Estado
            };

            var actualizado = await _vehiculoService.UpdateAsync(id, update);
            if (actualizado == null) return NotFound();

            var result = new VehiculoResponse
            {
                VehiculoId = actualizado.VehiculoId,
                Marca = actualizado.Marca,
                Modelo = actualizado.Modelo,
                Anio = actualizado.Anio,
                Placa = actualizado.Placa,
                Color = actualizado.Color,
                Tipo = actualizado.Tipo,
                PrecioPorDia = actualizado.PrecioPorDia,
                Estado = actualizado.Estado
            };

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _vehiculoService.DeleteAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }

        // PATCH: api/vehiculo/{id}/estado
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] string nuevoEstado)
        {
            var result = await _vehiculoService.CambiarEstadoAsync(id, nuevoEstado);
            return result ? NoContent() : BadRequest("No se pudo actualizar el estado del vehículo.");
        }
    }
}
