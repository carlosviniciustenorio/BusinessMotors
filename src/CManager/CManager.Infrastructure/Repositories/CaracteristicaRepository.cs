namespace CManager.Infrastructure.Repositories
{
    public class CaracteristicaRepository : ICaracteristicaRepository
    {
        protected DbSet<Caracteristica> _dbSet;
        protected CManagerDBContext _dbContext;

        public CaracteristicaRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Caracteristica>(), dbContext);
        public async Task AddAsync(Caracteristica caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Caracteristica>> GetListByIdAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Caracteristica> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<List<Caracteristica>> GetListByQueryAsync(GetCaracteristicasQuery.Caracteristicas query) => 
            await _dbSet.Where(d => string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                        .Skip(query.skip)
                        .Take(query.take)
                        .ToListAsync();

        public async Task<Caracteristica> GetByQueryAsync(GetCaracteristicaQuery.Caracteristica query) => 
            await _dbSet.FirstOrDefaultAsync(d => d.Id == query.id &&
                                                (string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                                            );

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}