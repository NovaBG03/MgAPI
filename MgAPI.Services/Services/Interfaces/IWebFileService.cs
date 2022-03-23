using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IWebFileService
    {
        IEnumerable<WebFile> GetAll();
        WebFile GetById(string id);
        WebFile Create(CreateWebFileRequest model);
        void Delete(string id);
    }
}
