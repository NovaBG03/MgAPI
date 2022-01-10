using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MgAPI.Data;
using MgAPI.JSONModels;
using MgAPI.Authorization;
using MgAPI.Helpers;
using MgAPI.Models;
using Microsoft.Extensions.Options;

namespace MgAPI.Services
{
    public interface IPostService
    {
        IEnumerable<Post> GetAll();
        Post GetById(string id);
        Post Create(CreatePostRequest model);
        Post Edit(EditPostRequest model);
        void Delete(string id);
    }
    public class PostService : IPostService
    {
        private Context _context;
        private IJwtUtils _jwtUtils;
        private readonly AppSettings _appSettings;

        public PostService(
            Context context,
            IJwtUtils jwtUtils,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }

        public Post GetById(string id)
        {
            var post = _context.Posts.Find(id);
            if (post == null) throw new KeyNotFoundException("Post not found");
            return post;
        }

        public Post Create(CreatePostRequest model)
        {
            User author = _context.Users.FirstOrDefault(x => x.ID == model.AuthorID);
            Post post = new Post
            {
                ID = Guid.NewGuid().ToString(),
                Author = author,
                Title = model.Title,
                Description = model.Description,
                PostDate = DateTime.Now
            };

            
            _context.Posts.Add(post);
            author.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        public Post Edit(EditPostRequest model)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.ID == model.ID);
            post.Title = model.Title;
            post.Description = model.Description;

            _context.SaveChanges();

            return post;
        }

        public void Delete(string id)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.ID == id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
    }
}
