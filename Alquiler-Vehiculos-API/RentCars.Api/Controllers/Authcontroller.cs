using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCars.Api.Data;
using RentCars.Api.DTOs.Auth;
using RentCars.Api.Services.Interfaces;
using System.Security.Claims;


namespace RentCars.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest dto)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Password, usuario.PasswordHash))
                {
                    return Unauthorized(new { mensaje = "Credenciales incorrectas. Verificá tu email y contraseña." });
                }

                if (!usuario.Activo)
                {
                    return StatusCode(403, new { mensaje = "Tu cuenta está inactiva. Contactá con soporte para más información." });
                }

                var token = _tokenService.GenerarToken(usuario);

                return Ok(new LoginResponse
                {
                    Token = token,
                    Expira = DateTime.Now.AddHours(2),
                    UsuarioId = usuario.UsuarioId,
                    NombreCompleto = usuario.Nombre_Completo,
                    Email = usuario.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado. Contactá con soporte si el problema persiste." });
            }
        }

        [HttpPost("recuperar-password")]
        public async Task<IActionResult> SolicitarReset(ForgotPasswordRequest dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario == null) return NotFound("Correo no registrado.");

            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            usuario.ResetToken = token;
            usuario.ResetTokenExpira = DateTime.UtcNow.AddHours(1);
            await _context.SaveChangesAsync();

            var link = $"http://localhost:5173/reset-password?email={usuario.Email}&token={Uri.EscapeDataString(token)}";
            Console.WriteLine($"🧭 LINK: {link}");

            return Ok(new { mensaje = "Token generado", link });
        }

        [HttpPost("resetear-password")]
        public async Task<IActionResult> Resetear(ResetPasswordRequest dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u =>
                u.Email == dto.Email &&
                u.ResetToken == dto.Token &&
                u.ResetTokenExpira > DateTime.UtcNow);

            if (usuario == null)
                return BadRequest("Token inválido o expirado.");

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NuevaPassword);
            usuario.ResetToken = null;
            usuario.ResetTokenExpira = null;
            await _context.SaveChangesAsync();

            return Ok("Contraseña actualizada exitosamente.");
        }

        [HttpGet("actual")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) return NotFound();
            return Ok(new
            {
                user.UsuarioId,
                user.Nombre_Completo,
                user.Email,
                user.Rol
            });
        }

    }
}
