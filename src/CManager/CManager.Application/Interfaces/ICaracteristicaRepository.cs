namespace CManager.Application.Interfaces
{
    public interface ICaracteristicaRepository
    {
        Task AddAsync(Caracteristica caracteristica);
        Task<List<Caracteristica>> GetListByIdAsync(List<int> ids);
        Task<Caracteristica> GetByIdAsync(int id);  
        Task SaveChangesAsync();
    }
}