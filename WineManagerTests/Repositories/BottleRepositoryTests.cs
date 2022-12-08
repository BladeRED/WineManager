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
                Name = "Test",
                Color = "Rouge",
                Vintage = 2020,
                Designation = "Domaine de Test",
                StartKeepingYear = 2022,
                EndKeepingYear = 2024,


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
        public async Task UpdateBottleAsyncTest()
        {
            // creation of the temp database and its context //

            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            BottleRepository BottleTest = new BottleRepository(context, null);

            Bottle MajBottle1 = new Bottle()
            {
                BottleId = 2,
                Name = "Test",
                Color = "Rouge",
                Vintage = 2020,
                Designation = "Domaine de Test",
                StartKeepingYear = 2022,
                EndKeepingYear = 2024,
            };

            BottleDtoPut MajBottle = new BottleDtoPut()
            {
                BottleId = 2,
                Name = "Test",
                Color = "Rouge",
                Vintage = 2020,
                Designation = "Domaine de Test",
                StartKeepingYear = 2022,
                EndKeepingYear = 2024,
            };

            // simulating the add method //

            var MyAddTest = await BottleTest.AddBottleAsync(MajBottle1);
            context.Bottles.Add(MajBottle1);

            var context2 = new WineManagerContext(builder.Options);
            BottleRepository BottleTest2 = new BottleRepository(context2, null);

            MajBottle.BottleId = 2;
            MajBottle.Name = "Test";
            MajBottle.Color = "Rouge";
            MajBottle.Vintage = 2020;
            MajBottle.Designation = "Domaine de Test";
            MajBottle.StartKeepingYear = 2022;
            MajBottle.EndKeepingYear = 2024;

            var MyUpdateTest = await BottleTest2.UpdateBottleAsync(MajBottle);

            Assert.AreNotSame(MyAddTest, MyUpdateTest);
        }
    }
}