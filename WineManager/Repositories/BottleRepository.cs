using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WineManager.Contexts;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;

namespace WineManager.Repositories
{
    public class BottleRepository : IBottleRepository
    {
        public WineManagerContext context;
        ILogger<BottleRepository> logger;
        public BottleRepository(WineManagerContext context, ILogger<BottleRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Get all bottles form DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<Bottle>> GetAllBottlesAsync()
        {
            var bottles = await context.Bottles.ToListAsync();
            if (bottles == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return bottles;
        }

        /// <summary>
        /// Get bottle from Id.
        /// </summary>
        /// <param name="bottleid">Id bottle</param>
        /// <param name="userId">CurrentUser's ID.</param>
        /// <returns></returns>
        public async Task<Bottle> GetBottleAsync(int bottleid, int userId)
        {
            var bottle = await context.Bottles.Where(b => (b.BottleId == bottleid) && (b.UserId == userId)).FirstOrDefaultAsync();
            if (bottle == null)
            {
                logger?.LogError("Item not found. Check the bottle ID.");

                return null;
            }
            return bottle;
        }


        /// <summary>
        /// Get bottle from Id with his user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BottleDtoGet> GetBottleWithUserAsync(int id)
        {
            var bottleWithUser = await context.Bottles.Include(p => p.User).Where(p => p.BottleId == id).Select(p => new BottleDtoGet(p.BottleId, p.Name, new UserDTOLight(p.User))).FirstOrDefaultAsync();
            if (bottleWithUser == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return bottleWithUser;
        }

        /// <summary>
        /// Get bottle from Id with his drawer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BottleDtoGet> GetBottleWithDrawerAsync(int id)
        {
            var bottleWithDrawer = await context.Bottles.Include(p => p.Drawer).Where(p => p.BottleId == id).Select(p => new BottleDtoGet(p.BottleId, p.Name, new DrawerDtoLight(p.Drawer))).FirstOrDefaultAsync();
            if (bottleWithDrawer == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return bottleWithDrawer;
        }

        /// <summary>
        /// Add a new bottle.
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        public async Task<Bottle?> AddBottleAsync(Bottle bottle)
        {
            try
            {
                context.Bottles.Add(bottle);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }
            return bottle;
        }

        /// <summary>
        /// Duplicate a new bottle, with a quantity for multiply the add requests.
        /// </summary>
        /// <param name="bottle"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task<List<Bottle>> DuplicateBottleAsync(List<Bottle> Bottles, int quantity)
        {
            try
            {
                foreach (Bottle bottle in Bottles)
                {
                    context.Bottles.Add(bottle);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());
                return null;
            }

            return Bottles;
        }

        /// <summary>
        /// Update bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bottle"></param>
        /// <returns></returns>
        public async Task<Bottle> UpdateBottleAsync(BottleDtoPut bottleDtoPut)
        {
            try
            {
                var bottleToUpdate = await context.Bottles.FirstOrDefaultAsync(b => b.BottleId == bottleDtoPut.BottleId);

                if (bottleDtoPut.Name != null)
                    bottleToUpdate.Name = bottleDtoPut.Name;
                if (bottleDtoPut.Vintage != null)
                    bottleToUpdate.Vintage = bottleDtoPut.Vintage;
                if (bottleDtoPut.StartKeepingYear != null)
                    bottleToUpdate.StartKeepingYear = bottleDtoPut.StartKeepingYear;
                if (bottleDtoPut.EndKeepingYear != null)
                    bottleToUpdate.EndKeepingYear = bottleDtoPut.EndKeepingYear;
                if (bottleDtoPut.Color != null)
                    bottleToUpdate.Color = bottleDtoPut.Color;


                await context.SaveChangesAsync();
                return bottleToUpdate;
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }

        }

        public async Task<Bottle> StockBottleAsync(BottleDtoStock bottleDtoStock, int userId)
        {
            try
            {
                var bottleToStock = await context.Bottles.Where(b => b.UserId == userId && b.BottleId == bottleDtoStock.BottleId).FirstOrDefaultAsync();

                if (bottleToStock == null)
                {
                    logger?.LogError("Bottle not found, check if the items belong to the connected User.");

                    return null;
                }
                if (bottleDtoStock.DrawerId == null)
                {
                    bottleToStock.DrawerId = null;
                    bottleToStock.DrawerPosition = null;
                    await context.SaveChangesAsync();


                    return bottleToStock;
                }
                bottleToStock.DrawerId = bottleDtoStock.DrawerId;
                bottleToStock.DrawerPosition = bottleDtoStock.DrawerPosition;

                var drawerToStock = await context.Drawers.FirstOrDefaultAsync(d => d.DrawerId == bottleDtoStock.DrawerId);
                drawerToStock.CaveId = bottleDtoStock.CaveId;
                drawerToStock.Level = bottleDtoStock.CaveLevel;


                await context.SaveChangesAsync();
                return bottleToStock;
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }
        }

        /// <summary>
        /// Delete bottle from Id.
        /// </summary>
        /// <param name="bottleId"></param>
        /// <returns></returns>
        public async Task<Bottle> DeleteBottleAsync(int bottleId)
        {
            try
            {
                var bottle = await context.Bottles.FindAsync(bottleId);
                if (bottle == null)
                {
                    logger?.LogError("Item not found. Check the bottle ID.");

                    return null;
                }
                context.Bottles.Remove(bottle);

                await context.SaveChangesAsync();
                return bottle;
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }

        }

    }
}

