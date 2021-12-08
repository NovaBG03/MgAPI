using MgAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Data
{
    public class UsersContext : IDB<User, string>
    {
        public void Create(User item)
        {
            using (Context dbContext = new Context())
            {
                dbContext.Users.Add(item);
                dbContext.SaveChanges();
            }
        }
        public void Delete(string key)
        {
            using (Context dbContext = new Context())
            {
                User userDB = new User();
                userDB.ID = key;
                dbContext.Entry<User>(userDB).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }
        public ICollection<User> Find(string index)
        {
            ICollection<User> userDB;

            using (Context dbContext = new Context())
            {
                userDB = dbContext.Users.Where(x => x.ID == index).ToList();
            }
            return userDB;
        }
        public User Read(string key)
        {
            User userDB;
            try
            {
                using (Context dbContext = new Context())
                {
                    userDB = dbContext.Users.Find(key);
                }
            }
            catch (InvalidOperationException)
            {
                ArgumentException msg = new ArgumentException();
                msg.Data.Add("Key", "There is no users with that id!");
                throw msg;
            }
            if (userDB == null)
            {
                throw new ArgumentException("There is no users with that id!");
            }
            return userDB;
        }
        public void Update(User item)
        {
            using (Context dbContext = new Context())
            {
                User userDB = dbContext.Users.Find(item.ID);
                userDB.ID = item.ID;
                dbContext.SaveChanges();
            }
        }

        public ICollection<User> ReadAll()
        {
            List<User> users;
            using (Context dbContext = new Context())
            {
                users = dbContext.Users.ToList();
            }
            return users;
        }
    }
}
