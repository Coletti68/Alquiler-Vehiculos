using RentCars.Api.Data;
using Microsoft.EntityFrameworkCore;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;
using RentCars.Api.DTOs.Alquiler;

namespace RentCars.Api.Services.Implementaciones
{
    public class AlquilerService : IAlquilerService
    {
        private readonly ApplicationDbContext _context;

        public AlquilerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlquilerResponse>> GetAllAsync()
        {
            var alquileres = await _context.Alquileres
                .Include(a => a.Usuario)
                .Include(a => a.Vehiculo)
                .Include(a => a.Empleado)
                .ToListAsync();

            return alquileres.Select(a => new AlquilerResponse
            {
                AlquilerId = a.AlquilerId,
                UsuarioId = a.UsuarioId,
                VehiculoId = a.VehiculoId,
                EmpleadoId = a.EmpleadoId,
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                Total = a.Total,
                Estado = a.Estado,
                AceptoTerminos = a.AceptoTerminos
            }).ToList();
        }

        public async Task<List<Alquiler>> GetAllDetalladoAsync()
        {
            return await _context.Alquileres
                .Include(a => a.Usuario)
                .Include(a => a.Vehiculo)
                .Include(a => a.Empleado)
                .ToListAsync();
        }

        public async Task<Alquiler> CreateAsync(Alquiler alquiler)
        {
           
            if (alquiler.FechaInicio.Date < DateTime.Today)
                throw new Exception("La fecha de inicio no puede ser anterior a hoy.");

            bool tieneAlquilerActivo = await _context.Alquileres
                .AnyAsync(a => a.UsuarioId == alquiler.UsuarioId &&
                              (a.Estado == "Activo" || a.Estado == "Reservado"));

            if (tieneAlquilerActivo)
                throw new Exception("Ya tenés un alquiler en curso o reservado. Cancelalo o esperá a que finalice para crear uno nuevo.");

            var vehiculo = await _context.Vehiculos.FindAsync(alquiler.VehiculoId);
            if (vehiculo == null)
                throw new Exception("El vehículo especificado no existe.");

            var estadosInvalidos = new[] { "Alquilado", "Reservado", "Mantenimiento" };
            if (estadosInvalidos.Contains(vehiculo.Estado))
                throw new Exception($"El vehículo no puede ser alquilado porque se encuentra en estado '{vehiculo.Estado}'.");

            var dias = (alquiler.FechaFin.Date - alquiler.FechaInicio.Date).Days;
            if (dias <= 0)
                throw new Exception("Las fechas seleccionadas no son válidas. Debe haber al menos un día de alquiler.");

            alquiler.Total = dias * vehiculo.PrecioPorDia;

            var hoy = DateTime.Today;
            alquiler.Estado = (alquiler.FechaInicio.Date > hoy) ? "Reservado" : "Activo";
            vehiculo.Estado = (alquiler.Estado == "Reservado") ? "Reservado" : "Alquilado";

            _context.Alquileres.Add(alquiler);
            await _context.SaveChangesAsync();

            return alquiler;
        }

        public async Task<AlquilerResponse?> GetByIdAsync(int id)
        {
            var a = await _context.Alquileres
                .Include(x => x.Usuario)
                .Include(x => x.Vehiculo)
                .Include(x => x.Empleado)
                .FirstOrDefaultAsync(x => x.AlquilerId == id);

            if (a == null) return null;

            return new AlquilerResponse
            {
                AlquilerId = a.AlquilerId,
                UsuarioId = a.UsuarioId,
                VehiculoId = a.VehiculoId,
                EmpleadoId = a.EmpleadoId,
                FechaInicio = a.FechaInicio,
                FechaFin = a.FechaFin,
                Total = a.Total,
                Estado = a.Estado,
                AceptoTerminos = a.AceptoTerminos
            };
        }

