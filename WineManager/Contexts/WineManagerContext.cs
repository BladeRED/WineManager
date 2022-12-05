using WineManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace WineManager.Contexts
{
    public class WineManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bottle> Bottles { get; set; }
        public DbSet<Drawer> Drawers { get; set; }
        public DbSet<Cave> Caves { get; set; }

        public WineManagerContext() { }
        public WineManagerContext(DbContextOptions<WineManagerContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Default data
            var u1 = new User { UserId = 1, Name = "Jerry Seinfeld", Email = "Jerry.Seinfeld@aol.com", BirthDate = DateTime.Now, Password = "password" };
            var u2 = new User { UserId = 2, Name = "George Costanza", Email = "George.Costanza@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "george" };
            var u3 = new User { UserId = 3, Name = "Elaine Benes", Email = "Elaine.Benes@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "jerry" };
            var u4 = new User { UserId = 4, Name = "Cosmo Kramer", Email = "Cosmo.Kramer@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "qzerty" };

            var b1 = new Bottle
            {
                BottleId = 1,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 2,
                DrawerId = 2,
                DrawerPosition = "1"
            };
            var b2 = new Bottle
            {
                BottleId = 2,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan"
            };
            var b3 = new Bottle
            {
                BottleId = 3,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan"
            };
            var b4 = new Bottle
            {
                BottleId = 4,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling"
            };
            var b5 = new Bottle
            {
                BottleId = 5,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling"
            };
            var b6 = new Bottle
            {
                BottleId = 6,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling"
            };

            var d1 = new Drawer
            {
                DrawerId = 1,
                Level = 1,
                MaxPosition = 10,
                UserId = 2,
                CaveId = 2

            };
            var d2 = new Drawer
            {
                DrawerId = 2,
                Level = 2,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1
            };

            var c1 = new Cave
            {
                CaveId = 1,
                CaveType = "Batman",
                Family = "Wayne",
                Brand = "Acme",
                Temperature = 12,
                UserId = 1
            };
            var c2 = new Cave
            {
                CaveId = 2,
                CaveType = "Batman",
                Family = "Wayne",
                Brand = "Acme",
                Temperature = 12,
                UserId = 2
            };



            base.OnModelCreating(modelBuilder);

        }
    }
}
