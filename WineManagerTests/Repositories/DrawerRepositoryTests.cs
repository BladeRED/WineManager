﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /// TO DO : wrong paramaters tests
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
    }
}