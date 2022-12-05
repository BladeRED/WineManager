using Microsoft.EntityFrameworkCore;
using WineManager.Contexts;
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
        public async Task<Bottle> GetBottleWithUserAsync(int id)
        {
            return await context.Bottles.Include(p => p.User).FirstOrDefaultAsync(p => p.BottleId == id);
        }

        /// <summary>
        /// Get bottle from Id with his drawer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Bottle> GetBottleWithDrawerAsync(int id)
        {
            return await context.Bottles.Include(p => p.Drawer).FirstOrDefaultAsync(p => p.BottleId == id);
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
        public async Task<Bottle> UpdateBottleAsync(int id, Bottle bottle)
        {
            try
            {
                context.Bottles.Update(bottle);

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

