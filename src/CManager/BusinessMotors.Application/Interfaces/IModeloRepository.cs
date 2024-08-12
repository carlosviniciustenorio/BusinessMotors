namespace BusinessMotors.Application.Interfaces
{
    public interface IModeloRepository
    {
        Task AddAsync(Modelo caracteristica);
        Task<List<Modelo>> GetListByIdsAsync(List<int> ids);
        Task<Modelo> GetByIdAsync(int id);
        Task<List<Modelo>> GetListByQueryAsync(GetModelosQuery.Modelos query);
        Task<Modelo> GetByQueryAsync(GetModeloQuery.Modelo query);
        Task SaveChangesAsync();
    }
}