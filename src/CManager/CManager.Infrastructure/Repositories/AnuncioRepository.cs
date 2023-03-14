namespace CManager.Infrastructure.Repositories
{
    public class AnuncioRepository: IAnuncioRepository
    {
        protected DbSet<Anuncio> _dbSet;
        protected CManagerDBContext _dbContext;

        public AnuncioRepository(CManagerDBContext dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Anuncio>(), dbContext);
        public async Task AddAsync(Anuncio caracteristica) => await _dbSet.AddAsync(caracteristica);        
        public async Task<List<Anuncio>> GetListByIdAsync(List<Guid> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Anuncio> GetByIdAsync(Guid id) => await _dbSet.IgnoreAutoIncludes()
                                                                        .Include(d => d.Caracteristicas)
                                                                        .Include(d => d.Opcionais)
                                                                        .Include(d => d.TiposCombustiveis)
                                                                        .Include(d => d.Modelo)
                                                                            .ThenInclude(d => d.Marca)
                                                                        .Include(d => d.Versao)
                                                                        .FirstOrDefaultAsync(d => d.Id == id);
        public async Task<List<Anuncio>> GetAllAsync() => await _dbSet.IgnoreAutoIncludes()
                                                                      .Include(d => d.Modelo)
                                                                        .ThenInclude(d => d.Marca)
                                                                      .Include(d => d.Versao)
                                                                      .ToListAsync();
        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}