using MgAPI.Business.JSONModels;
using MgAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MgAPI.Business.Services.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Task<Post> GetById(string id);
        Task<Post> Create(CreatePostRequest model);
        Task<Post> Edit(EditPostRequest model);
        Task Delete(string id);
    }
}
