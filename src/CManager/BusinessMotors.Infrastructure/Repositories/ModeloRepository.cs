namespace BusinessMotors.Infrastructure.Repositories
{
    public class ModeloRepository : IModeloRepository
    {
         protected DbSet<Modelo> _dbSet;
        protected CManagerDBContext _dbContext;

        public ModeloRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Modelo>(), dbContext);
        public async Task AddAsync(Modelo caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<Modelo>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Modelo> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<List<Modelo>> GetListByQueryAsync(GetModelosQuery.Modelos query) => 
            await _dbSet.IgnoreAutoIncludes()
                        .Include(d => d.Versoes)
                        .Include(d => d.Marca)
                        .Where(d => 
                                (string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome)) &&
                                (query.idMarca <= 0 || d.Marca.Id == query.idMarca)
                            )
                        .Skip(query.skip)
                        .Take(query.take)
                        .ToListAsync();

        public async Task<Modelo> GetByQueryAsync(GetModeloQuery.Modelo query) => 
            await _dbSet.IgnoreAutoIncludes()
                        .Include(d => d.Versoes)
                        .Include(d => d.Marca)
                        .FirstOrDefaultAsync(d => d.Id == query.id &&
                                                (string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                                            );
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}