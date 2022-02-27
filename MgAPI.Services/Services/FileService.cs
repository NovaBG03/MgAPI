using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data;
using MgAPI.Data.Entities;
using MgAPI.Services.Authorization;
using MgAPI.Services.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Business.Services
{
    public class FileService : IFileService
    {
        private Context _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public FileService(
            Context context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<File> GetAll()
        {
            return _context.Files;
        }

        public File GetById(string id)
        {
            var file = _context.Files.Find(id);
            if (file == null) throw new KeyNotFoundException("Post not found");
            return file;
        }

        public File Create(CreateFileRequest model)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.ID == model.PostID);
            File file = new File
            {
                ID = Guid.NewGuid().ToString(),
                Post = post,
                Path = model.Path,
                Name = model.Name,
                NameInServer = model.NameInServer,
                Extension = model.Extension,
            };


            _context.Files.Add(file);
            post.Files.Add(file);
            _context.SaveChanges();

            return file;
        }

        public void Delete(string id)
        {
            File file = _context.Files.FirstOrDefault(x => x.ID == id);
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }
}
