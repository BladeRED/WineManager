using Microsoft.VisualStudio.TestTools.UnitTesting;
using WineManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineManager.Contexts;
using Microsoft.EntityFrameworkCore.InMemory;

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public async Task GetAllUsersAsyncTest()
        {
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            UserRepository MyGetAllTest = new UserRepository(context, null);

            var MyList = await MyGetAllTest.GetAllUsersAsync();

            context.Database.EnsureDeleted();
            Assert.AreEqual(10, MyList.Count);
        }

        [TestMethod()]
        public void AddUserAsyncTest()
        {
            Assert.Fail();
        }
    }
}