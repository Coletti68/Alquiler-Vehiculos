using RentCars.Api.DTOs.Alquiler;
using RentCars.Api.Models;

namespace RentCars.Api.Services.Interfaces
{
    public interface IAlquilerService
    {
        Task<List<Alquiler>> GetAllDetalladoAsync();
        Task<IEnumerable<AlquilerResponse>> GetAllAsync();
        Task<AlquilerResponse?> GetByIdAsync(int id);
        Task<Alquiler> CreateAsync(Alquiler alquiler);
        Task<bool> UpdateAsync(int id, AlquilerUpdate dto);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Alquiler>> GetByUsuarioIdAsync(int id);
        Task<IEnumerable<Alquiler>> GetByEstadoAsync(string estado);
        Task<IEnumerable<Alquiler>> GetByRangoFechasAsync(DateTime desde, DateTime hasta);
        Task<IEnumerable<Alquiler>> GetAlquileresActivosAsync(int? id);
        Task<IEnumerable<Alquiler>> GetAlquileresVencidosAsync();

        Task<bool> FinalizarAlquilerAsync(int alquilerId, DateTime FechaFin);
        Task<bool> CancelarAlquilerAsync(int alquilerId);

        Task<bool> VerificarDisponibilidadVehiculoAsync(int vehiculoId, DateTime desde, DateTime hasta);
        Task<IEnumerable<Alquiler>> GetAlquileresPorVehiculoAsync(int vehiculoId);
        Task<int> ContarAlquileresPorUsuarioAsync(int id);
        Task<decimal> CalcularTotalFacturadoAsync(DateTime desde, DateTime hasta);
        Task<int> FinalizarAlquileresVencidosAsync();

        //Task<FacturaAlquilerDTO?> GenerarFacturaDTOAsync(int alquilerId);

    }
}