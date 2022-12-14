using Microsoft.EntityFrameworkCore;
using WineManager.Entities;
using WineManager.IRepositories;
using WineManager.Contexts;
using Microsoft.Extensions.Hosting;
using WineManager.DTO;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace WineManager.Repositories
{
    public class DrawerRepository : IDrawerRepository
    {

        WineManagerContext WineManagerContext;
        ILogger<DrawerRepository> logger;
        public DrawerRepository(WineManagerContext WineManagerContext, ILogger<DrawerRepository> logger)
        {
            this.WineManagerContext = WineManagerContext;
            this.logger = logger;
        }

        /// <summary>
        /// Get all drawers
        /// </summary>
        /// <returns></returns>
        public async Task<List<Drawer>> GetDrawersAsync()
        {
            var drawers = await WineManagerContext.Drawers.AsNoTracking().ToListAsync();
            if (drawers == null)
            {
                logger?.LogError("Item not found");

                return null;
            }
            return drawers;
        }

        /// <summary>
        /// Get drawer from Id with his cave.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DrawerDtoGet> GetDrawerWithCaveAsync(int id)
        {
            var drawerWithCave = await WineManagerContext.Drawers.AsNoTracking().Include(p => p.Cave).Where(p => p.DrawerId == id && p.CaveId != null).Select(p => new DrawerDtoGet(p.DrawerId, new CaveDtoLight(p.Cave))).FirstOrDefaultAsync();
            if (drawerWithCave == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return drawerWithCave;
        }

        /// <summary>
        /// Get drawer from Id with his user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DrawerDtoGet> GetDrawerWithUserAsync(int id)
        {
            var drawerWithUser = await WineManagerContext.Drawers.AsNoTracking().Include(p => p.User).Where(p => p.DrawerId == id).Select(p => new DrawerDtoGet(p.DrawerId, new UserDTOLight(p.User))).FirstOrDefaultAsync();
            if (drawerWithUser == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return drawerWithUser;
        }

        /// <summary>
        /// Get drawer from Id with his bottles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DrawerDtoGet> GetDrawerWithBottlesAsync(int id)
        {
            var drawerWithBottles = await WineManagerContext.Drawers.AsNoTracking().Include(p => p.Bottles).Where(p => p.DrawerId == id).Select(p => new DrawerDtoGet(p.DrawerId, p.Bottles)).FirstOrDefaultAsync();
            if (drawerWithBottles == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return drawerWithBottles;
        }

        /// <summary>
        /// Get drawer from Id user
        /// </summary>
        /// <param name="userId">Id User</param>
        /// <returns></returns>
        public async Task<Drawer> GetByIdUserAsync(int userId)
        {
            return await WineManagerContext.Drawers.AsNoTracking().Include(p => p.UserId).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Get drawer from drawer's ID.
        /// </summary>
        /// <param name="drawerId">Drawer's ID.</param>
        /// <param name="userId">User's ID.</param>
        /// <returns></returns>
        public async Task<Drawer?> GetDrawerAsync(int drawerId, int userId)
        {
            var drawer = await WineManagerContext.Drawers.AsNoTracking().Where(d => (d.DrawerId == drawerId) && (d.UserId == userId)).FirstOrDefaultAsync();
            if (drawer == null)
            {
                logger?.LogError("Item not found. Check the drawer ID.");

                return null;
            }
            return drawer;

            // return await WineManagerContext.Drawers.FirstOrDefaultAsync(p => p.DrawerId == idDrawer);
        }

        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idCave">Id Drawer</param>
        /// <returns></returns>
        public async Task<Drawer?> GetByIdCaveAsync(int idCave)
        {
            return await WineManagerContext.Drawers.AsNoTracking().FirstOrDefaultAsync(p => p.CaveId == idCave);
        }

        /// <summary>
        /// Add a Drawer
        /// </summary>
        /// <returns></returns>
        public async Task<Drawer?> AddDrawerAsync(Drawer drawer)
        {
            try
            {
                if ((drawer.CaveId != null && drawer.Level == null) || (drawer.CaveId == null && drawer.Level != null))
                {
                    logger?.LogError("CaveId can't have a value when level doesn't and vice versa.");
                    return null;
                }
                else if (drawer.CaveId != null && drawer.Level != null)
                {
                    var cave = await WineManagerContext.Caves.AsNoTracking().Include(c => c.Drawers).Where(c => c.UserId == drawer.UserId && c.CaveId == drawer.CaveId).FirstOrDefaultAsync();
                    if (cave == null)
                    {
                        logger?.LogError("There isn't a cave belonging to you with this CaveId.");
                        return null;
                    }
                    else if (drawer.Level < 0)
                    {
                        logger?.LogError("Level must be positive.");
                        return null;
                    }
                    else if (cave.Drawers != null && cave.Drawers.Count == cave.NbMaxDrawer)
                    {
                        logger?.LogError("This drawer is already full.");
                        return null;
                    }
                    else if (cave.NbMaxDrawer < drawer.Level)
                    {
                        logger?.LogError("There isn't a cave that have such Level with the caveId you provided.");
                        return null;
                    }
                    else if (cave.NbMaxBottlePerDrawer != drawer.MaxPosition)
                    {
                        logger?.LogError("Your drawer doesn't fit into this cave.");
                        return null;
                    }
                    else if (cave.Drawers != null)
                    {
                        foreach (var d in cave.Drawers)
                        {
                            if (d.Level == drawer.Level)
                            {
                                logger?.LogError("The drawer can't fit in an occupied level.");
                                return null;
                            }
                        }
                    }
                }
                WineManagerContext.Drawers.Add(drawer);
                await WineManagerContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }
            return drawer;
        }

        /// <summary>
        /// Update a drawer
        /// </summary>
        /// <returns></returns>
        public async Task<Drawer?> UpdateDrawerAsync(Drawer drawer, int userId)
        {
            var drawerToUpdate = await WineManagerContext.Drawers.FirstOrDefaultAsync(d => d.DrawerId == drawer.DrawerId && userId == d.UserId);

            if (drawerToUpdate == null)
            {
                return null;
            }

            if (drawer.Level != null)
                drawerToUpdate.Level = drawer.Level;

            await WineManagerContext.SaveChangesAsync();

            return drawerToUpdate;
        }

        /// <summary>
        /// Range a Drawer into a Cave
        /// </summary>
        /// <param name="drawerId"></param>
        /// <param name="caveId"></param>
        /// <param name="userId"></param>
        /// <param name="caveLevel"></param>
        /// <returns></returns>
        public async Task<Drawer?> StockDrawerAsync(int drawerId, int? caveId, int userId, int? caveLevel)
        {
            var drawerToPut = await WineManagerContext.Drawers.Where(d => d.UserId == userId && d.DrawerId == drawerId).FirstOrDefaultAsync();
            if ((caveId != null && caveLevel == null) || (caveId == null && caveLevel != null))
            {
                logger?.LogError("CaveId can't have a value when level doesn't and vice versa.");
                return null;
            }
            else if (caveId != null && caveLevel != null)
            {
                if (drawerToPut == null)
                {
                    logger?.LogError("Drawer not found, check if the Drawer belong to the connected User.");

                    return null;
                }
                var getCave = await WineManagerContext.Caves.AsNoTracking().Include(c => c.Drawers).Where(c => c.CaveId == caveId & c.UserId == userId).FirstOrDefaultAsync();
                if (getCave == null)
                {
                    logger?.LogError("Cave not found, check if the Cave belong to the connected User.");
                    return null;
                }
                else if (drawerToPut.Level < 0)
                {
                    logger?.LogError("Level must be positive.");
                    return null;
                }
                else if (getCave.Drawers != null && getCave.Drawers.Count == getCave.NbMaxDrawer)
                {
                    logger?.LogError("This drawer is already full.");
                    return null;
                }
                else if (getCave.NbMaxDrawer < drawerToPut.Level)
                {
                    logger?.LogError("There isn't a cave that have such Level with the caveId you provided.");
                    return null;
                }
                else if (getCave.NbMaxBottlePerDrawer != drawerToPut.MaxPosition)
                {
                    logger?.LogError("Your drawer doesn't fit into this cave.");
                    return null;
                }
                else if (getCave.Drawers != null)
                {
                    foreach (var item in getCave.Drawers)
                    {
                        //comparer cavelevel
                        if (caveLevel == item.Level)
                        {
                            logger?.LogError("Impossible to place your drawer in a location already taken.");
                            return null;
                        }
                    }
                }
            }
            if (drawerToPut == null)
            {
                logger?.LogError("Drawer not found, check if the Drawer belong to the connected User.");

                return null;
            }
            drawerToPut.Level = caveLevel;
            drawerToPut.CaveId = caveId;
            await WineManagerContext.SaveChangesAsync();
            return drawerToPut;


        }


        /// <summary>
        /// Delete a Drawer
        /// </summary>
        /// <returns></returns>
        public async Task<Drawer> DeleteDrawerAsync(int idDrawer, int userId)
        {
            var drawerToDelete = await WineManagerContext.Drawers.Where(b => b.DrawerId == idDrawer && b.UserId == userId).Include(c => c.Bottles).FirstOrDefaultAsync();
            if (drawerToDelete == null)
            {
                logger?.LogError("Item not found. Check the drawer ID.");
                return null;
            }
            if (drawerToDelete.Bottles != null)
            {
                foreach (var bottle in drawerToDelete.Bottles)
                {
                    bottle.DrawerId = null;
                }
            }
            WineManagerContext?.Drawers.Remove(drawerToDelete);

            await WineManagerContext!.SaveChangesAsync();

            return drawerToDelete;
        }
    }
}


