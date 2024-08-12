namespace BusinessMotors.Infrastructure.Repositories
{
    public class TipoCombustivelRepository : ITipoCombustivelRepository
    {
        protected DbSet<TipoCombustivel> _dbSet;
        protected BusinessMotorsDBContext _dbContext;

        public TipoCombustivelRepository(BusinessMotorsDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<TipoCombustivel>(), dbContext);
        public async Task AddAsync(TipoCombustivel caracteristica) => await _dbSet.AddAsync(caracteristica);
        public async Task<List<TipoCombustivel>> GetListByIdsAsync(List<int> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<TipoCombustivel> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<List<TipoCombustivel>> GetListByQueryAsync(GetTiposCombustiveisQuery.TiposCombustiveis query) => 
            await _dbSet.Where(d => string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                        .Skip(query.skip)
                        .Take(query.take)
                        .ToListAsync();

        public async Task<TipoCombustivel> GetByQueryAsync(GetTipoCombustivelQuery.TipoCombustivel query) => 
            await _dbSet.FirstOrDefaultAsync(d => d.Id == query.id &&
                                                (string.IsNullOrEmpty(query.nome) || d.Descricao.Contains(query.nome))
                                            );
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}