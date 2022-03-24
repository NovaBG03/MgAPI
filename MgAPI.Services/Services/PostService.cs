using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MgAPI.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public IEnumerable<Post> GetAll()
        {
            return _repository.ReadAll();
        }

        public async Task<Post> GetById(string id)
        {
            Post post = await _repository.Read(id);
            if (post == null) throw new KeyNotFoundException("Post not found");
            return post;
        }

        public async Task<Post> Create(CreatePostRequest model)
        {
            User author = await _userRepository.Read(model.AuthorID);

            if (author == null) throw new KeyNotFoundException("User not found");

            Post post = new()
            {
                ID = Guid.NewGuid().ToString(),
                Author = author,
                Title = model.Title,
                Description = model.Description,
                PostDate = DateTime.Now
            };

            author.Posts.Add(post);

            await _repository.Create(post);

            return post;
        }

        public async Task<Post> Edit(EditPostRequest model)
        {
            Post post = await _repository.Read(model.ID);

            if (post == null) throw new KeyNotFoundException("Post not found");

            post.Title = model.Title;
            post.Description = model.Description;

            await _repository.Update(post);

            return post;
        }

        public async Task Delete(string id)
        {
            if (!await _repository.Exists(id)) throw new KeyNotFoundException("Post not found");

            await _repository.Delete(id);
        }
    }
}
