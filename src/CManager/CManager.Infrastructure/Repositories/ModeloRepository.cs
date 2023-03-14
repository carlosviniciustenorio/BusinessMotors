namespace CManager.Infrastructure.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
         protected DbSet<Modelo> _dbSet;
        protected CManagerDBContext _dbContext;

        public ModeloRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Modelo>(), dbContext);
        public async Task AddAsync(Modelo caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Modelo>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Modelo> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}