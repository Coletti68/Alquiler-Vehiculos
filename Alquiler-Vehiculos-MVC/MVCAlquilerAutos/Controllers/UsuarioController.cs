using Microsoft.AspNetCore.Mvc;
using MVCAlquilerAutos.Models;
using MVCAlquilerAutos.Services;

namespace MVCAlquilerAutos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuarioService.GetUsuarioConAlquileresYMultasAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(usuario);

            var resultado = await _usuarioService.UpdateUsuarioAsync(usuario);

            if (!resultado)
                return StatusCode(500, "Error al actualizar el usuario.");

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MarcarMultaPagada(int multaId)
        {
            var resultado = await _usuarioService.MarcarMultaComoPagadaAsync(multaId);

            if (!resultado)
                return BadRequest("No se pudo marcar la multa como pagada.");

            return Ok();
        }
    }
}
