using Microsoft.AspNetCore.Mvc;
using RentCars.Api.DTOs.Usuario;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDTO>>> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();

            var response = usuarios.Select(u => new UsuarioResponseDTO
            {
                UsuarioId = u.UsuarioId,
                NombreCompleto = u.Nombre_Completo,
                DNI = u.DNI,
                FechaNacimiento = u.FechaNacimiento,
                Telefono = u.Telefono,
                Email = u.Email,
                Pais = u.Pais,
                Direccion = u.Direccion,
                FechaRegistro = u.FechaRegistro,
                Activo = u.Activo
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetById(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            var response = new UsuarioResponseDTO
            {
                UsuarioId = usuario.UsuarioId,
                NombreCompleto = usuario.Nombre_Completo,
                DNI = usuario.DNI,
                FechaNacimiento = usuario.FechaNacimiento,
                Telefono = usuario.Telefono,
                Email = usuario.Email,
                Pais = usuario.Pais,
                Direccion = usuario.Direccion,
                FechaRegistro = usuario.FechaRegistro
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioRegisterRequest dto)

        {
            var nuevo = new Usuario
            {
                Nombre_Completo = dto.NombreCompleto,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                DNI = dto.DNI,
                FechaNacimiento = !string.IsNullOrWhiteSpace(dto.FechaNacimiento)
                    ? DateTime.Parse(dto.FechaNacimiento).Date
                    : null,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Pais = dto.Pais,
                Direccion = dto.Direccion,
                Activo = true,
                FechaRegistro = DateTime.Now

            };

            var creado = await _usuarioService.CreateAsync(nuevo);

            var response = new UsuarioResponseDTO
            {
                UsuarioId = creado.UsuarioId,
                NombreCompleto = creado.Nombre_Completo,
                DNI = creado.DNI,
                FechaNacimiento = creado.FechaNacimiento,
                Telefono = creado.Telefono,
                Email = creado.Email,
                Pais = creado.Pais,
                Direccion = creado.Direccion,
                Activo = true,
                FechaRegistro = DateTime.Now

            };

            return CreatedAtAction(nameof(GetById), new { id = creado.UsuarioId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateRequestDTO dto)
        {
            var existente = await _usuarioService.GetByIdAsync(id);
            if (existente == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.NombreCompleto))
                existente.Nombre_Completo = dto.NombreCompleto;

            if (!string.IsNullOrEmpty(dto.DNI))
                existente.DNI = dto.DNI;

            if (!string.IsNullOrEmpty(dto.FechaNacimiento))
                existente.FechaNacimiento = DateTime.Parse(dto.FechaNacimiento).Date;

            if (!string.IsNullOrEmpty(dto.Telefono))
                existente.Telefono = dto.Telefono;

            if (!string.IsNullOrEmpty(dto.Email))
                existente.Email = dto.Email;

            if (!string.IsNullOrEmpty(dto.Pais))
                existente.Pais = dto.Pais;

            if (!string.IsNullOrEmpty(dto.Direccion))
                existente.Direccion = dto.Direccion;

            if (!string.IsNullOrEmpty(dto.Password))
                existente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            if (dto.Activo != null)
                existente.Activo = dto.Activo.Value;

            var actualizado = await _usuarioService.UpdateAsync(id, existente);
            return actualizado != null ? NoContent() : StatusCode(500, "No se pudo actualizar el usuario.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            var result = await _usuarioService.DesactivarAsync(id);
            return result ? NoContent() : NotFound();
        }

        // GET: api/usuario/{id}/historial
        [HttpGet("{id}/historial")]
        public async Task<IActionResult> ObtenerHistorial(int id)
        {
            var historial = await _usuarioService.GetHistorialPorUsuarioAsync(id);
            return historial.Any() ? Ok(historial) : NotFound($"El usuario {id} no tiene alquileres registrados.");
        }

        [HttpGet("detalles/{id}")]
        public async Task<IActionResult> GetUsuarioConAlquileresYMultas(int id)
        {
            var usuario = await _usuarioService.GetUsuarioConAlquileresYMultasAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }
    }
}

