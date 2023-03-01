namespace CManager.Infrastructure.Repositories
{
    public class CaracteristicaRepository : ICaracteristicaRepository
    {
        protected DbSet<Caracteristica> _dbSet;

        public CaracteristicaRepository(CManagerDBContext dbContext) => _dbSet = dbContext.Set<Caracteristica>();
        public async Task AddAsync(Caracteristica caracteristica) => await _dbSet.AddAsync(caracteristica);        
        public async Task<List<Caracteristica>> GetListByIdAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Caracteristica> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    }
}