using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
