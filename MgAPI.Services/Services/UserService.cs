using MgAPI.Business.Authorization.Interfaces;
using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using MgAPI.Services.Helpers;
using System;
using System.Collections.Generic;
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
            var user = _repository.ReadByUsername(model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _repository.ReadAll();
        }

        public User GetById(string id)
        {
            var user = _repository.Read(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public User Create(CreateUserRequest model)
        {
            User user = new User
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

            _repository.Create(user);

            return user;
        }

        public User Edit(EditUserRequest model)
        {
            User user = _repository.Read(model.ID);
            if (user == null) throw new KeyNotFoundException("User not found");

            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Username = model.Username;
            user.Email = model.Email;
            //user.PasswordHash = BCryptNet.HashPassword(model.Password);

            _repository.Update(user);

            return user;
        }

        public void ChangePassword(string id, ChangePasswordRequest model)
        {
            User user = _repository.Read(id);

            if (user == null) throw new KeyNotFoundException("User not found");

            if (user.PasswordHash == BCryptNet.HashPassword(model.OldPassword))
            {
                user.PasswordHash = BCryptNet.HashPassword(model.NewPassword);
                _repository.Update(user);
            }
            else
            {
                throw new ArgumentException("Wrong password!");
            }
        }

        public void Delete(string id)
        {
            if (!_repository.Exists(id)) throw new KeyNotFoundException("User not found");

            _repository.Delete(id);
        }

    }
}
