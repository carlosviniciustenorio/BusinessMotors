namespace CManager.Infrastructure.Repositories
{
    public class AnuncioRepository: IAnuncioRepository
    {
        protected DbSet<Anuncio> _dbSet;
        protected CManagerDBContext _dbContext;

        public AnuncioRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Anuncio>(), dbContext);
        public async Task AddAsync(Anuncio caracteristica) => await _dbSet.AddAsync(caracteristica);        
        public async Task<List<Anuncio>> GetListByIdAsync(List<Guid> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Anuncio> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}