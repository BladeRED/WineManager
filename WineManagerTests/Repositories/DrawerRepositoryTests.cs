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

namespace WineManager.Repositories.Tests
{
    [TestClass()]
    public class DrawerRepositoryTests
    {
        [TestMethod()]
        public async Task AddDrawerAsyncTest()
        {
            // creation of the temp database and its context //

            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            DrawerRepository DrawerTest = new DrawerRepository(context);

            // creation of the object to add //

            Drawer TestDrawer = new Drawer()
            {
                DrawerId = 1,
                Level= 1,
                MaxPosition= 1,
            };

            // simulating the add method //

            var MyAddTest = await DrawerTest.AddDrawerAsync(TestDrawer);
            context.Drawers.Add(TestDrawer);

            var MyList = await DrawerTest.GetDrawersAsync();

            // comparing the list of objects to see if there is a new entry in the database //

            Assert.AreEqual(1, MyList.Count);

            context.Database.EnsureDeleted();

        }

        [TestMethod()]
        public void UpdateDrawerAsyncTest()
        {
            Assert.Fail();
        }
    }
}