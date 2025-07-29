using RentCars.Api.DTOs.Pago;

namespace RentCars.Api.Services.Interfaces
{ 
public interface IPagoService
 {
    Task<IEnumerable<PagoResponse>> GetAllAsync();
    Task<PagoResponse> GetByIdAsync(int id);
    Task<PagoResponse> CreateAsync(PagoCreateRequest request);
    Task<bool> DeleteAsync(int id);
 }
}