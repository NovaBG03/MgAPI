using MgAPI.Business.JSONModels;
using MgAPI.Business.Services.Interfaces;
using MgAPI.Data;
using MgAPI.Data.Entities;
using MgAPI.Services.Authorization;
using MgAPI.Services.Helpers;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Business.Services
{
    public class PostService : IPostService
    {
        private Context _context;

        public PostService(Context context)
        {
            _context = context;
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
