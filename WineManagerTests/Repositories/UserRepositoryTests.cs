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
        /// <summary>
        /// Testing GetAllUsersAsync
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Testing AddUserAsync
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Testing UpdateUserAsync
        /// with correct settings
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task UpdateUserAsyncTest1()
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

            // Testing with correct settings //
            Assert.AreEqual("newtest", UserDtoUploaded.Name);
            Assert.AreEqual("newtest", UserDtoUploaded.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), UserDtoUploaded.BirthDate);

            Assert.AreEqual("newtest", userToUpdate.Name);
            Assert.AreEqual("newtest", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2002, 02, 02), userToUpdate.BirthDate);
            Assert.AreEqual("newtest", userToUpdate.Password);



            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing UpdateUserAsync
        /// with wrong email.
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task UpdateUserAsyncTest2()
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
                CurrentEmail = "wrong",
                CurrentPassword = "test",
                NewName = "newtest",
                NewEmail = "newtest",
                NewBirthDate = new DateTime(2002, 02, 02),
                NewPassword = "newtest"
            });

            // Test with wrong email //
            Assert.IsNull(UserDtoUploaded);

            Assert.AreEqual("test", userToUpdate.Name);
            Assert.AreEqual("test", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2001, 01, 01), userToUpdate.BirthDate);
            Assert.AreEqual("test", userToUpdate.Password);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing UpdateUserAsync
        /// with wrong password.
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task UpdateUserAsyncTest3()
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
                CurrentPassword = "wrong",
                NewName = "newtest",
                NewEmail = "newtest",
                NewBirthDate = new DateTime(2002, 02, 02),
                NewPassword = "newtest"
            });

            // Test with wrong password //
            Assert.IsNull(UserDtoUploaded);

            Assert.AreEqual("test", userToUpdate.Name);
            Assert.AreEqual("test", userToUpdate.Email);
            Assert.AreEqual(new DateTime(2001, 01, 01), userToUpdate.BirthDate);
            Assert.AreEqual("test", userToUpdate.Password);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing DeleteUserAsync
        /// with a correct userId
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task DeleteUserAsyncTest1()
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

            // Process DeleteUserAsync with correct user ID //
            var userCorrectRequest = await userRepository.DeleteUserAsync(userToDelete.UserId);
            users = context.Users.ToList();

            // Tests with correct user ID //
            Assert.IsNotNull(userCorrectRequest);
            Assert.AreEqual(0, users.Count);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing DeleteUserAsync
        /// with a wrong userId
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task DeleteUserAsyncTest2()
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

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

    }

}