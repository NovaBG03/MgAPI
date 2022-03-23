using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(string id);
        User Create(CreateUserRequest model);
        User Edit(EditUserRequest model);
        void ChangePassword(string id, ChangePasswordRequest model);
        void Delete(string id);
    }
}
