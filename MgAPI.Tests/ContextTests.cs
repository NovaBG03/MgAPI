using MgAPI.Data;
using MgAPI.Data.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MgAPI.Tests
{
    public class ContextTests
    {
        Context context = new Context();


        [TearDown]
        public void ClearAfterEachTest()
        {
            List<User> users = context.Users.ToList();

            for (int i = 0; i < users.Count; i++)
            {
                context.Users.Remove(users[i]);
                context.SaveChanges();
            }

            List<Post> posts = context.Posts.ToList();

            for (int i = 0; i < users.Count; i++)
            {
                context.Posts.Remove(posts[i]);
                context.SaveChanges();
            }

            List<WebFile> files = context.Files.ToList();

            for (int i = 0; i < users.Count; i++)
            {
                context.Files.Remove(files[i]);
                context.SaveChanges();
            }
        }

        [Test]
        public void WithNoUsersCountShouldReturnZero()
        {
            var count = context.Users.Count();

            Assert.AreEqual(0, count);
        }

        [Test]
        public void AfterAddingUser_CountShouldReturnOne()
        {
            User user = new User { ID = "admin1", Firstname = "Admin", Lastname = "User", Username = "admin", PasswordHash = "admin", Role = Role.Admin };

            context.Users.Add(user);
            context.SaveChanges();
            var count = context.Users.Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public void WithNoPosts_CountShouldReturnZero()
        {
            var count = context.Posts.Count();

            Assert.AreEqual(0, count);
        }

        [Test]
        public void AfterAddingPost_CountShouldReturnOne()
        {
            context.Posts.Add(new Post() { ID = Guid.NewGuid().ToString() });
            context.SaveChanges();
            var count = context.Posts.Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public void WithNoFiles_CountShouldReturnZero()
        {
            var count = context.Files.Count();

            Assert.AreEqual(0, count);
        }

        [Test]
        public void AfterAddingFile_CountShouldReturnOne()
        {
            context.Files.Add(new WebFile() { ID = Guid.NewGuid().ToString() });
            context.SaveChanges();
            var count = context.Files.Count();

            Assert.AreEqual(1, count);
        }
    }
}