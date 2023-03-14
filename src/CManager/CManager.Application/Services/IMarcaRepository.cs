using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Services
{
    public interface IMarcaRepository
    {
        Task AddAsync(Marca caracteristica);
        Task<List<Marca>> GetListByIdAsync(List<int> ids);
        Task<Marca> GetByIdAsync(int id);  
        Task SaveChangesAsync();
    }
}