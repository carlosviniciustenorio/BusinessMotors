using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CManager.Integration.Cache
{
    public interface ICacheService
    {
        Task Add(string key, string value);
        Task<string?> Get(string key);
        Task Remove(string key);
    }
}
