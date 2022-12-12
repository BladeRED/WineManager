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
            UserRepository userRepository = new UserRepository(context, null);

            // Process GetAllUsersAsync //
            var myList = await userRepository.GetAllUsersAsync();

            // Test
            Assert.AreEqual(0, myList.Count);

            // Bdd delete
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task AddUserAsyncTest()
        {
            // Bdd create //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");

            // Context //
            var context = new WineManagerContext(builder.Options);
            UserRepository userRepository = new UserRepository(context, null);

            // UserPostDto //
            var myUserPostDto = new UserPostDto()
            {
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2000, 01, 01),
                Password = "test"
            };

            // Process AddUserAsync //
            var myUserAdded = await userRepository.AddUserAsync(myUserPostDto);
            var users = await context.Users.ToListAsync();

            // Tests 
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("test", myUserAdded.Name);

            // Db delete
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task UpdateUserAsyncTest()
        {
            // Create BDD //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");

            // Create context //
            var context = new WineManagerContext(builder.Options);
            UserRepository userRepository = new UserRepository(context, null);

            // Create User //
            var userToUpdate = new User()
            {
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2001, 01, 01),
                Password = "test"
            };

            // Add user to the context //
            context.Add(userToUpdate);
            await context.SaveChangesAsync();

            // Process UpdateUserAsync //
            var UserDtoUploaded = await userRepository.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "test",
                CurrentPassword = "test",
                NewName = "newtest",
                NewEmail = "newtest",
                NewBirthDate = new DateTime(2002, 02, 02),
                NewPassword = "newtest"
            });

            // Tests : normal run //
            Assert.AreEqual("newtest", UserDtoUploaded.Name);
            Assert.AreEqual("newtest", UserDtoUploaded.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), UserDtoUploaded.BirthDate);

            Assert.AreEqual("newtest", userToUpdate.Name);
            Assert.AreEqual("newtest", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), userToUpdate.BirthDate);
            Assert.AreEqual("newtest", userToUpdate.Password);

            // Process UpdateUserAsync with wrong parameters : bad email //
            var userWrongDto = await userRepository.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "BadRequest",
                CurrentPassword = "newtest",
                NewName = "BRtest",
                NewEmail = "BRtest",
                NewBirthDate = new DateTime(2003, 03, 03),
                NewPassword = "BRtest"
            });

            // Test with wrong email //
            Assert.IsNull(userWrongDto);

            Assert.AreEqual("newtest", userToUpdate.Name);
            Assert.AreEqual("newtest", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), userToUpdate.BirthDate);
            Assert.AreEqual("newtest", userToUpdate.Password);

            // Process UpdateUserAsync with wrong parameters : bad password //
            userWrongDto = await userRepository.UpdateUserAsync(new UserPutDto
            {
                CurrentEmail = "newTest",
                CurrentPassword = "BadRequest",
                NewName = "BRtest",
                NewEmail = "BRtest",
                NewBirthDate = new DateTime(2003, 03, 03),
                NewPassword = "BRtest"
            });

            // Test with wrong password //
            Assert.IsNull(userWrongDto);

            Assert.AreEqual("newtest", userToUpdate.Name);
            Assert.AreEqual("newtest", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), userToUpdate.BirthDate);
            Assert.AreEqual("newtest", userToUpdate.Password);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        [TestMethod()]
        public async Task DeleteUserAsyncTest()
        {
            // Create BDD //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");

            // Create context //
            var context = new WineManagerContext(builder.Options);

            // Repository //
            UserRepository userRepository = new UserRepository(context, null);

            // Create User //
            var userToDelete = new User()
            {
                Name = "test",
                Email = "test",
                BirthDate = new DateTime(2001, 01, 01),
                Password = "test"
            };

            // Add user to the context //
            context.Add(userToDelete);
            await context.SaveChangesAsync();

            // User list //
            var users = context.Users.ToList();

            // Test user list count //
            Assert.AreEqual(1, users.Count);

            // Process DeleteUserAsync with wrong user ID //
            var userBadRequest = await userRepository.DeleteUserAsync(userToDelete.UserId + 1);
            users = context.Users.ToList();

            // Tests with wrong user ID //
            Assert.IsNull(userBadRequest);
            Assert.AreEqual(1, users.Count);

            // Process DeleteUserAsync with correct user ID //
            var userCorrectRequest = await userRepository.DeleteUserAsync(userToDelete.UserId);
            users = context.Users.ToList();

            // Tests with correct user ID //
            Assert.IsNotNull(userCorrectRequest);
            Assert.AreEqual(0, users.Count);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }
    }
}