using RentCars.Api.DTOs.Multa;

namespace RentCars.Api.Services.Interfaces
{
    public interface IMultaService
    {
        Task<IEnumerable<MultaResponse>> GetAllAsync();
        Task<MultaResponse?> GetByIdAsync(int id);
        Task<MultaResponse> CreateAsync(MultaCreateRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MultaResumenResponse>> ObtenerMultasImpagasAsync();
        Task<bool> CambiarEstadoAsync(int id, string nuevoEstado);
    }
}
