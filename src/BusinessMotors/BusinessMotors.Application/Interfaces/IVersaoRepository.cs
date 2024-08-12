namespace BusinessMotors.Application.Interfaces
{
    public interface IVersaoRepository
    {
        Task AddAsync(Versao caracteristica);
        Task<List<Versao>> GetListByIdsAsync(List<int> ids);
        Task<Versao> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}