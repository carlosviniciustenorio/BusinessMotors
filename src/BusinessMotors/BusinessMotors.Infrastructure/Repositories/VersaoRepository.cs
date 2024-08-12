namespace BusinessMotors.Infrastructure.Repositories
{
    public class VersaoRepository : IVersaoRepository
    {
        protected DbSet<Versao> _dbSet;
        protected BusinessMotorsDBContext _dbContext;

        public VersaoRepository(BusinessMotorsDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Versao>(), dbContext);
        public async Task AddAsync(Versao caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Versao>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Versao> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}