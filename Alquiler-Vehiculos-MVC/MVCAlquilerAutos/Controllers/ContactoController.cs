using Microsoft.AspNetCore.Mvc;
using MVCAlquilerAutos.Models;
using MVCAlquilerAutos.Services;
using Newtonsoft.Json;
using System.Reflection;

public class ContactoController : Controller
{
    private readonly ContactoService _contactoService;

    public ContactoController(ContactoService contactoService)
    {
        _contactoService = contactoService;
    }

    public async Task<IActionResult> Index()
    {
        var contactos = await _contactoService.GetAllAsync();
        var pendientes = contactos.Where(c => !c.Respondido).ToList();
        return View(pendientes);
    }

    [HttpPost]
    public async Task<IActionResult> Responder(int id)
    {
        var actualizado = await _contactoService.MarcarComoRespondidoAsync(id);
        if (actualizado == null)
            return NotFound();

        return RedirectToAction("Index");
    }
}

