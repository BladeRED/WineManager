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
            var bottles = await context.Bottles.AsNoTracking().ToListAsync();
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
            var bottle = await context.Bottles.AsNoTracking().Where(b => (b.BottleId == bottleid) && (b.UserId == userId)).FirstOrDefaultAsync();
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
            var bottleWithUser = await context.Bottles.AsNoTracking().Include(p => p.User).Where(p => p.BottleId == id).Select(p => new BottleDtoGet(p.BottleId, p.Name, new UserDTOLight(p.User))).FirstOrDefaultAsync();
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
            var bottleWithDrawer = await context.Bottles.AsNoTracking().Include(p => p.Drawer).Where(p => p.BottleId == id && p.DrawerId != null).Select(p => new BottleDtoGet(p.BottleId, p.Name, new DrawerDtoLight(p.Drawer))).FirstOrDefaultAsync();
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
        /// <param name="bottleDto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Bottle?> AddBottleAsync(BottleDto bottleDto, int userId)
        {
            try
            {
                if (bottleDto.DrawerId != null && bottleDto.DrawerPosition != null)
                {
                    var drawer = context.Drawers.Include(d => d.Bottles).Where(d => d.DrawerId == bottleDto.DrawerId).FirstOrDefault();
                    if (drawer == null)
                    {
                        logger?.LogError("This Drawer is not exist.");

                        return null;
                    }
                    else if (drawer.UserId != userId)
                    {
                        logger?.LogError("Wrong drawer owner.");

                        return null;
                    }
                    else if (drawer.Bottles != null)
                    {
                        if (drawer.Bottles.Count == drawer.MaxPosition)
                        {
                            logger?.LogError("This Drawer is already full.");

                            return null;
                        }
                        foreach (var item in drawer.Bottles)
                        {
                            if (item.DrawerPosition == bottleDto.DrawerPosition)
                            {
                                logger?.LogError("This position is occuped.");

                                return null;
                            }
                        }
                    }
                }
                else if (bottleDto.DrawerId != null || bottleDto.DrawerPosition != null)
                {
                    logger?.LogError("DrawerId can't have a value when DrawerPosition doesn't and vice versa..");

                    return null;
                }

                if (bottleDto.StartKeepingYear != null && bottleDto.EndKeepingYear != null)
                {
                    if (bottleDto.StartKeepingYear > bottleDto.EndKeepingYear)
                    {
                        logger?.LogError("StartKeepingYear must be smaller than EndKeepingYear");

                        return null;
                    }
                }
                else if (bottleDto.StartKeepingYear == null || bottleDto.EndKeepingYear == null)
                {
                    logger?.LogError("Please give both StartKeepingYear and EndKeepingYear or don't give both values.");

                    return null;
                }
                var newBottle = new Bottle(bottleDto, userId);
                context.Bottles.Add(newBottle);
                await context.SaveChangesAsync();
                return newBottle;
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }
        }

        /// <summary>
        /// Duplicate a new bottle, with a quantity for multiply the add requests.
        /// </summary>
        /// <param name="Bottles"></param>
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
        /// <param name="bottleDtoPut"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Bottle> UpdateBottleAsync(BottleDtoPut bottleDtoPut, int userId)
        {
            try
            {
                var bottleToUpdate = await context.Bottles.FirstOrDefaultAsync(b => b.BottleId == bottleDtoPut.BottleId && b.UserId == userId);
                if (bottleToUpdate != null)
                {
                    if (bottleDtoPut.StartKeepingYear != null && bottleDtoPut.EndKeepingYear != null)
                    {
                        bottleToUpdate.StartKeepingYear = (int)bottleDtoPut.StartKeepingYear;
                        bottleToUpdate.EndKeepingYear = (int)bottleDtoPut.EndKeepingYear;
                    }

                    if (bottleDtoPut.StartKeepingYear == null || bottleDtoPut.EndKeepingYear == null)
                    {
                        logger?.LogError("Please give both StartKeepingYear and EndKeepingYear or don't give both values.");

                        return null;
                    }
                    if (bottleDtoPut.StartKeepingYear > bottleDtoPut.EndKeepingYear)
                    {
                        logger?.LogError("StartKeepingYear must be smaller than EndKeepingYear.");

                        return null;
                    }

                    if (bottleDtoPut.Name != null)
                        bottleToUpdate.Name = bottleDtoPut.Name;
                    bottleToUpdate.Vintage = bottleDtoPut.Vintage;
                    if (bottleDtoPut.Color != null)
                        bottleToUpdate.Color = bottleDtoPut.Color;
                    if (bottleDtoPut.Designation != null)
                        bottleToUpdate.Designation = bottleDtoPut.Designation;

                    await context.SaveChangesAsync();
                    return bottleToUpdate;
                }
                else
                {
                    logger?.LogError("Update not correct.");
                    return null;
                }
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
                // Search for the bottle to be stored
                var bottleToStock = await context.Bottles.Where(b => b.UserId == userId && b.BottleId == bottleDtoStock.BottleId).FirstOrDefaultAsync();

                // The bottle exists for this user ?
                if (bottleToStock == null)
                {
                    logger?.LogError("Bottle not found, check if the items belong to the connected User.");

                    return null;
                }

                // If no drawer is specified, the bottle is released.
                if (bottleDtoStock.DrawerId == null)
                {
                    bottleToStock.DrawerId = null;
                    bottleToStock.DrawerPosition = null;

                    await context.SaveChangesAsync();

                    return bottleToStock;
                }

                // If a DrawerId is specified.
                if (bottleDtoStock.DrawerId != null)
                {
                    // Verification that the Drawer to store exists.
                    var drawerForStock = await context.Drawers.FirstOrDefaultAsync(d => d.DrawerId == bottleDtoStock.DrawerId);
                    if (drawerForStock == null)
                    {
                        logger?.LogError("This drawer doesn't exist");

                        return null;
                    }
                    else
                    {
                        // DrawerPosition Default
                        if (bottleDtoStock.DrawerPosition == null)
                        {
                            bottleDtoStock.DrawerPosition = "Default";
                        }

                        // If we proceed in the same Drawer we change only DrawerPosition.
                        if (bottleToStock.DrawerId == bottleDtoStock.DrawerId)
                        {
                            bottleToStock.DrawerPosition = bottleDtoStock.DrawerPosition;

                            await context.SaveChangesAsync();

                            return bottleToStock;
                        }

                        // If you proceed in another drawer, check that the drawer has a free space.
                        var bottlesCount = await context.Bottles.Where(b => b.DrawerId == bottleDtoStock.DrawerId).CountAsync();
                        if (bottlesCount == drawerForStock.MaxPosition)
                        {
                            logger?.LogError("This drawer is full");

                            return null;
                        }
                        else 
                        {
                            bottleToStock.DrawerId = bottleDtoStock.DrawerId;
                            bottleToStock.DrawerPosition = bottleDtoStock.DrawerPosition;

                            await context.SaveChangesAsync();

                            return bottleToStock;
                        }
                    }
                }
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Bottle> DeleteBottleAsync(int bottleId, int userId)
        {
            try
            {
                var bottle = await context.Bottles.FirstOrDefaultAsync(b => b.BottleId == bottleId && b.UserId == userId);
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

