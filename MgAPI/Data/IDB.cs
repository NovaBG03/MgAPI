using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Data
{
    public interface IDB<T, K> where K : IConvertible
    {
        void Create(T item);
        T Read(K key);
        ICollection<T> ReadAll();
        void Update(T item);
        void Delete(K key);
        ICollection<T> Find(K index);
    }
}
