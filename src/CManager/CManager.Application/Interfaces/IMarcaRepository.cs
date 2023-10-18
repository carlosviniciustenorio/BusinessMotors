namespace CManager.Application.Interfaces
{
    public interface IMarcaRepository
    {
        Task AddAsync(Marca caracteristica);
        Task<List<Marca>> GetListByQueryAsync(GetMarcasQuery.Marcas query);
        Task<Marca> GetByQueryAsync(GetMarcaQuery.Marca query);  
        Task SaveChangesAsync();
    }
}