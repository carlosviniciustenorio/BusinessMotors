namespace BusinessMotors.Application.Interfaces
{
    public interface IAnuncioRepository
    {
        Task AddAsync(Anuncio caracteristica);
        Task<List<Anuncio>> GetListByIdAsync(List<Guid> ids);
        Task<Anuncio> GetByIdAsync(Guid id);
        Task<List<Anuncio>> GetAllAsync(GetAnunciosQuery.Anuncios querie);
        Task SaveChangesAsync();
    }
}