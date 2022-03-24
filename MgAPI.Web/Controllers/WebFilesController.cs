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
    [ApiController]
    [Route("[controller]")]
    public class WebFilesController : ControllerBase
    {
        private readonly IWebFileService _webFileService;

        public WebFilesController(IWebFileService webFileService)
        {
            _webFileService = webFileService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<WebFile> files = _webFileService.GetAll();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            // only admins can access file records
            User currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            WebFile file = await _webFileService.GetById(id);
            return Ok(file);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateWebFileRequest model)
        {
            try
            {
                WebFile file = await _webFileService.Create(model);
                return CreatedAtAction("create", file);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }


        [Authorize(Role.Admin, Role.Moderator)]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            await _webFileService.Delete(id);
            return Ok(new JSONMessage("File deleted successfully!"));
        }
    }
}
