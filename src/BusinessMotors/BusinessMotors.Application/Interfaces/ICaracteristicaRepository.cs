namespace BusinessMotors.Application.Interfaces
{
    public interface ICaracteristicaRepository
    {
        Task AddAsync(Caracteristica caracteristica);
        Task<List<Caracteristica>> GetListByIdAsync(List<int> ids);
        Task<Caracteristica> GetByIdAsync(int id);  
        Task<List<Caracteristica>> GetListByQueryAsync(GetCaracteristicasQuery.Caracteristicas query);
        Task<Caracteristica> GetByQueryAsync(GetCaracteristicaQuery.Caracteristica query);
        Task SaveChangesAsync();
    }
}