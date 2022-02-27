using MgAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Data
{
    public class UsersContext : IDB<User>
    {
        private Context _context;

        public UsersContext(Context context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item);

            _context.SaveChanges();
        }

        public User Read(string key)
        {
            return _context.Users.Find(key);
        }

        public User Read(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.SingleOrDefault(predicate);
        }

        public ICollection<User> ReadAll()
        {
            return _context.Users.ToList();
        }

        public ICollection<User> ReadAll(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate).ToList();
        }

        public void Update(User item)
        {
            User userToUpdate = _context.Users.Find(item.ID);
            _context.Entry(userToUpdate).CurrentValues.SetValues(item);

            _context.SaveChanges();
        }

        public void Delete(string key)
        {
            User userToRemove = _context.Users.FirstOrDefault(x => x.ID == key);
            _context.Users.Remove(userToRemove);

            _context.SaveChanges();
        }
    }
}
