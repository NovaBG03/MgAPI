using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IWebFileService
    {
        IEnumerable<WebFile> GetAll();
        Task<WebFile> GetById(string id);
        Task<WebFile> Create(CreateWebFileRequest model);
        Task Delete(string id);
    }
}
