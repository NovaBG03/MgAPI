using MgAPI.Data;
using MgAPI.Data.Entities;
using MgAPI.Data.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgAPI.Tests
{
    public class PostRepositoryTests
    {
        PostRepository repository = new PostRepository(new Context());

        [TearDown]
        public async Task ClearPostsAfterEachTest()
        {
            List<Post> posts = repository.ReadAll().ToList();

            for (int i = 0; i < posts.Count; i++)
            {
                await repository.Delete(posts[i].ID);
            }

        }

        [Test]
        public async Task AfterCreatePostsCountShouldReturnOne()
        {
            await repository.Create(new Post() { ID = Guid.NewGuid().ToString() });
            int count = repository.ReadAll().Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AfterReadPostIdShouldBeCorrect()
        {
            await repository.Create(new Post() { ID = "1" });
            Post post = await repository.Read("1");

            Assert.AreEqual(post.ID, "1");
        }

        [Test]
        public async Task AfterUpdatePostShouldHaveNewTitle()
        {
            await repository.Create(new Post() { ID = "1", Title = "A" });
            Post post = await repository.Read("1");
            await repository.Update(new Post() { ID = "1", Title = "B" });

            Assert.AreEqual(post.Title, "B");
        }

        [Test]
        public async Task AfterDeletePostsShouldBeZero()
        {
            await repository.Create(new Post() { ID = "1", Title = "A" });
            await repository.Delete("1");
            int count = repository.ReadAll().Count();

            Assert.AreEqual(0, count);
        }
    }
}
