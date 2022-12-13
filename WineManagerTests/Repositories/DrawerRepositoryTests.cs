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
using WineManager.DTO;

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class DrawerRepositoryTests
    {
        /// <summary>
        /// Testing AddDrawerAsync
        /// with correct parameters
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task AddDrawerAsyncTest()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            DrawerRepository drawerRepository = new DrawerRepository(context, null);

            // Creation of the drawer to add //
            Drawer drawer = new Drawer()
            {
                UserId = 1,
            };

            // Creation drawer list //
            var drawers = await context.Drawers.ToListAsync();
            Assert.AreEqual(0, drawers.Count);

            // Process the AddDrawerAsync //
            var MyAddTest = await drawerRepository.AddDrawerAsync(drawer);
            drawers = await context.Drawers.ToListAsync();
            Assert.AreEqual(1, drawers.Count);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }

        /// <summary>
        /// Testing StockDrawerAsync
        /// with correct parameters
        /// </summary>
        /// <returns></returns>
        [TestMethod()]
        public async Task StockDrawerAsyncTest()
        {
            // Creation of the temp database and its context //
            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            DrawerRepository drawerRepository = new DrawerRepository(context, null);

            // Creation of cave //
            Cave cave = new Cave()
            {
                UserId= 1,
                NbMaxBottlePerDrawer = 2,
                NbMaxDrawer = 2,
                CaveType = "Testing cave"
            };
            await context.Caves.AddAsync(cave);

            // Creation of drawers to add //
            Drawer drawer = new Drawer()
            {
                UserId = 1,
                MaxPosition= 2,
            };
            await context.Drawers.AddAsync(drawer);
            await context.SaveChangesAsync();

            // Process StockDrawerAsync //
            var drawerTest = await drawerRepository.StockDrawerAsync(drawer.DrawerId, cave.CaveId, 1, 1);

            // Tests //
            Assert.IsNotNull(drawerTest);
            Assert.AreEqual(drawer.DrawerId, drawerTest.DrawerId);
            Assert.AreEqual(cave.CaveId, drawerTest.CaveId);
            Assert.AreEqual(1, drawerTest.Level);

            // Delete BDD //
            context.Database.EnsureDeleted();
        }
    }
}