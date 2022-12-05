﻿using Microsoft.EntityFrameworkCore;
using WineManager.Entities;
using WineManager.IRepositories;
using WineManager.Contexts;
using Microsoft.Extensions.Hosting;

namespace WineManager.Repositories
{
    public class DrawerRepository : IDrawerRepository
    {

        readonly WineManagerContext WineManagerContext;
        public DrawerRepository(WineManagerContext WineManagerContext)
        {
            this.WineManagerContext = WineManagerContext;
        }
        /// <summary>
        /// Get all drawers
        /// </summary>
        /// <returns></returns>

        public async Task<List<Drawer>> GetDrawersAsync()
        {
            return await WineManagerContext.Drawers.ToListAsync();
        }
        /// <summary>
        /// Get drawer from Id user
        /// </summary>
        /// <param name="userId">Id User</param>
        /// <returns></returns>

        public async Task<Drawer> GetByIdUserAsync(int userId)
        {
            return await WineManagerContext.Drawers.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);
        }
        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idDrawer">Id Drawer</param>
        /// <returns></returns>
        /// 
        public async Task<Drawer?> GetByIdAsync(int idDrawer)
        {
            return await WineManagerContext.Drawers.FirstOrDefaultAsync(p => p.DrawerId == idDrawer);
        }

        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idCave">Id Drawer</param>
        /// <returns></returns>
        /// 
        public async Task<Drawer?> GetByIdCaveAsync(int idCave)
        {
            return await WineManagerContext.Drawers.FirstOrDefaultAsync(p => p.CaveId == idCave);
        }
        /// <summary>
        /// Add a Drawer
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<Drawer?> AddDrawerAsync(Drawer drawer)
        {

            WineManagerContext.Drawers.Add(drawer);

            await WineManagerContext.SaveChangesAsync();

            return drawer;
        }
        /// <summary>
        /// Update a drawer
        /// </summary>
        /// <returns></returns>
        /// 
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
        /// Delete a Drawer
        /// </summary>
        /// <returns></returns>
        /// 
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


