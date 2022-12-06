using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineManager.Contexts;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;

namespace WineManager.Repositories
{
    public class BottleRepository : IBottleRepository
    {
        WineManagerContext context;
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
            return await context.Bottles.ToListAsync();
        }

        /// <summary>
        /// Get bottle from Id.
        /// </summary>
        /// <param name="id">Id bottle</param>
        /// <returns></returns>
        public async Task<Bottle> GetBottleAsync(int id)
        {
            return await context.Bottles.FindAsync(id);
        }

        /// <summary>
        /// Get bottle from Id with his user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BottleDtoGet> GetBottleWithUserAsync(int id)
        {
            var bottleWithUser = await context.Bottles.Include(p => p.User).Where(p => p.BottleId == id).Select(p => new BottleDtoGet(p.BottleId, p.Name, new UserDTOLight(p.User))).FirstOrDefaultAsync();

            return bottleWithUser;
        }

        /// <summary>
        /// Get bottle from Id with his drawer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BottleDtoGet> GetBottleWithDrawerAsync(int id)
        {
            var bottleWithDrawer = await context.Bottles.Include(p => p.Drawer).Where(p => p.BottleId == id).Select(p => new BottleDtoGet(p.BottleId, p.Name, new DrawerDTOLight(p.Drawer))).FirstOrDefaultAsync();
            return bottleWithDrawer;
        }

        /// <summary>
        /// Add a new bottle.
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        public async Task<Bottle> AddBottleAsync(Bottle bottle)
        {
            try
            {
                context.Bottles.Add(bottle);

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }

            return bottle;
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
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }

        }

        /// <summary>
        /// Delete bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Bottle> DeleteBottleAsync(int id)
        {
            Bottle bottle;
            try
            {
                bottle = await GetBottleAsync(id);
                context.Bottles.Remove(bottle);

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }
            return bottle;
        }
    }
}

