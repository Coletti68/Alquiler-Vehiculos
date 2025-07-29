using RentCars.Api.DTOs.Vehiculo;
using RentCars.Api.Models;

namespace RentCars.Api.Services.Interfaces
{
    public interface IVehiculoService
    {
        Task<IEnumerable<VehiculoListadoDTO>> GetListadoDetalladoAsync();
        Task<IEnumerable<Vehiculo>> GetAllAsync();
        Task<Vehiculo?> GetByIdAsync(int id);
        Task<Vehiculo> CreateAsync(Vehiculo vehiculo);
        Task<Vehiculo?> UpdateAsync(int id, Vehiculo vehiculo);
        Task<bool> DeleteAsync(int id);
        Task<bool> CambiarEstadoAsync(int id, string nuevoEstado);
    }
}