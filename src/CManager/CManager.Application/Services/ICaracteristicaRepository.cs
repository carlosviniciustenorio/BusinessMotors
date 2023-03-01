namespace CManager.Application.Services
{
    public interface ICaracteristicaRepository
    {
        Task AddAsync(Caracteristica caracteristica);
        Task<List<Caracteristica>> GetListByIdAsync(List<int> ids);
        Task<Caracteristica> GetByIdAsync(int id);  
    }
}