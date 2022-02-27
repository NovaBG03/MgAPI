using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data;
using MgAPI.Data.Entities;
using MgAPI.Services.Authorization;
using MgAPI.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MgAPI.Business.Services
{
    public class WebFileService : IWebFileService
    {
        private Context _context;

        public WebFileService(Context context)
        {
            _context = context;
        }

        public IEnumerable<WebFile> GetAll()
        {
            return _context.Files;
        }

        public WebFile GetById(string id)
        {
            var file = _context.Files.Find(id);
            if (file == null) throw new KeyNotFoundException("File not found");
            return file;
        }

        public WebFile Create(CreateWebFileRequest model)
        {
            byte[] fileToUpload = File.ReadAllBytes(model.Localpath);
            string fileName = model.Localpath.Split(@"\").Last();
            string extension = model.Localpath.Split(".").Last();
            string serverFileName = "file" + _context.Files.Count() + "." + extension;                     
            string serverPath = @"..\MgAPI.Data\Files\" + serverFileName;

            Post post = _context.Posts.FirstOrDefault(x => x.ID == model.PostID);
            WebFile file = new WebFile
            {
                ID = Guid.NewGuid().ToString(),
                Post = post,
                Path = serverPath,
                Name = fileName,
                NameInServer = serverFileName,
                Extension = extension,
            };

            File.WriteAllBytes(serverPath, fileToUpload);

            _context.Files.Add(file);
            post.Files.Add(file);
            _context.SaveChanges();

            return file;
        }

        public void Delete(string id)
        {
            WebFile fileToDelete = _context.Files.FirstOrDefault(x => x.ID == id);
            if (fileToDelete == null) throw new KeyNotFoundException("File not found");
            File.Delete(fileToDelete.Path);

            _context.Files.Remove(fileToDelete);
            _context.SaveChanges();
        }
    }
}
