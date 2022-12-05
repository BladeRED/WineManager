﻿using Microsoft.EntityFrameworkCore;
using WineManager.Entities;
using WineManager.IRepositories;
using WineManager.Contexts;
using Microsoft.Extensions.Hosting;

namespace WineManager.Repositories
{
    public class CaveRepository : ICaveRepository
    {

        readonly WineManagerContext WineManagerContext;
        public CaveRepository(WineManagerContext WineManagerContext)
        {
            this.WineManagerContext = WineManagerContext;
        }
        /// <summary>
        /// Get all caves
        /// </summary>
        /// <returns></returns>
        
        public async Task<List<Cave>> GetCavesAsync()
        {
            return await WineManagerContext.Caves.ToListAsync();
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
        /// Get cave from Id drawer
        /// </summary>
        /// <param name="drawerId">Id User</param>
        /// <returns></returns>

        public async Task<Cave> GetByIdDrawerAsync(int drawerId)
        {
            return await WineManagerContext.Caves.Include(p => p.DrawerId).FirstOrDefaultAsync(p => p.UserId == drawerId);
        }

        /// <summary>
        /// Get cave from Id user
        /// </summary>
        /// <param name="userId">Id User</param>
        /// <returns></returns>

        public async Task<Cave> GetWithUserAsync(int caveId)
        {
            return await WineManagerContext.Caves.Include(p => p.UserId).FirstOrDefaultAsync(p => p.CaveId == caveId);
        }
        /// <summary>
        /// Get cave from Id cave
        /// </summary>
        /// <param name="idCave">Id Cave</param>
        /// <returns></returns>
        /// 
        public async Task<Cave?> GetByIdAsync(int idCave)
        {
            return await WineManagerContext.Caves.FirstOrDefaultAsync(p => p.CaveId == idCave);
        }
        /// <summary>
        /// Add a Cave
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<Cave?> AddCaveAsync(Cave cave)
        {

            WineManagerContext.Caves.Add(cave);

            await WineManagerContext.SaveChangesAsync();

            return cave;
        }
        /// <summary>
        /// Update a cave
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<Cave?> UpdateCaveAsync(Cave cave)
        {
            var caveToUpdate = await GetByIdAsync(cave.CaveId);

            if (caveToUpdate == null) return null;


            caveToUpdate.CaveType = cave.CaveType;
            caveToUpdate.Family = cave.Family;
            caveToUpdate.Brand = cave.Brand;
            caveToUpdate.Temperature = cave.Temperature;

            await WineManagerContext?.SaveChangesAsync();

            return caveToUpdate;
        }
        /// <summary>
        /// Delete a Cave
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<bool> DeleteCaveAsync(int idCave)
        {
            var caveToDelete = await GetByIdAsync(idCave);

            if (caveToDelete == null) return false;

            WineManagerContext?.Caves.Remove(caveToDelete);


            WineManagerContext?.Caves.Remove(caveToDelete);

            await WineManagerContext?.SaveChangesAsync();

            return true;
        }
    }
}

