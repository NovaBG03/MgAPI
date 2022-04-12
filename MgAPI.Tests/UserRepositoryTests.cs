using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MgAPI.Data;
using MgAPI.Data.Entities;
using MgAPI.Data.Repositories;
using NUnit.Framework;

namespace MgAPI.Tests
{
    public class UserRepositoryTests
    {
        UserRepository repository = new UserRepository(new Context());

        [TearDown]
        public async Task ClearUsersAfterEachTest()
        {
            List<User> users = repository.ReadAll().ToList();

            for (int i = 0; i < users.Count; i++)
            {
                await repository.Delete(users[i].ID);
            }

        }

        [Test]
        public async Task AfterCreateUsersCountShouldReturnOne()
        {
            await repository.Create(new User() { ID = Guid.NewGuid().ToString() });
            int count = repository.ReadAll().Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AfterReadUserIdIsCorrect()
        {
            await repository.Create(new User() { ID = "1" });
            User user = await repository.Read("1");

            Assert.AreEqual(user.ID, "1");
        }

        [Test]
        public async Task AfterUpdateUserHasNewName()
        {
            await repository.Create(new User() { ID = "1", Username = "A" });
            User user = await repository.Read("1");
            await repository.Update(new User() { ID = "1", Username = "B" });

            Assert.AreEqual(user.Username, "B");
        }

        [Test]
        public async Task AfterDeleteUsersAreZero()
        {
            await repository.Create(new User() { ID = "1", Username = "A" });
            await repository.Delete("1");
            int count = repository.ReadAll().Count();

            Assert.AreEqual(0, count);
        }
    }
}
