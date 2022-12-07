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
        [TestMethod()]
        public async Task AddCaveAsyncTest()
        {
            // creation of the temp database and its context //

            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            CaveRepository CaveTest = new CaveRepository(context,null);

            // creation of the object to add //

            Cave TestCave = new Cave()
            {
                CaveId = 1,
                CaveType = "Cave de test",
                Brand = "Lambda",
                Family = "Random",
                Temperature = 12,
            };

            // simulating the add method //

            var MyAddTest = await CaveTest.AddCaveAsync(TestCave);
            context.Caves.Add(TestCave);

            var MyList = await CaveTest.GetCavesAsync();

            // comparing the list of objects to see if there is a new entry in the database //

            Assert.AreEqual(1, MyList.Count);

            context.Database.EnsureDeleted();

        }

        [TestMethod()]
        public async Task UpdateCaveAsyncTest()
        {
            // creation of the temp database and its context //

            var builder = new DbContextOptionsBuilder<WineManagerContext>().UseInMemoryDatabase("WineManagerTest");
            var context = new WineManagerContext(builder.Options);
            CaveRepository CaveTest = new CaveRepository(context, null);

            Cave MajCave = new Cave()
            {
                CaveId = 2,
                CaveType = "Cave de test",
                Brand = "Lambda",
                Family = "Random",
                Temperature = 12,
            };

            // simulating the add method //

            var MyAddTest = await CaveTest.AddCaveAsync(MajCave);
            context.Caves.Add(MajCave);

            var context2 = new WineManagerContext(builder.Options);
            CaveRepository CaveTest2 = new CaveRepository(context2, null);

            MajCave.CaveType = "Cave de test 2";
            MajCave.Brand = "Lambda2";
            MajCave.Family = "Random2";
            MajCave.Temperature = 14;

            var MyUpdateTest= await CaveTest2.UpdateCaveAsync(MajCave);

            Assert.AreNotSame(MyAddTest, MyUpdateTest);
        }
    }
}