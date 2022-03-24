using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Services.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MgAPI.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            AuthenticateResponse response = _userService.Authenticate(model);
            return Ok(response);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<User> users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            // only admins can access other user records
            User currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            User user = await _userService.GetById(id);
            return Ok(user);
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUserRequest model)
        {
            try
            {
                User user = await _userService.Create(model);
                return CreatedAtAction("create", user);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async Task<IActionResult> Edit(EditUserRequest model)
        {

            User currentUser = (User)HttpContext.Items["User"];
            if (model.ID != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            try
            {
                User user = await _userService.Edit(model);
                return Ok(user);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest model)
        {

            User currentUser = (User)HttpContext.Items["User"];

            try
            {
                await _userService.ChangePassword(currentUser.ID, model);
                return Ok(new JSONMessage("Password updated successfully!"));
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User currentUser = (User)HttpContext.Items["User"];
            if (id == currentUser.ID && currentUser.Role == Role.Admin)
                return Conflict(new JSONMessage("Cannot delete admin profile!"));

            await _userService.Delete(id);
            return Ok(new JSONMessage("User deleted successfully!"));
        }

    }
}
