using MgAPI.Data.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MgAPI.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Create(T item);
        public Task<T> Read(string id);
        public T Read(Expression<Func<T, bool>> predicate);
        public Task<bool> Exists(string id);
        public bool Exists(Expression<Func<T, bool>> predicate);
        public IQueryable<T> ReadAll();
        public IQueryable<T> ReadAll(Expression<Func<T, bool>> predicate);
        public Task Update(T item);
        public Task Delete(string key);
    }
}
