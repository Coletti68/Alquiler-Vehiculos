using Microsoft.AspNetCore.Mvc;
using RentCars.Api.DTOs;
using RentCars.Api.DTOs.Alquiler;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlquilerController : ControllerBase
    {
        private readonly IAlquilerService _alquilerService;

        public AlquilerController(IAlquilerService alquilerService)
        {
            _alquilerService = alquilerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alquileres = await _alquilerService.GetAllAsync();
            return Ok(alquileres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alquiler = await _alquilerService.GetByIdAsync(id);
            return alquiler == null ? NotFound() : Ok(alquiler);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlquilerRegistroDTO dto)
        {
            if (!dto.AceptoTerminos)
            {
                return BadRequest(new { error = "Debe aceptar los términos y condiciones para realizar un alquiler." });
            }

            var alquiler = new Alquiler
            {
                UsuarioId = dto.UsuarioId,
                VehiculoId = dto.VehiculoId,
                EmpleadoId = dto.EmpleadoId,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = dto.Estado,
                Pagos = new List<Pago>(),
                Multas = new List<Multa>(),
                AceptoTerminos = dto.AceptoTerminos
            };

            try
            {
                var creado = await _alquilerService.CreateAsync(alquiler);
                return CreatedAtAction(nameof(GetById), new { id = creado.AlquilerId }, creado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlquilerUpdate dto)
        {
            var actualizado = await _alquilerService.UpdateAsync(id, dto);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpPut("finalizar/vencidos")]
        public async Task<IActionResult> FinalizarAutomáticos()
        {
            var finalizados = await _alquilerService.FinalizarAlquileresVencidosAsync();
            return Ok($"{finalizados} alquileres fueron finalizados automáticamente.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _alquilerService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("usuario/{id}/resumen")]
        public async Task<IActionResult> GetResumenPorCliente(int id)
        {
            var alquileres = await _alquilerService.GetByUsuarioIdAsync(id);

            var resumen = alquileres.Select(a => new AlquilerResponse
            {
                AlquilerId = a.AlquilerId,
                Vehiculo = $"{a.Vehiculo.Marca} {a.Vehiculo.Modelo} ({a.Vehiculo.Placa})",
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                Total = a.Total,
                Estado = a.Estado,
                AceptoTerminos = a.AceptoTerminos
            }).ToList();

            return Ok(resumen);
        }

        // GET: api/Alquiler/usuario/{usuarioId}
        [HttpGet("usuario/{id}")]
        public async Task<IActionResult> GetByClienteId(int id)
        {
            var result = await _alquilerService.GetByUsuarioIdAsync(id);
            return Ok(result);
        }

        // GET: api/Alquiler/estado/{estado}
        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            var estadosValidos = new[] { "Activo", "Cancelado", "Finalizado" };
            if (!estadosValidos.Contains(estado))
                return BadRequest("Estado inválido. Los valores permitidos son: Activo, Cancelado, Finalizado.");

            var result = await _alquilerService.GetByEstadoAsync(estado);
            return Ok(result);
        }

        // GET: api/Alquiler/rango?desde=2025-01-01&hasta=2025-12-31
        [HttpGet("rango")]
        public async Task<IActionResult> GetByRangoFechas(DateTime desde, DateTime hasta)
        {

            if (hasta < desde)
                return BadRequest("La fecha 'hasta' debe ser mayor o igual a la fecha 'desde'.");

            var result = await _alquilerService.GetByRangoFechasAsync(desde, hasta);
            return Ok(result);
        }

        // GET: api/Alquiler/activos
        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos([FromQuery] int? id)
        {
            var alquileres = await _alquilerService.GetAlquileresActivosAsync(id);
            return Ok(alquileres);
        }

        // GET: api/Alquiler/vencidos
        [HttpGet("vencidos")]
        public async Task<IActionResult> GetVencidos()
        {
            var result = await _alquilerService.GetAlquileresVencidosAsync();
            return Ok(result);
        }

        // PUT: api/Alquiler/finalizar/{id}
        [HttpPut("finalizar/{id}")]
        public async Task<IActionResult> Finalizar(int id, [FromQuery] DateTime FechaFin)
        {
            if (FechaFin == default)
                return BadRequest("Se requiere una fecha de devolución válida.");

            var result = await _alquilerService.FinalizarAlquilerAsync(id, FechaFin);
            return result ? Ok() : NotFound();
        }

        // PUT: api/Alquiler/cancelar/{id}
        [HttpPut("cancelar/{id}")]
        public async Task<IActionResult> Cancelar(int id)
        { 
            var result = await _alquilerService.CancelarAlquilerAsync(id);
            if (!result)
                return BadRequest("No se puede cancelar el alquiler. Puede que ya esté finalizado o cancelado.");

            return Ok();
        }

        // GET: api/Alquiler/disponible?vehiculoId=1&desde=2025-07-01&hasta=2025-07-05
        [HttpGet("disponible")]
        public async Task<IActionResult> VerificarDisponibilidad(int vehiculoId, DateTime desde, DateTime hasta)
        {
            if (desde > hasta)
                return BadRequest("La fecha 'desde' debe ser anterior o igual a la fecha 'hasta'.");

            var disponible = await _alquilerService.VerificarDisponibilidadVehiculoAsync(vehiculoId, desde, hasta);
            return Ok(disponible);
        }

        // GET: api/Alquiler/vehiculo/5
        [HttpGet("vehiculo/{vehiculoId}")]
        public async Task<IActionResult> GetPorVehiculo(int vehiculoId)
        {
            var result = await _alquilerService.GetAlquileresPorVehiculoAsync(vehiculoId);

            if (result == null || !result.Any())
                return NotFound($"No se encontraron alquileres para el vehículo con ID {vehiculoId}.");
            return Ok(result);
        }

        // GET: api/Alquiler/cliente/5/total
        [HttpGet("usuario/{id}/total")]
        public async Task<IActionResult> ContarPorUsuario(int id)
        {
            var count = await _alquilerService.ContarAlquileresPorUsuarioAsync(id);
            return Ok(count);
        }

        // GET: api/Alquiler/facturado?desde=2025-01-01&hasta=2025-12-31
        [HttpGet("facturado")]
        public async Task<IActionResult> CalcularTotal(DateTime desde, DateTime hasta)
        {
            if (hasta < desde)
                return BadRequest("La fecha 'hasta' debe ser mayor o igual a la fecha 'desde'.");
            var total = await _alquilerService.CalcularTotalFacturadoAsync(desde, hasta);
            return Ok(total);
        }

        // GET: api/Alquiler/detallado
        [HttpGet("detallado")]
        public async Task<IActionResult> GetAlquileresDetallados()
        {
            var alquileres = await _alquilerService.GetAllDetalladoAsync();

            var result = alquileres.Select(a => new AlquilerDTO
            {
                AlquilerId = a.AlquilerId,
                NombreUsuario = a.Usuario?.Nombre_Completo ?? "Sin datos",
                NombreVehiculo = a.Vehiculo != null ? $"{a.Vehiculo.Marca} {a.Vehiculo.Modelo}" : "Sin vehículo",
                NombreEmpleado = a.Empleado?.Nombre_Completo ?? "Sin asignar",
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                FechaDevolucion = a.FechaFin,
                Total = a.Total,
                Estado = a.Estado
            }).ToList();

            return Ok(result);
        }
    }
}

