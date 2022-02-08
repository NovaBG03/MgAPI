using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MgAPI.Data;
using MgAPI.JSONModels;
using MgAPI.Authorization;
using MgAPI.Helpers;
using MgAPI.Models;
using Microsoft.Extensions.Options;

namespace MgAPI.Services
{
    public interface IFileService
    {
        IEnumerable<File> GetAll();
        File GetById(int id);
        File Create(CreateFileRequest model);
        void Delete(int id);
    }
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

        public File GetById(int id)
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
                ID =int.Parse(Guid.NewGuid().ToString()),
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

        public void Delete(int id)
        {
            File file = _context.Files.FirstOrDefault(x => x.ID == id);
            _context.Files.Remove(file);
            _context.SaveChanges();
        }
    }
}
