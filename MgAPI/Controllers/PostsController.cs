using MgAPI.JSONModels;
using MgAPI.Authorization;
using MgAPI.Models;
using MgAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MgAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var posts = _postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // only admins can access post records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var post = _postService.GetById(id);
            return Ok(post);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public IActionResult Create(CreatePostRequest model)
        {
            try
            {
                Post post = _postService.Create(model);
                return CreatedAtAction("create", post);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPatch("[action]")]
        public IActionResult Edit(EditPostRequest model)
        {

            var currentUser = (User)HttpContext.Items["User"];
            if (model.ID != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            try
            {
                Post post = _postService.Edit(model);
                return Ok(post);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpDelete("[action]/{id}")]
        public IActionResult Delete(string id)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            _postService.Delete(id);
            return Ok("Post deleted successfully!");
        }
    }
}
