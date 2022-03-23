using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MgAPI.Business.Services
{
    public class WebFileService : IWebFileService
    {
        private readonly IWebFileRepository _repository;
        private readonly IPostRepository _postRepository;

        public WebFileService(IWebFileRepository repository, IPostRepository postRepository)
        {
            _repository = repository;
            _postRepository = postRepository;
        }

        public IEnumerable<WebFile> GetAll()
        {
            return _repository.ReadAll();
        }

        public WebFile GetById(string id)
        {
            var file = _repository.Read(id);
            if (file == null) throw new KeyNotFoundException("File not found");
            return file;
        }

        public WebFile Create(CreateWebFileRequest model)
        {
            byte[] fileToUpload = File.ReadAllBytes(model.Localpath);
            string fileName = model.Localpath.Split(@"\").Last();
            string extension = model.Localpath.Split(".").Last();
            string serverFileName = "file" + _repository.ReadAll().Count() + "." + extension;                     
            string serverPath = @"..\MgAPI.Data\Files\" + serverFileName;
            Post post = _postRepository.Read(model.PostID);

            if (post == null) throw new KeyNotFoundException("Post not found");

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

            _repository.Create(file);

            return file;
        }

        public void Delete(string id)
        {
            WebFile fileToDelete = _repository.Read(id);
            if (fileToDelete == null) throw new KeyNotFoundException("File not found");
            File.Delete(fileToDelete.Path);

            _repository.Delete(id);
        }
    }
}
