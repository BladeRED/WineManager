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
            // Users
            var u1 = new User { UserId = 1, Name = "Jerry Seinfeld", Email = "jerry@aol.com", BirthDate = DateTime.Now, Password = "pwd" };
            var u2 = new User { UserId = 2, Name = "George Costanza", Email = "George.Costanza@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "george" };
            var u3 = new User { UserId = 3, Name = "Elaine Benes", Email = "Elaine.Benes@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "jerry" };
            var u4 = new User { UserId = 4, Name = "Cosmo Kramer", Email = "Cosmo.Kramer@aol.com", BirthDate = new DateTime(10, 10, 10), Password = "qzerty" };

            // Bottles
            var b1 = new Bottle
            {
                BottleId = 1,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 1,
                DrawerPosition = "A1"
            };
            var b2 = new Bottle
            {
                BottleId = 2,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 1,
                DrawerPosition = "A2"
            };
            var b3 = new Bottle
            {
                BottleId = 3,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 1,
                DrawerPosition = "A3"
            };
            var b4 = new Bottle
            {
                BottleId = 4,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 1,
                DrawerId = 1,
                DrawerPosition = "A4"
            };
            var b5 = new Bottle
            {
                BottleId = 5,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B1"
            };
            var b6 = new Bottle
            {
                BottleId = 6,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B2"
            };
            var b7 = new Bottle
            {
                BottleId = 7,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B3"
            };
            var b8 = new Bottle
            {
                BottleId = 8,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B4"
            };
            var b9 = new Bottle
            {
                BottleId = 9,
                Name = "Chateau Pape Clement",
                Vintage = 2007,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "red",
                Designation = "Pessac-Leognan",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B5"
            };
            var b10 = new Bottle
            {
                BottleId = 10,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 1,
                DrawerId = 2,
                DrawerPosition = "B6"
            };
            var b11 = new Bottle
            {
                BottleId = 11,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 2
            };
            var b12 = new Bottle
            {
                BottleId = 12,
                Name = "Krick Vin D'Alsace",
                Vintage = 2017,
                StartKeepingYear = 5,
                EndKeepingYear = 8,
                Color = "White",
                Designation = "Riesling",
                UserId = 3
            };

            // Drawers
            var d1 = new Drawer
            {
                DrawerId = 1,
                Level = 1,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1

            };
            var d2 = new Drawer
            {
                DrawerId = 2,
                Level = 2,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1
            };
            var d3 = new Drawer
            {
                DrawerId = 3,
                Level = 3,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1

            };
            var d4 = new Drawer
            {
                DrawerId = 4,
                Level = 4,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1
            };
            var d5 = new Drawer
            {
                DrawerId = 5,
                Level = 5,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1

            };
            var d6 = new Drawer
            {
                DrawerId = 6,
                Level = 6,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 1
            };
            var d7 = new Drawer
            {
                DrawerId = 7,
                Level = 1,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2

            };
            var d8 = new Drawer
            {
                DrawerId = 8,
                Level = 2,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2
            };
            var d9 = new Drawer
            {
                DrawerId = 9,
                Level = 3,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2

            };
            var d10 = new Drawer
            {
                DrawerId = 10,
                Level = 4,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2
            };
            var d11 = new Drawer
            {
                DrawerId = 11,
                Level = 5,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2

            };
            var d12 = new Drawer
            {
                DrawerId = 12,
                Level = 6,
                MaxPosition = 6,
                UserId = 1,
                CaveId = 2
            };

            // Caves
            var c1 = new Cave
            {
                CaveId = 1,
                CaveType = "Cellar of the kitchen",
                Family = "Service cellar",
                Brand = "Liebherr",
                Temperature = 16,
                NbMaxDrawer = 6,
                NbMaxBottlePerDrawer = 6,
                UserId = 1
            };
            var c2 = new Cave
            {
                CaveId = 2,
                CaveType = "Garage cellar",
                Family = "Cellar of guard",
                Brand = "La Sommelière",
                Temperature = 14,
                NbMaxDrawer = 6,
                NbMaxBottlePerDrawer = 6,
                UserId = 1
            };
            modelBuilder.Entity<User>().HasData(new List<User> { u1, u2, u3, u4 });
            modelBuilder.Entity<Bottle>().HasData(new List<Bottle> { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12 });
            modelBuilder.Entity<Drawer>().HasData(new List<Drawer> { d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12 });
            modelBuilder.Entity<Cave>().HasData(new List<Cave> { c1, c2 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
