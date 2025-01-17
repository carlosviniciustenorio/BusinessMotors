namespace BusinessMotors.Application.Interfaces
{
    public interface ITipoCombustivelRepository
    {
        Task AddAsync(TipoCombustivel caracteristica);
        Task<List<TipoCombustivel>> GetListByIdsAsync(List<int> ids);
        Task<TipoCombustivel> GetByIdAsync(int id);
        Task<List<TipoCombustivel>> GetListByQueryAsync(GetTiposCombustiveisQuery.TiposCombustiveis query);
        Task<TipoCombustivel> GetByQueryAsync(GetTipoCombustivelQuery.TipoCombustivel query);
        Task SaveChangesAsync();
    }
}