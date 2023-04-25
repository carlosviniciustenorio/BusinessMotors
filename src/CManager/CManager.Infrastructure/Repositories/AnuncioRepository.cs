using CManager.Application.Queries;

namespace CManager.Infrastructure.Repositories
{
    public class AnuncioRepository : Repository<Anuncio>, IAnuncioRepository
    {
        protected DbSet<Anuncio> _dbSet;
        protected CManagerDBContext _dbContext;

        public AnuncioRepository(CManagerDBContext dbContext) : base(dbContext) => (_dbSet, _dbContext) = (dbContext.Set<Anuncio>(), dbContext);
        public async Task<List<Anuncio>> GetListByIdAsync(List<Guid> ids) => await _dbSet.Where(d => ids.Contains(d.Id)).ToListAsync();
        public async Task<Anuncio> GetByIdAsync(Guid id) => await _dbSet.IgnoreAutoIncludes()
                                                                        .Include(d => d.Caracteristicas)
                                                                        .Include(d => d.Opcionais)
                                                                        .Include(d => d.TiposCombustiveis)
                                                                        .Include(d => d.Modelo)
                                                                            .ThenInclude(d => d.Marca)
                                                                        .Include(d => d.Versao)
                                                                        .FirstOrDefaultAsync(d => d.Id == id);
        public async Task<List<Anuncio>> GetAllAsync(GetAnunciosQuery.Anuncios querie) => 
                                                    await _dbSet.IgnoreAutoIncludes()
                                                                .Include(d => d.Modelo)
                                                                .ThenInclude(d => d.Marca)
                                                                .Include(d => d.Versao)
                                                                .Where(d => 
                                                                        (string.IsNullOrEmpty(querie.estado) || d.Estado.Equals(querie.estado)) && 
                                                                        (!querie.precoInicio.HasValue || d.Preco >= querie.precoInicio) &&
                                                                        (!querie.precoFim.HasValue || d.Preco <= querie.precoFim) &&
                                                                        (!querie.kmInicio.HasValue || Convert.ToInt32(d.Km) >= querie.kmInicio) &&
                                                                        (!querie.kmFim.HasValue || Convert.ToInt32(d.Km) <= querie.kmFim) &&
                                                                        (!querie.anoModeloInicio.HasValue || d.Modelo.AnoModelo >= querie.anoModeloInicio) &&
                                                                        (!querie.anoModeloFim.HasValue || d.Modelo.AnoModelo <= querie.anoModeloFim) &&
                                                                        (!querie.idModelo.HasValue || d.Modelo.Id >= querie.idModelo) &&
                                                                        (!querie.idMarca.HasValue || d.Modelo.Marca.Id == querie.idMarca) &&
                                                                        (!querie.idVersao.HasValue || d.Modelo.Versoes.Any(d => d.Id == querie.idVersao))
                                                                      )
                                                                .ToListAsync();
    }
}