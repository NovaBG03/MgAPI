using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data.Entities;
using MgAPI.Data.Interfaces;
using System;
using System.Collections.Generic;

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

        public Post GetById(string id)
        {
            var post = _repository.Read(id);
            if (post == null) throw new KeyNotFoundException("Post not found");
            return post;
        }

        public Post Create(CreatePostRequest model)
        {
            User author = _userRepository.Read(model.AuthorID);

            if (author == null) throw new KeyNotFoundException("User not found");

            Post post = new Post
            {
                ID = Guid.NewGuid().ToString(),
                Author = author,
                Title = model.Title,
                Description = model.Description,
                PostDate = DateTime.Now
            };

            author.Posts.Add(post);

            _repository.Create(post);

            return post;
        }

        public Post Edit(EditPostRequest model)
        {
            Post post = _repository.Read(model.ID);

            if (post == null) throw new KeyNotFoundException("Post not found");

            post.Title = model.Title;
            post.Description = model.Description;

            _repository.Update(post);

            return post;
        }

        public void Delete(string id)
        {
            if (!_repository.Exists(id)) throw new KeyNotFoundException("Post not found");

            _repository.Delete(id);
        }
    }
}
