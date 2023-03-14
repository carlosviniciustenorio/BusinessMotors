namespace CManager.Infrastructure.Repositories
{
    public class OpcionalRepository : IOpcionalRepository
    {
        protected DbSet<Opcional> _dbSet;
        protected CManagerDBContext _dbContext;

        public OpcionalRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Opcional>(), dbContext);
        public async Task AddAsync(Opcional caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Opcional>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Opcional> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}