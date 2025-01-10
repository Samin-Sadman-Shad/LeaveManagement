using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistance
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetAsync(int id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T>  AddAsync(T item);
        public Task<T> UpdateAsync(T item);
        public Task<T> DeleteAsync(int id);

        public Task<bool> Exists(int id);
    }
}
