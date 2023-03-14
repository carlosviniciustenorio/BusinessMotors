using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CManager.Application.Services
{
    public interface IVersaoRepository
    {
        Task AddAsync(Versao caracteristica);
        Task<List<Versao>> GetListByIdsAsync(List<int> ids);
        Task<Versao> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}