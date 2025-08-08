using Microsoft.AspNetCore.Mvc;
using MVCAlquilerAutos.Models;
using MVCAlquilerAutos.Services;

namespace MVCAlquilerAutos.Controllers
{
    public class MultaController : Controller
    {
        private readonly MultaService _multaService;

        public MultaController(MultaService multaService)
        {
            _multaService = multaService;
        }

        [HttpGet]
        public IActionResult Asignar(int alquilerId)
        {
            var multa = new Multa { AlquilerId = alquilerId };
            return View(multa);
        }

        [HttpPost]
        public async Task<IActionResult> Asignar(Multa multa)
        {
            if (!ModelState.IsValid)
                return View(multa);

            var success = await _multaService.AsignarMultaAsync(multa);
            if (success)
                TempData["Mensaje"] = "Multa asignada correctamente.";
            else
                TempData["Error"] = "Error al asignar la multa.";

            return RedirectToAction("Index", "Alquiler");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var multas = await _multaService.GetAllAsync();
            return View(multas);
        }
    }
}

