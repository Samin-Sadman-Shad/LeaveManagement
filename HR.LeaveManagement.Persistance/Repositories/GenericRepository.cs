using HR.LeaveManagement.Application.Persistance.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal readonly LeaveManagementDbContext _dbContext;
        private DbContextOptions options;

        public GenericRepository(LeaveManagementDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public GenericRepository(DbContextOptions options)
        {
            this.options = options;
        }

        public async Task<T> AddAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var item = await _dbContext.FindAsync<T>(id);
            if (item != null)
            {
                _dbContext.Set<T>().Remove(item);
            }
            await _dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Exists(int id)
        {
            var item = await GetAsync(id);
            return item != null;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public async Task<T> UpdateAsync(T item)
        {
            var itemTracked = _dbContext.Entry(item);
            itemTracked.State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
