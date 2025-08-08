using Microsoft.EntityFrameworkCore;
using RentCars.Api.Data;
using RentCars.Api.DTOs.Multa;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;

namespace RentCars.Api.Services.Implementaciones
{
    public class MultaService : IMultaService
    {
        private readonly ApplicationDbContext _context;

        public MultaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MultaResponse>> GetAllAsync()
        {
            return await _context.Multas
                .Select(m => new MultaResponse
                {
                    MultaId = m.MultaId,
                    AlquilerId = m.AlquilerId,
                    Descripcion = m.Descripcion,
                    Monto = m.Monto,
                    FechaMulta = m.FechaMulta,
                    Estado = m.Estado,
                    Tipo = m.Tipo
                }).ToListAsync();
        }

        public async Task<MultaResponse?> GetByIdAsync(int id)
        {
            var multa = await _context.Multas.FindAsync(id);
            if (multa == null) return null;

            return new MultaResponse
            {
                MultaId = multa.MultaId,
                AlquilerId = multa.AlquilerId,
                Descripcion = multa.Descripcion,
                Monto = multa.Monto,
                FechaMulta = multa.FechaMulta,
                Estado = multa.Estado,
                Tipo = multa.Tipo
            };
        }

        public async Task<MultaResponse> CreateAsync(MultaCreateRequest request)
        {
            var multa = new Multa
            {
                AlquilerId = request.AlquilerId,
                Descripcion = request.Descripcion,
                Monto = request.Monto,
                FechaMulta = request.FechaMulta,
                Estado = request.Estado,
                Tipo = request.Tipo
            };

            _context.Multas.Add(multa);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(multa.MultaId) ?? throw new Exception("No se pudo crear la multa");
        }

        public async Task<bool> CambiarEstadoAsync(int multaId, string nuevoEstado)
        {
            var estadosValidos = new[] { "Pendiente", "Pago", "Atrasado" };
            if (!estadosValidos.Contains(nuevoEstado))
                return false;

            var multa = await _context.Multas.FindAsync(multaId);
            if (multa == null)
                return false;

            multa.Estado = nuevoEstado;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var multa = await _context.Multas.FindAsync(id);
            if (multa == null) return false;

            _context.Multas.Remove(multa);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<MultaResumenResponse>> ObtenerMultasImpagasAsync()
        {
            return await _context.Multas
                .Where(m => m.Estado == "Pendiente" || m.Estado == "Atrasado")
                .Include(m => m.Alquiler).ThenInclude(a => a.Usuario)
                .Select(m => new MultaResumenResponse
                {
                    MultaId = m.MultaId,
                    Descripcion = m.Descripcion,
                    Monto = m.Monto,
                    Estado = m.Estado,
                    Fecha = m.FechaMulta,
                    UsuarioNombre = m.Alquiler.Usuario.Nombre_Completo + " "
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<MultaResponse>> ObtenerMultasPorUsuarioAsync(int usuarioId)
        {
            return await _context.Multas
                .Include(m => m.Alquiler)
                .Where(m => m.Alquiler.UsuarioId == usuarioId)
                .Select(m => new MultaResponse
                {
                    MultaId = m.MultaId,
                    AlquilerId = m.AlquilerId,
                    Descripcion = m.Descripcion,
                    Monto = m.Monto,
                    FechaMulta = m.FechaMulta,
                    Estado = m.Estado,
                    Tipo = m.Tipo
                })
                .ToListAsync();
        }

    }
}
