namespace CManager.Application.Interfaces
{
    public interface IOpcionalRepository
    {
        Task AddAsync(Opcional caracteristica);
        Task<List<Opcional>> GetListByIdsAsync(List<int> ids);
        Task<Opcional> GetByIdAsync(int id);
        Task<List<Opcional>> GetListByQueryAsync(GetOpcionaisQuery.Opcionais query);
        Task<Opcional> GetByQueryAsync(GetOpcionalQuery.Opcional query);
        Task SaveChangesAsync();
    }
}