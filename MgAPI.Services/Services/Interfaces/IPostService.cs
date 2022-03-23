using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;

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
