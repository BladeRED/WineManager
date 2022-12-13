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
using WineManager.Entities;

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class CaveRepositoryTests
    {
        /// <summary>
        /// Testing AddCaveAsyncTest
        /// with correct parameters
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task AddCaveAsyncTest()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            CaveRepository caveRepository = new CaveRepository(context, null);

            // Creation of the object to add //
            Cave cave = new Cave()
            {
                CaveId = 1,
                CaveType = "test",
                Brand = "test",
                Family = "test",
                Temperature = 12,
            };

            // Simulating the add method //
            var caveAdded = await caveRepository.AddCaveAsync(cave);
            context.Caves.Add(cave);

            var caves = await context.Caves.ToListAsync();

            // Comparing the list of objects to see if there is a new entry in the database //
            Assert.AreEqual(1, caves.Count);
            Assert.IsNotNull(caveAdded);

            // Delete BDD //
            context.Database.EnsureDeleted();

        }
    }
}