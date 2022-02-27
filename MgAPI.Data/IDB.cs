using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Data
{
    public interface IDB<T>
    {
        void Create(T item);
        T Read(string key);
        T Read(Expression<Func<T, bool>> predicate);
        ICollection<T> ReadAll();
        ICollection<T> ReadAll(Expression<Func<T, bool>> predicate);
        void Update(T item);
        void Delete(string key);
    }
}
