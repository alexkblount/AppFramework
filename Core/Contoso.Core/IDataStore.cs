using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Core
{
    public interface IDataStore<T>
    {
        Task<bool> AddAsync(T model);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAsync(string id);
        Task<T> GetAsync(string id);
        Task<IList<T>> GetAsync();
    }
}