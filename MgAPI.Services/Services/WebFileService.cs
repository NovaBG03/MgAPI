using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<WebFile> GetById(string id)
        {
            WebFile webFile = await _repository.Read(id);
            if (webFile == null) throw new KeyNotFoundException("File not found");
            return webFile;
        }

        public async Task<WebFile> Create(CreateWebFileRequest model)
        {
            byte[] fileToUpload = await File.ReadAllBytesAsync(model.Localpath);
            string fileName = model.Localpath.Split(@"\").Last();
            string extension = model.Localpath.Split(".").Last();
            string serverFileName = "file" + _repository.ReadAll().Count() + "." + extension;                     
            string serverPath = @"..\MgAPI.Data\Files\" + serverFileName;
            Post post = await _postRepository.Read(model.PostID);

            if (post == null) throw new KeyNotFoundException("Post not found");

            WebFile file = new()
            {
                ID = Guid.NewGuid().ToString(),
                Post = post,
                Path = serverPath,
                Name = fileName,
                NameInServer = serverFileName,
                Extension = extension,
            };

            await File.WriteAllBytesAsync(serverPath, fileToUpload);

            await _repository.Create(file);

            return file;
        }

        public async Task Delete(string id)
        {
            WebFile fileToDelete = await _repository.Read(id);
            if (fileToDelete == null) throw new KeyNotFoundException("File not found");
            File.Delete(fileToDelete.Path);

            await _repository.Delete(id);
        }
    }
}
