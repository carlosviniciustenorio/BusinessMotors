namespace CManager.Application.Services
{
    public interface ITipoCombustivelRepository
    {
        Task AddAsync(TipoCombustivel caracteristica);
        Task<List<TipoCombustivel>> GetListByIdsAsync(List<int> ids);
        Task<TipoCombustivel> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}