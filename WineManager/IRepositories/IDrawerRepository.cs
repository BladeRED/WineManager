﻿using Microsoft.Extensions.Hosting;
using WineManager.DTO;
using WineManager.Entities;

namespace WineManager.IRepositories
{
    public interface IDrawerRepository
    {

        /// <summary>
        /// Get all drawers
        /// </summary>
        /// <returns></returns>
        Task<List<Drawer>> GetDrawersAsync();

        /// <summary>
        /// Get drawer from Id user
        /// </summary>
        /// <param name="UserId">Id User</param>
        /// <returns></returns>
        Task<Drawer> GetByIdUserAsync(int UserId);

        /// <summary>
        /// Get drawer from Id drawer
        /// </summary>
        /// <param name="idDrawer">Id Drawer</param>
        /// <returns></returns>
        Task<Drawer?> GetByIdAsync(int idDrawer);

        /// <summary>
        /// Get drawer from Id Cave
        /// </summary>
        /// <param name="idCave">Id Drawer</param>
        /// <returns></returns>
        Task<Drawer?> GetByIdCaveAsync(int idCave);

        /// <summary>
        /// Get drawer from Id with his user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DrawerDtoGet> GetDrawerWithUserAsync(int id);

        /// <summary>
        /// Get drawer from Id with his bottles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DrawerDtoGet> GetDrawerWithBottlesAsync(int id);

        /// <summary>
        /// Get drawer from Id with his cave.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DrawerDtoGet> GetDrawerWithCaveAsync(int id);

        /// <summary>
        /// Add a Drawer
        /// </summary>
        /// <returns></returns>
        Task<Drawer?> AddDrawerAsync(Drawer drawer);

        /// <summary>
        /// Update a drawer
        /// </summary>
        /// <returns></returns>
        /// 
        Task<Drawer?> UpdateDrawerAsync(Drawer drawer);

        /// <summary>
        /// Delete a Drawer
        /// </summary>
        /// <returns></returns>
        Task<bool> DeleteDrawerAsync(int idDrawer);

    }
}