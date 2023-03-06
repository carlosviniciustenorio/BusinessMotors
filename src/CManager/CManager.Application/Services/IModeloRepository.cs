using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Services
{
    public interface IModeloRepository
    {
        Task AddAsync(Modelo caracteristica);
        Task<List<Modelo>> GetListByIdsAsync(List<int> ids);
        Task<Modelo> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}