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
using WineManager.DTO;
using System.Collections;

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class UserRepositoryTests
    {
        [TestMethod()]
        public async Task GetAllUsersAsyncTest()
        {
            // Bdd create
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");

            // Context
            var context = new WineManagerContext(builder.Options);
            UserRepository testContext = new UserRepository(context, null);

            // List
            var myList = await testContext.GetAllUsersAsync();

            // Test
            Assert.AreEqual(0, myList.Count);

            // Bdd delete
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task AddUserAsyncTest()
        {
            // Bdd create
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");

            // Context
            var context = new WineManagerContext(builder.Options);
            UserRepository testContext = new UserRepository(context, null);

            // Object to inject
            var myUserPostDto = new UserPostDto()
            {
                Name= "test",
                Email= "test",
                BirthDate= new DateTime(2000, 01, 01),
                Password= "test"
            };

            
            var myUserAdded = await testContext.AddUserAsync(myUserPostDto);
            var myList = await testContext.GetAllUsersAsync();

            context.Database.EnsureDeleted();
            Assert.AreEqual(1, myList.Count);
        }
    }
}