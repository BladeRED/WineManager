using Microsoft.EntityFrameworkCore;
using WineManager.Entities;
using WineManager.IRepositories;
using WineManager.Contexts;
using Microsoft.Extensions.Hosting;
using WineManager.DTO;
using Microsoft.Extensions.Logging;

namespace WineManager.Repositories
{
    public class CaveRepository : ICaveRepository
    {

        readonly WineManagerContext WineManagerContext;
        ILogger<DrawerRepository> logger;
        public CaveRepository(WineManagerContext WineManagerContext, ILogger<DrawerRepository> logger)
        {
            this.WineManagerContext = WineManagerContext;
            this.logger = logger;
        }

        /// <summary>
        /// Get all caves
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cave>> GetCavesAsync()
        {
            var cave = await WineManagerContext.Caves.ToListAsync();
            if (cave == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return cave;
        }

        /// <summary>
        /// Get cave from Id user
        /// </summary>
        /// <param name="userId">Id User</param>
        /// <returns></returns>
        public async Task<Cave> GetByIdUserAsync(int userId)
        {
            return await WineManagerContext.Caves.Include(p => p.UserId).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        /// <summary>
        /// Get Drawer from Id Cave
        /// </summary>
        /// <param name="caveId">Id Drawer</param>
        /// <returns></returns>
        public async Task<CaveDtoGet> GetWithDrawerAsync(int caveId)
        {
            var cave = await WineManagerContext.Caves.Include(p => p.Drawers).Where(p => p.CaveId == caveId).Select(p => new CaveDtoGet(p.CaveId, p.Drawers)).FirstOrDefaultAsync();
            if (cave == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return cave;
        }

        /// <summary>
        /// Get cave from Id user
        /// </summary>
        /// <param name="userId">Id User</param>
        /// <returns></returns>
        public async Task<CaveDtoGet> GetWithUserAsync(int caveId)
        {
            var cave = await WineManagerContext.Caves.Include(p => p.UserId).Where(p => p.CaveId == caveId).Select(p => new CaveDtoGet(p.CaveId, new UserDTOLight(p.User))).FirstOrDefaultAsync();
            if (cave == null)
            {
                logger?.LogError("Item not found");
                return null;
            }
            return cave;
        }

        /// <summary>
        /// Get cave from Id cave
        /// </summary>
        /// <param name="caveId">Cave's Id</param>
        /// <param name="userId">User's Id</param>
        /// <returns></returns>
        public async Task<Cave?> GetCaveAsync(int caveId, int userId)
        {
            var cave = await WineManagerContext.Caves.Where(c => (c.CaveId == caveId) && (c.UserId == userId)).FirstOrDefaultAsync();
            if (cave == null)
            {
                logger?.LogError("Item not found. Check the Cave ID.");

                return null;
            }
            return cave;
        }

        /// <summary>
        /// Add a Cave
        /// </summary>
        /// <returns></returns>
        public async Task<Cave?> AddCaveAsync(Cave cave)
        {
            try
            {
                WineManagerContext.Caves.Add(cave);
                await WineManagerContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger?.LogError(e?.InnerException?.ToString());
                return null;
            }
            return cave;
        }

        /// <summary>
        /// Update a cave
        /// </summary>
        /// <returns></returns>
        public async Task<Cave?> UpdateCaveAsync(Cave cave, int userId)
        {
            var caveToUpdate = await WineManagerContext.Caves.FirstOrDefaultAsync(c => c.CaveId == cave.CaveId && userId == c.UserId);

            if (caveToUpdate == null)
            {
                return null;
            }

            if (cave.CaveType != null)
                caveToUpdate.CaveType = cave.CaveType;
            if (cave.Family != null)
                caveToUpdate.Family = cave.Family;
            if (cave.Brand != null)
                caveToUpdate.Brand = cave.Brand;
            if (cave.Temperature != null)
                caveToUpdate.Temperature = cave.Temperature;

            await WineManagerContext?.SaveChangesAsync();

            return caveToUpdate;
        }

        /// <summary>
        /// Delete a Cave
        /// </summary>
        /// <returns></returns>
        public async Task<Cave> DeleteCaveAsync(int idCave, int userId)
        {
            var caveToDelete = await WineManagerContext.Caves.Where(b => b.CaveId == idCave && b.UserId == userId).Include(c => c.Drawers).FirstOrDefaultAsync();
            if (caveToDelete == null)
            {
                logger?.LogError("Item not found. Check the cave ID.");
                return null;
            }
            foreach (var drawer in caveToDelete.Drawers)
            {
                drawer.CaveId = null;
            }

            WineManagerContext?.Caves.Remove(caveToDelete);

            await WineManagerContext?.SaveChangesAsync();

            return caveToDelete;
        }
    }
}


