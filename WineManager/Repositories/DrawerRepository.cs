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
        /// <returns></returns
        public async Task<List<Drawer>> GetDrawersAsync()
        {
            var drawers = await WineManagerContext.Drawers.ToListAsync();
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
            var drawerWithCave = await WineManagerContext.Drawers.Include(p => p.Cave).Where(p => p.DrawerId == id).Select(p => new DrawerDtoGet(p.DrawerId, new CaveDtoLight(p.Cave))).FirstOrDefaultAsync();
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
            var drawerWithUser = await WineManagerContext.Drawers.Include(p => p.User).Where(p => p.DrawerId == id).Select(p => new DrawerDtoGet(p.DrawerId, new UserDTOLight(p.User))).FirstOrDefaultAsync();
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
            var drawerWithBottles = await WineManagerContext.Drawers.Include(p => p.Bottles).Where(p => p.DrawerId == id).Select(p => new DrawerDtoGet(p.DrawerId, p.Bottles)).FirstOrDefaultAsync();
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
            return await WineManagerContext.Drawers.Include(p => p.UserId).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idDrawer">Id Drawer</param>
        /// <returns></returns>
        public async Task<Drawer?> GetByIdAsync(int idDrawer)
        {
            return await WineManagerContext.Drawers.FirstOrDefaultAsync(p => p.DrawerId == idDrawer);
        }

        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idCave">Id Drawer</param>
        /// <returns></returns>
        public async Task<Drawer?> GetByIdCaveAsync(int idCave)
        {
            return await WineManagerContext.Drawers.FirstOrDefaultAsync(p => p.CaveId == idCave);
        }

        /// <summary>
        /// Add a Drawer
        /// </summary>
        /// <returns></returns>
        public async Task<Drawer?> AddDrawerAsync(Drawer drawer)
        {
            try
            {
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
        public async Task<Drawer?> UpdateDrawerAsync(Drawer drawer)
        {
            var drawerToUpdate = await GetByIdAsync(drawer.DrawerId);

            if (drawerToUpdate == null) return null;

            drawerToUpdate.Level = drawer.Level;
            drawerToUpdate.MaxPosition = drawer.MaxPosition;

            await WineManagerContext.SaveChangesAsync();

            return drawerToUpdate;
        }

        /// <summary>
        /// Range a Drawer into a Cave
        /// </summary>
        /// <param name="drawerId"></param>
        /// <param name="caveId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Drawer?> StockDrawerAsync(int drawerId, int caveId, int userId)
        {
            var drawerToPut = await WineManagerContext.Drawers.Where(d => d.UserId == userId && d.DrawerId == drawerId).FirstOrDefaultAsync();

            if (drawerToPut == null || !(drawerToPut.CaveId == caveId))
            {
                logger?.LogError("Drawer not found, check if the Drawer and the Cave belong to the connected User.");

                return null;
            }

            drawerToPut.CaveId = caveId;

            await WineManagerContext.SaveChangesAsync();

            return drawerToPut;
        }


        /// <summary>
        /// Delete a Drawer
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DeleteDrawerAsync(int idDrawer)
        {
            var drawerToDelete = await GetByIdAsync(idDrawer);

            if (drawerToDelete == null) return false;

            WineManagerContext?.Drawers.Remove(drawerToDelete);


            WineManagerContext?.Drawers.Remove(drawerToDelete);

            await WineManagerContext.SaveChangesAsync();

            return true;
        }
    }
}


