namespace CManager.Infrastructure.Repositories
{
    public class OpcionalRepository : IOpcionalRepository
    {
        protected DbSet<Opcional> _dbSet;

        public OpcionalRepository(CManagerDBContext dbContext) => _dbSet = dbContext.Set<Opcional>();
        public async Task AddAsync(Opcional caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Opcional>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Opcional> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    }
}