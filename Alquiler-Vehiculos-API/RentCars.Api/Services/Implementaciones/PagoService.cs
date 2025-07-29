using RentCars.Api.DTOs.Pago;
using RentCars.Api.Models;
using RentCars.Api.Services.Interfaces;
using RentCars.Api.Data;
using Microsoft.EntityFrameworkCore;

public class PagoService : IPagoService
{
    private readonly ApplicationDbContext _context;

    public PagoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PagoResponse>> GetAllAsync()
    {
        return await _context.Pagos
            .Include(p => p.Alquiler)
            .Select(p => new PagoResponse
            {
                PagoId = p.PagoId,
                AlquilerId = p.AlquilerId,
                Monto = p.Monto,
                MetodoPago = p.MetodoPago,
                FechaPago = p.FechaPago
            })
            .ToListAsync();
    }

    public async Task<PagoResponse> GetByIdAsync(int id)
    {
      var pago = await _context.Pagos.FindAsync(id);
      if (pago == null) return null;

      return new PagoResponse
    {
        PagoId = pago.PagoId,
        AlquilerId = pago.AlquilerId,
        Monto = pago.Monto,
        MetodoPago = pago.MetodoPago,
        FechaPago = pago.FechaPago,
        CodigoReferencia = pago.CodigoReferencia,
        Observaciones = pago.Observaciones
    };
}

    public async Task<PagoResponse> CreateAsync(PagoCreateRequest request)
    {
        var alquiler = await _context.Alquileres.FindAsync(request.AlquilerId);
        if (alquiler == null) throw new InvalidOperationException("Alquiler no encontrado.");

        var pago = new Pago
        {
            AlquilerId = request.AlquilerId,
            Monto = request.Monto,
            MetodoPago = request.MetodoPago,
            FechaPago = request.FechaPago
        };

        _context.Pagos.Add(pago);
        await _context.SaveChangesAsync();

        return new PagoResponse
        {
            PagoId = pago.PagoId,
            AlquilerId = pago.AlquilerId,
            Monto = pago.Monto,
            MetodoPago = pago.MetodoPago,
            FechaPago = pago.FechaPago
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pago = await _context.Pagos.FindAsync(id);
        if (pago == null) return false;

        _context.Pagos.Remove(pago);
        await _context.SaveChangesAsync();
        return true;
    }
}