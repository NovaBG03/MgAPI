using MgAPI.Business.Authorization.Interfaces;
using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using MgAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MgAPI.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository repository, IJwtUtils jwtUtils)
        {
            _repository = repository;
            _jwtUtils = jwtUtils;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User user = _repository.ReadByUsername(model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            string jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.ReadAll();
        }

        public async Task<User> GetById(string id)
        {
            User user = await _repository.Read(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task<User> Create(CreateUserRequest model)
        {
            User user = new()
            {
                ID = Guid.NewGuid().ToString(),
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Username = model.Username,
                Email = model.Email,
                CreationDate = DateTime.Now,
                Role = (Role)Enum.Parse(typeof(Role), "Moderator"),
                PasswordHash = BCryptNet.HashPassword(model.Password)
            };

            await _repository.Create(user);

            return user;
        }

        public async Task<User> Edit(EditUserRequest model)
        {
            User user = await _repository.Read(model.ID);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Username = model.Username;
            user.Email = model.Email;
            //user.PasswordHash = BCryptNet.HashPassword(model.Password);

            await _repository.Update(user);

            return user;
        }

        public async Task ChangePassword(string id, ChangePasswordRequest model)
        {
            User user = await _repository.Read(id);

            if (user == null) throw new KeyNotFoundException("User not found");

            if (user.PasswordHash == BCryptNet.HashPassword(model.OldPassword))
            {
                user.PasswordHash = BCryptNet.HashPassword(model.NewPassword);
                await _repository.Update(user);
            }
            else
            {
                throw new ArgumentException("Wrong password!");
            }
        }

        public async Task Delete(string id)
        {
            if (!await _repository.Exists(id)) throw new KeyNotFoundException("User not found");

            await _repository.Delete(id);
        }

    }
}