        public async Task<bool> UpdateAsync(int id, AlquilerUpdate dto)
        {
            var alquiler = await _context.Alquileres.FindAsync(id);
            if (alquiler == null) return false;

            alquiler.FechaInicio = dto.FechaInicio;
            alquiler.FechaFin = dto.FechaFin;
            alquiler.Total = dto.Total;
            alquiler.Estado = dto.Estado;
            alquiler.AceptoTerminos = dto.AceptoTerminos;
            alquiler.UsuarioId = dto.UsuarioId;
            alquiler.VehiculoId = dto.VehiculoId;
            alquiler.EmpleadoId = dto.EmpleadoId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> FinalizarAlquileresVencidosAsync()
        {
            var hoy = DateTime.Today;

            var vencidos = await _context.Alquileres
                .Where(a => a.Estado == "Activo" && a.FechaFin < hoy)
                .ToListAsync();

            foreach (var alquiler in vencidos)
            {
                alquiler.Estado = "Finalizado";
                alquiler.FechaFin = hoy;

                var vehiculo = await _context.Vehiculos.FindAsync(alquiler.VehiculoId);
                if (vehiculo != null)
                    vehiculo.Estado = "Disponible";
            }

            await _context.SaveChangesAsync();
            return vencidos.Count;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var alquiler = await _context.Alquileres.FindAsync(id);
            if (alquiler == null) return false;

            _context.Alquileres.Remove(alquiler);
            await _context.SaveChangesAsync();
            return true;
        }

        //
        public async Task<IEnumerable<Alquiler>> GetByUsuarioIdAsync(int id)
        {
            return await _context.Alquileres
                .Include(a => a.Vehiculo) 
                .Include(a => a.Usuario)   
                .Where(a => a.UsuarioId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alquiler>> GetByEstadoAsync(string estado)
        {
            return await _context.Alquileres
                .Where(a => a.Estado == estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alquiler>> GetByRangoFechasAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Alquileres
                .Where(a => a.FechaInicio >= desde && a.FechaFin <= hasta)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alquiler>> GetAlquileresActivosAsync(int? id = null)
        {
            var query = _context.Alquileres
                .Where(a => a.Estado == "Activo");

            if (id.HasValue)
                query = query.Where(a => a.UsuarioId == id.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Alquiler>> GetAlquileresVencidosAsync()
        {
            var hoy = DateTime.Today;
            return await _context.Alquileres
                .Where(a => a.Estado == "Activo" && a.FechaFin < hoy)
                .ToListAsync();
        }
   
        public async Task<bool> FinalizarAlquilerAsync(int alquilerId, DateTime FechaFin)
        {
            var alquiler = await _context.Alquileres.FindAsync(alquilerId);
            if (alquiler == null || alquiler.Estado != "Activo")
                return false;

            alquiler.Estado = "Finalizado";
            alquiler.FechaFin = FechaFin;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelarAlquilerAsync(int alquilerId)
        {
            var alquiler = await _context.Alquileres.FindAsync(alquilerId);
            if (alquiler == null || alquiler.Estado != "Activo")
                return false;

            alquiler.Estado = "Cancelado";
            await _context.SaveChangesAsync();
            return true;
        }

        //

        public async Task<bool> VerificarDisponibilidadVehiculoAsync(int vehiculoId, DateTime desde, DateTime hasta)
        {
            return !await _context.Alquileres
                .AnyAsync(a =>
                    a.VehiculoId == vehiculoId &&
                    a.Estado == "Activo" &&
                    (desde <= a.FechaFin && hasta >= a.FechaInicio));
        }

        public async Task<IEnumerable<Alquiler>> GetAlquileresPorVehiculoAsync(int vehiculoId)
        {
            return await _context.Alquileres
                .Where(a => a.VehiculoId == vehiculoId)
                .ToListAsync();
        }

        public async Task<int> ContarAlquileresPorUsuarioAsync(int clienteId)
        {
            return await _context.Alquileres
                .CountAsync(a => a.UsuarioId == clienteId);
        }

        public Task<decimal> CalcularTotalFacturadoAsync(DateTime desde, DateTime hasta)
        {
            throw new NotImplementedException();
        }

        /*public async Task<decimal> CalcularTotalFacturadoAsync(DateTime desde, DateTime hasta)
        {
            return await _context.Alquileres
                .Where(a => a.FechaInicio >= desde && a.FechaFin <= hasta && a.Estado == "Finalizado")
                .SumAsync(a => a.Total);
        */
    }
    }
