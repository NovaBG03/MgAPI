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
    public class WebFilesController : ControllerBase
    {
        private IWebFileService _fileService;

        public WebFilesController(IWebFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var files = _fileService.GetAll();
            return Ok(files);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            // only admins can access file records
            var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.ID && currentUser.Role != Role.Admin)
                return Unauthorized(new JSONMessage("Unauthorized"));

            var file = _fileService.GetById(id);
            return Ok(file);
        }

        [Authorize(Role.Admin, Role.Moderator)]
        [HttpPost("[action]")]
        public IActionResult Create(CreateWebFileRequest model)
        {
            try
            {
                WebFile file = _fileService.Create(model);
                return CreatedAtAction("create", file);
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
                return Unauthorized(new JSONMessage("Unauthorized"));

            _fileService.Delete(id);
            return Ok(new JSONMessage("File deleted successfully!"));
        }
    }
}
