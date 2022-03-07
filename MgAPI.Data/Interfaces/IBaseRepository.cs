using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MgAPI.Data.Entities;
using System.Linq.Expressions;

namespace MgAPI.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T item);
        public T Read(string id);
        public T Read(Expression<Func<T, bool>> predicate);
        public ICollection<T> ReadAll();
        public ICollection<T> ReadAll(Expression<Func<T, bool>> predicate);
        public void Update(T item);
        public void Delete(string key);
    }
}
