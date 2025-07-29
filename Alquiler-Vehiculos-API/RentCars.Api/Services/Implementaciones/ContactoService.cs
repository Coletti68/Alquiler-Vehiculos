using Microsoft.EntityFrameworkCore;
using RentCars.Api.Data;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Services.Implementaciones
{
    public class ContactoService : IContactoService
    {
        private readonly ApplicationDbContext _context;

        public ContactoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contacto>> GetAllAsync()
            => await _context.Contactos.ToListAsync();

        public async Task<Contacto?> GetByIdAsync(int id)
            => await _context.Contactos.FindAsync(id);

        public async Task<Contacto> CreateAsync(Contacto contacto)
        {
            contacto.Fecha = DateTime.Now;
            _context.Contactos.Add(contacto);
            await _context.SaveChangesAsync();
            return contacto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null) return false;

            _context.Contactos.Remove(contacto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Contacto?> MarcarComoRespondidoAsync(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto == null) return null;

            contacto.Respondido = true;
            await _context.SaveChangesAsync();
            return contacto;
        }
    }
}
