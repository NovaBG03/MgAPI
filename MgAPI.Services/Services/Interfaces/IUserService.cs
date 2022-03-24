using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        Task<User> GetById(string id);
        Task<User> Create(CreateUserRequest model);
        Task<User> Edit(EditUserRequest model);
        Task ChangePassword(string id, ChangePasswordRequest model);
        Task Delete(string id);
    }
}
