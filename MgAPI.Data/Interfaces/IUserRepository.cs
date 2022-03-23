using MgAPI.Data.Entities;

namespace MgAPI.Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User ReadByUsername(string userName);
        User ReadByEmail(string email);
    }
}
