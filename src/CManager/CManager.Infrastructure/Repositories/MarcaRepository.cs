namespace CManager.Infrastructure.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        protected DbSet<Marca> _dbSet;
        protected CManagerDBContext _dbContext;

        public MarcaRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Marca>(), dbContext);
        public async Task AddAsync(Marca caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Marca>> GetListByIdAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Marca> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}