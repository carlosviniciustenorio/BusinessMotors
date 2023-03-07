namespace CManager.Infrastructure.Repositories
{
    public class TipoCombustivelRepository : ITipoCombustivelRepository
    {
        protected DbSet<TipoCombustivel> _dbSet;
        protected CManagerDBContext _dbContext;

        public TipoCombustivelRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<TipoCombustivel>(), dbContext);
        public async Task AddAsync(TipoCombustivel caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<TipoCombustivel>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<TipoCombustivel> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}