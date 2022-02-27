using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IFileService
    {
        IEnumerable<File> GetAll();
        File GetById(string id);
        File Create(CreateFileRequest model);
        void Delete(string id);
    }
}
