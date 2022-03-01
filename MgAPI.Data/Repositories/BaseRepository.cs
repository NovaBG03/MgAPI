using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MgAPI.Data.Entities;

namespace MgAPI.Data.Repositories
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }
        public void Create(T item)
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
        }
        public T Read(string id)
        {
            return _context.Set<T>().Find(id);

        }

        public T Read(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }
        public ICollection<T> ReadAll()
        {
            return _context.Set<T>().ToList();
        }

        public ICollection<T> ReadAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        public void Update(T item)
        {
            T itemToUpdate = _context.Set<T>().Find(item.Id);
            _context.Entry(itemToUpdate).CurrentValues.SetValues(item);

            _context.SaveChanges();
        }

        public void Delete(string key)
        {
            T itemToRemove = _context.Set<T>().FirstOrDefault(x => x.Id == key);
            _context.Set<T>().Remove(itemToRemove);

            _context.SaveChanges();
        }
    }
}
