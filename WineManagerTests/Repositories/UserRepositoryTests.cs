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
        var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
        var context = new WineManagerContext(builder.Options);
        UserRepository MyGetAllTest = new UserRepository(context, null);
        [TestMethod()]
        public async Task GetAllUsersAsyncTest()
        {
            var MyList = await MyGetAllTest.GetAllUsersAsync();

            Assert.AreEqual(0, MyList.Count);
        }

        [TestMethod()]
        public void AddUserAsyncTest()
        {

            Assert.Fail();
        }
        context.Database.EnsureDeleted();
    }
}