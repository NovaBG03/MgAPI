using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post GetById(string id);
        Post Create(CreatePostRequest model);
        Post Edit(EditPostRequest model);
        void Delete(string id);
    }
}
