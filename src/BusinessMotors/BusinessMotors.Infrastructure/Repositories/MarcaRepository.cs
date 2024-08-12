namespace BusinessMotors.Infrastructure.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        protected DbSet<Marca> _dbSet;
        protected BusinessMotorsDBContext _dbContext;

        public MarcaRepository(BusinessMotorsDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Marca>(), dbContext);
        
        public async Task AddAsync(Marca caracteristica) => await _dbSet.AddAsync(caracteristica);
        
        public async Task<List<Marca>> GetListByQueryAsync(GetMarcasQuery.Marcas query) => 
            await _dbSet.Where(d => string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                        .Skip(query.skip)
                        .Take(query.take)
                        .ToListAsync();

        public async Task<Marca> GetByQueryAsync(GetMarcaQuery.Marca query) => 
            await _dbSet.FirstOrDefaultAsync(d => d.Id == query.id &&
                                                (string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                                            );
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}