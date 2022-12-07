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
    public class BottleRepositoryTests
    {
        [TestMethod()]
        public async Task AddBottleAsyncTest()
        {
            // creation of the temp database and its context //

            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            BottleRepository BottleTest = new BottleRepository(context, null);

            // creation of the object to add //

            Bottle TestBottle = new Bottle()
            {
                BottleId = 1,
                Name= "Test",
                Color = "Rouge",
                Vintage= 2020,
                Designation= "Domaine de Test",
                StartKeepingYear=2022,
                EndKeepingYear=2024,
                
                
            };

            // simulating the add method //

            var MyAddTest = await BottleTest.AddBottleAsync(TestBottle);
            context.Bottles.Add(TestBottle);

            var MyList = await BottleTest.GetAllBottlesAsync();

            // comparing the list of objects to see if there is a new entry in the database //

            Assert.AreEqual(1, MyList.Count);

            context.Database.EnsureDeleted();

        }

        [TestMethod()]
        public void UpdateBottleAsyncTest()
        {
            Assert.Fail();
        }
    }
}