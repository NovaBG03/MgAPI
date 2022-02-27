using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // only admins can access other user records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            var user = _userService.GetById(id);
            return Ok(user);
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public IActionResult Create(CreateUserRequest model)
        {
            try
            {
                User user = _userService.Create(model);
                return CreatedAtAction("create", user);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public IActionResult Edit(EditUserRequest model)
        {

            var currentUser = (User)HttpContext.Items["User"];
            if (model.ID != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            try
            {
                User user = _userService.Edit(model);
                return Ok(user);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public IActionResult ChangePassword(ChangePasswordRequest model)
        {

            var currentUser = (User)HttpContext.Items["User"];

            try
            {
                _userService.ChangePassword(currentUser.ID, model);
                return Ok(new JSONMessage("Password updated successfully!"));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (id == currentUser.ID && currentUser.Role == Role.Admin)
                return Conflict(new JSONMessage("Cannot delete admin profile!"));

            _userService.Delete(id);
            return Ok(new JSONMessage("User deleted successfully!"));
        }

    }
}
