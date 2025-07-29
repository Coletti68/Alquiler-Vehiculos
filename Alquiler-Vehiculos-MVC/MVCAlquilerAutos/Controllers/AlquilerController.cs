using Microsoft.AspNetCore.Mvc;
using MVCAlquilerAutos.Models;
using MVCAlquilerAutos.Services;

namespace MVCAlquilerAutos.Controllers
{
    public class AlquilerController : Controller
    {
        private readonly IAlquilerService _alquilerService;

        public AlquilerController(IAlquilerService alquilerService)
        {
            _alquilerService = alquilerService;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _alquilerService.GetDetalladoAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            var alquiler = await _alquilerService.GetByIdAsync(id);
            if (alquiler == null) return NotFound();

            return View(alquiler);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alquiler alquiler)
        {
            if (!ModelState.IsValid) return View(alquiler);

            var ok = await _alquilerService.CreateAsync(alquiler);
            if (!ok)
            {
                ModelState.AddModelError("", "Error al crear el alquiler");
                return View(alquiler);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var alquiler = await _alquilerService.GetByIdAsync(id);
            if (alquiler == null) return NotFound();

            return View(alquiler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Alquiler alquiler)
        {
            if (id != alquiler.AlquilerId) return BadRequest();
            if (!ModelState.IsValid) return View(alquiler);

            var ok = await _alquilerService.UpdateAsync(id, alquiler);
            if (!ok)
            {
                ModelState.AddModelError("", "Error al actualizar el alquiler");
                return View(alquiler);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var alquiler = await _alquilerService.GetByIdAsync(id);
            if (alquiler == null) return NotFound();

            return View(alquiler);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ok = await _alquilerService.DeleteAsync(id);
            if (!ok)
                ModelState.AddModelError("", "Error al eliminar el alquiler");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FinalizarVencidos()
        {
            await _alquilerService.FinalizarVencidosAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FiltrarPorEstado(string estado)
        {
            var lista = await _alquilerService.GetDetalladoAsync();
            var filtrado = lista.Where(a => a.Estado.Equals(estado, StringComparison.OrdinalIgnoreCase));
            return View("Index", filtrado);
        }

        public async Task<IActionResult> GetHabilitados()
        {
            var lista = await _alquilerService.GetDetalladoAsync();
            var habilitados = lista.Where(a => a.Estado == "Activo");
            return View("Index", habilitados);
        }

        public async Task<IActionResult> ResumenPorUsuario(int usuarioId)
        {
            var lista = await _alquilerService.GetResumenPorUsuarioAsync(usuarioId);
            return View("Index", lista);
        }

        public async Task<IActionResult> AlquileresPorRango(DateTime desde, DateTime hasta)
        {
            var lista = await _alquilerService.GetDetalladoAsync();
            var enRango = lista.Where(a => a.FechaInicio >= desde && a.FechaFin <= hasta);
            return View("Index", enRango);
        }

        public async Task<IActionResult> NumeradorPorId(int id)
        {
            var numerador = await _alquilerService.GetNumeradorPorIdAsync(id);
            TempData["Mensaje"] = $"Numerador: {numerador}";
            return RedirectToAction(nameof(Index));
        }
    }
}
