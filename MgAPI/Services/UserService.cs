using MgAPI.JSONModels;
using MgAPI.Authorization;
using MgAPI.Helpers;
using MgAPI.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCryptNet = BCrypt.Net.BCrypt;
using MgAPI.Data;

namespace MgAPI.Services
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

    public class UserService : IUserService
    {
        private UsersContext _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public UserService(
            UsersContext context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }


        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Read(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.ReadAll();
        }

        public User GetById(string id)
        {
            var user = _context.Read(id);
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

            _context.Create(user);

            return user;
        }

        public User Edit(EditUserRequest model)
        {
            User user = _context.Read(x => x.ID == model.ID);
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Username = model.Username;
            user.Email = model.Email;
            //user.PasswordHash = BCryptNet.HashPassword(model.Password);

            _context.Update(user);

            return user;
        }
              
        public void ChangePassword(string id, ChangePasswordRequest model)
        {
            User user = _context.Read(x => x.ID == id);

            if (user.PasswordHash == BCryptNet.HashPassword(model.OldPassword))
            {
                user.PasswordHash = BCryptNet.HashPassword(model.NewPassword);
                _context.Update(user);
            }
            else
            {
                throw new ArgumentException("Wrong password!");
            }
        }

        public void Delete(string id)
        {
            _context.Delete(id);           
        }
         
    }
}
