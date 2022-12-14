using Microsoft.VisualStudio.TestTools.UnitTesting;
using WineManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WineManager.Contexts;
using WineManager.Entities;
using System.Drawing;
using WineManager.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class BottleRepositoryTests
    {
        private readonly ILogger<BottleRepository>? logger;

        /// <summary>
        /// Testing AddBottleAsync
        /// with full correct BottleDTO
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task AddBottleAsyncTest1()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            var bottleRepository = new BottleRepository(context, null);


            // Empty Drawer //
            var drawer = new Drawer()
            {
                MaxPosition = 2,
                UserId = 1,
            };
            context.Add(drawer);
            context.SaveChanges();

            // Creation of a bottleDTO to add //
            var bottleDto = new BottleDto()
            {
                Name = "name",
                Vintage = 2000,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "color",
                Designation = "Designation",
                DrawerId = drawer.DrawerId,
                DrawerPosition = "Position"
            };

            // Process AddBottleAsync //
            var bottleAdded = await bottleRepository.AddBottleAsync(bottleDto, 1);
            var bottles = await context.Bottles.ToListAsync();

            // Testing a correct BottleDTO //
            Assert.AreEqual(1, bottles.Count);
            Assert.IsNotNull(bottleAdded);
            Assert.AreEqual("name", bottleAdded.Name);
            Assert.AreEqual(2000, bottleAdded.Vintage);
            Assert.AreEqual(5, bottleAdded.StartKeepingYear);
            Assert.AreEqual(8, bottleAdded.EndKeepingYear);
            Assert.AreEqual("color", bottleAdded.Color);
            Assert.AreEqual("Designation", bottleAdded.Designation);
            Assert.AreEqual(drawer.DrawerId, bottleAdded.DrawerId);
            Assert.AreEqual("Position", bottleAdded.DrawerPosition);

            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing AddBottleAsync
        /// with StartKeepingYear > EndKeepingYear
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task AddBottleAsyncTest2()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            var bottleRepository = new BottleRepository(context, null);

            // Empty Drawer //
            var drawer = new Drawer()
            {
                MaxPosition = 2,
                UserId = 1,
            };
            context.Add(drawer);
            context.SaveChanges();

            // Creation of a bottleDTO to add //
            var bottleDto = new BottleDto()
            {
                Name = "test",
                Vintage = 2000,
                StartKeepingYear = 8,
                EndKeepingYear = 5,
                Color = "Red",
                Designation = "test",
                DrawerId = drawer.DrawerId,
                DrawerPosition = "B1"
            };

            // Process AddBottleAsync with bad DTO : StartKeepingYear > EndKeepingYear //
            var badBottle = await bottleRepository.AddBottleAsync(bottleDto, 1);
            var bottles = await context.Bottles.ToListAsync();

            // Testing bottle list count and the null return //
            Assert.AreEqual(0, bottles.Count);
            Assert.IsNull(badBottle);

            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing AddBottleAsync
        /// with a full Drawer
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task AddBottleAsyncTest3()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            var bottleRepository = new BottleRepository(context, null);

            // Empty Drawer //
            var drawer = new Drawer()
            {
                MaxPosition = 2,
                UserId = 1,
            };
            context.Add(drawer);
            context.SaveChanges();

            // Creation of a bottleDTO to add //
            var bottleDto = new BottleDto()
            {
                Name = "test",
                Vintage = 2000,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "Red",
                Designation = "test",
                DrawerId = drawer.DrawerId,
                DrawerPosition = "B1"
            };

            // Process AddBottleAsync with a full Drawer //
            var bottle1 = await bottleRepository.AddBottleAsync(bottleDto, 1);
            bottleDto.DrawerPosition = "B2";
            var bottle2 = await bottleRepository.AddBottleAsync(bottleDto, 1);
            bottleDto.DrawerPosition = "B3";
            var bottle3 = await bottleRepository.AddBottleAsync(bottleDto, 1);
            var bottles = await context.Bottles.ToListAsync();

            // Testing bottle list count and the null return //
            Assert.AreEqual(2, bottles.Count);
            Assert.IsNotNull(bottle1);
            Assert.IsNotNull(bottle2);
            Assert.IsNull(bottle3);

            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing UpdateBottle
        /// with correct setting
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task UpdateBottleAsyncTest1()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            BottleRepository bottleRepository = new BottleRepository(context, null);

            // Empty Drawer //
            var drawer = new Drawer()
            {
                MaxPosition = 2,
                UserId = 1,
            };
            context.Add(drawer);
            context.SaveChanges();

            // Creation of a Bottle //
            Bottle bottleToUpdate = new Bottle()
            {
                Name = "test",
                Vintage = 2000,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "Red",
                Designation = "test",
                DrawerId = drawer.DrawerId,
                DrawerPosition = "B1",
                UserId = drawer.UserId,
            };
            context.Add(bottleToUpdate);
            context.SaveChanges();
            Assert.IsNotNull(bottleToUpdate.BottleId);

            // Creation of a BottleDotPut for process //
            BottleDtoPut bottleDtoPut = new BottleDtoPut()
            {
                BottleId = bottleToUpdate.BottleId,
                Name = "test2",
                Vintage = 2001,
                StartKeepingYear = 6,
                EndKeepingYear = 9,
                Color = "White",
                Designation = "test2",
            };

            // Process UpdateBottleAsync //
            var bottleUpdated = await bottleRepository.UpdateBottleAsync(bottleDtoPut, 1);

            // Tests : normal run //
            Assert.AreEqual(1, bottleUpdated.BottleId);
            Assert.AreEqual("test2", bottleUpdated.Name);
            Assert.AreEqual(2001, bottleUpdated.Vintage);
            Assert.AreEqual(6, bottleUpdated.StartKeepingYear);
            Assert.AreEqual(9, bottleUpdated.EndKeepingYear);
            Assert.AreEqual("White", bottleUpdated.Color);
            Assert.AreEqual("test2", bottleUpdated.Designation);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing UpdateBottle
        /// with
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task UpdateBottleAsyncTest2()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            BottleRepository bottleRepository = new BottleRepository(context, null);

            // Empty Drawer //
            var drawer = new Drawer()
            {
                MaxPosition = 2,
                UserId = 1,
            };
            context.Add(drawer);
            context.SaveChanges();

            // Creation of a Bottle //
            Bottle bottleToUpdate = new Bottle()
            {
                Name = "test",
                Vintage = 2000,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "Red",
                Designation = "test",
                DrawerId = drawer.DrawerId,
                DrawerPosition = "B1",
                UserId = drawer.UserId,
            };
            context.Add(bottleToUpdate);
            context.SaveChanges();
            Assert.IsNotNull(bottleToUpdate.BottleId);

            // Creation of a BottleDotPut for process //
            BottleDtoPut bottleDtoPut = new BottleDtoPut()
            {
                BottleId = bottleToUpdate.BottleId,
                Name = "test2",
                Vintage = 2001,
                StartKeepingYear = 8,
                EndKeepingYear = 5,
                Color = "White",
                Designation = "test2",
            };

            // Process UpdateBottleAsync : StartKeepingYear > EndKeepingYear //

            var bottleUpdated = await bottleRepository.UpdateBottleAsync(bottleDtoPut, 1);
            var bottles = await context.Bottles.ToListAsync();

            // Tests : bad keeping years //
            Assert.AreEqual(1, bottles.Count());
            Assert.IsNull(bottleUpdated);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }
    }
}