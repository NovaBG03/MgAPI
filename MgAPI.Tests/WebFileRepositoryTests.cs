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
    public class WebFileRepositoryTests
    {
        WebFileRepository repository = new WebFileRepository(new Context());

        [TearDown]
        public async Task ClearFilesAfterEachTest()
        {
            List<WebFile> files = repository.ReadAll().ToList();

            for (int i = 0; i < files.Count; i++)
            {
                await repository.Delete(files[i].ID);
            }

        }

        [Test]
        public async Task AfterCreateFilesCountShouldReturnOne()
        {
            await repository.Create(new WebFile() { ID = Guid.NewGuid().ToString() });
            int count = repository.ReadAll().Count();

            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task AfterReadFileIdShouldBeCorrect()
        {
            await repository.Create(new WebFile() { ID = "1" });
            WebFile file = await repository.Read("1");

            Assert.AreEqual(file.ID, "1");
        }

        [Test]
        public async Task AfterUpdateFileShouldHaveNewTitle()
        {
            await repository.Create(new WebFile() { ID = "1", Name = "A" });
            WebFile file = await repository.Read("1");
            await repository.Update(new WebFile() { ID = "1", Name = "B" });

            Assert.AreEqual(file.Name, "B");
        }

        [Test]
        public async Task AfterDeleteFilesShouldBeZero()
        {
            await repository.Create(new WebFile() { ID = "1", Name = "A" });
            await repository.Delete("1");
            int count = repository.ReadAll().Count();

            Assert.AreEqual(0, count);
        }
    }
}
