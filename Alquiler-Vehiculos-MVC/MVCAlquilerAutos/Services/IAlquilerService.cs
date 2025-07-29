using MVCAlquilerAutos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCAlquilerAutos.Services
{
    public interface IAlquilerService
    {
        Task<IEnumerable<Alquiler>> GetDetalladoAsync();
        Task<IEnumerable<Alquiler>> GetAllAsync();
        Task<Alquiler> GetByIdAsync(int id);
        Task<bool> CreateAsync(Alquiler alquiler);
        Task<bool> UpdateAsync(int id, Alquiler alquiler);
        Task<bool> DeleteAsync(int id);

        Task<bool> FinalizarVencidosAsync();
        Task<bool> FinalizarAsync(int id);
        Task<bool> CancelarAsync(int id);
        Task<IEnumerable<Alquiler>> GetByEstadoAsync(string estado);

        Task<IEnumerable<Alquiler>> GetHabilitadosAsync();
        Task<IEnumerable<Alquiler>> GetResumenPorUsuarioAsync(int usuarioId);
        Task<string> GetNumeradorPorIdAsync(int id);
        Task<IEnumerable<Alquiler>> GetByRangoFechasAsync(DateTime desde, DateTime hasta);
    }
}

