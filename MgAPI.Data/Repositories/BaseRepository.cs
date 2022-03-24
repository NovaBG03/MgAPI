using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MgAPI.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }
        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<T> Read(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Read(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public async Task<bool> Exists(string id)
        {
            return await Read(id) != null;
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Read(predicate) != null;
        }

        public IQueryable<T> ReadAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public IQueryable<T> ReadAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public async Task Update(T item)
        {
            T itemToUpdate = await Read(item.ID);
            _context.Entry(itemToUpdate).CurrentValues.SetValues(item);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string key)
        {
            T itemToRemove = await Read(key);
            _context.Set<T>().Remove(itemToRemove);

            await _context.SaveChangesAsync();
        }
    }
}
