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
using WineManager.Entities;
using Microsoft.Extensions.Logging;

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
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2000, 01, 01),
                Password = "test"
            };

            
            var myUserAdded = await testContext.AddUserAsync(myUserPostDto);
            var myList = await testContext.GetAllUsersAsync();

            // Tests 
            Assert.AreEqual(1, myList.Count);
            Assert.AreEqual("test", myUserAdded.Name);

            // Db delete
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task UpdateUserAsyncTest()
        {
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            UserRepository testContext = new UserRepository(context, null);


            var myUserPostDto = new UserPostDto()
            {
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2001, 01, 01),
                Password = "test"
            };
            var myUserAdded = await testContext.AddUserAsync(myUserPostDto);
            var userFinded = await context.Users.FirstAsync(u => u.Email == "test");
            Assert.AreEqual("test", userFinded.Name);
            var myUserUploaded = await testContext.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "test",
                CurrentPassword = "test",
                NewName = "newtest",
                NewEmail = "newtest",
                NewBirthDate = new DateTime(2002, 02, 02),
                NewPassword = "newtest"
            });
            Assert.AreSame("newtest", userFinded.Name);
            Assert.AreEqual("newtest", userFinded.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), userFinded.BirthDate);
            Assert.AreEqual("newtest", userFinded.Password);

            var myUserBabRequest = await testContext.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "BadRequest",
                CurrentPassword = "newtest",
                NewName = "BRtest",
                NewEmail = "BRtest",
                NewBirthDate = new DateTime(2003, 03, 03),
                NewPassword = "BRtest"
            });
            Assert.AreNotEqual("BRtest", userFinded.Name);
            Assert.AreNotEqual("BRtest", userFinded.Email);
            Assert.AreNotEqual(new DateTime(2003, 03, 03), userFinded.BirthDate);
            Assert.AreNotEqual("BRtest", userFinded.Password);

            myUserBabRequest = await testContext.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "newtest",
                CurrentPassword = "BadRequest",
                NewName = "BRtest",
                NewEmail = "BRtest",
                NewBirthDate = new DateTime(2003, 03, 03),
                NewPassword = "BRtest"
            });
            Assert.AreNotEqual("BRtest", userFinded.Name);
            Assert.AreNotEqual("BRtest", userFinded.Email);
            Assert.AreNotEqual(new DateTime(2003, 03, 03), userFinded.BirthDate);
            Assert.AreNotEqual("BRtest", userFinded.Password);

            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task DeleteUserAsyncTest()
        {
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            UserRepository testContext = new UserRepository(context, null);

            var myUserPostDto = new UserPostDto()
            {
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2000, 01, 01),
                Password = "test"
            };

            var myUserAdded = await testContext.AddUserAsync(myUserPostDto);
            var myList = await testContext.GetAllUsersAsync();

            Assert.AreEqual(1, myList.Count);

            var userBadRequest = await testContext.DeleteUserAsync(-1);
            myList = await testContext.GetAllUsersAsync();

            Assert.IsNull(userBadRequest);
            Assert.AreEqual(1, myList.Count);

            var id = (await context.Users.FirstAsync(u => u.Email == "test")).UserId;
            var UserDeleted = await testContext.DeleteUserAsync(id);
            myList = await testContext.GetAllUsersAsync();

            Assert.IsNotNull(UserDeleted);
            Assert.AreEqual(0, myList.Count);

            context.Database.EnsureDeleted();
        }
    }
}