using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;

namespace MgAPI.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context)
            : base(context)
        { }

        public User ReadByUsername(string username)
        {
            return Read(x => x.Username == username);
        }
        public User ReadByEmail(string email)
        {
            return Read(x => x.Email == email);
        }
    }
}
