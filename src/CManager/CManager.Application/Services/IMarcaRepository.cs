using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Services
{
    public interface IMarcaRepository
    {
        Task AddAsync(Marca caracteristica);
        Task<List<Marca>> GetListByQueryAsync(GetMarcasQuery.Marcas query);
        Task<Marca> GetByQueryAsync(GetMarcaQuery.Marca query);  
        Task SaveChangesAsync();
    }
}