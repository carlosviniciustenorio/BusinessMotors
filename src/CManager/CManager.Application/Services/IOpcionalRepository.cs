namespace CManager.Application.Services
{
    public interface IOpcionalRepository
    {
        Task AddAsync(Opcional caracteristica);
        Task<List<Opcional>> GetListByIdsAsync(List<int> ids);
        Task<Opcional> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}