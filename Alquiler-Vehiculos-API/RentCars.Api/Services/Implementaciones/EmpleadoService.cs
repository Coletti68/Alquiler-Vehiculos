using Microsoft.EntityFrameworkCore;
using RentCars.Api.Data;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Services.Implementaciones
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Empleado?> GetByIdAsync(int id)
        {
            return await _context.Empleados.FindAsync(id);
        }
        public async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            return await _context.Empleados.ToListAsync();
        }

        public async Task<Empleado> CreateAsync(Empleado empleado)
        {
            bool dniRepetido = await _context.Empleados.AnyAsync(e => e.DNI == empleado.DNI);
            if (dniRepetido)
                throw new InvalidOperationException("Ya existe un empleado con ese DNI.");

            bool emailRepetido = await _context.Empleados.AnyAsync(e => e.Email.ToLower() == empleado.Email.ToLower());
            if (emailRepetido)
                throw new InvalidOperationException("Ya existe un empleado con ese Email.");

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<Empleado?> UpdateAsync(int id, Empleado empleado)
        {
            var existente = await _context.Empleados.FindAsync(id);
            if (existente == null) return null;

            existente.Nombre_Completo = empleado.Nombre_Completo;
            existente.Cargo = empleado.Cargo;
            existente.DNI = empleado.DNI;
            existente.Telefono = empleado.Telefono;
            existente.Email = empleado.Email;
            existente.Sucursal = empleado.Sucursal;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return false;

            empleado.Activo = false; 
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
