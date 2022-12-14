using Microsoft.Extensions.Hosting;
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
        /// Get drawer from drawer's ID.
        /// </summary>
        /// <param name="drawerId">Drawer's ID.</param>
        /// <param name="userId">User's ID.</param>
        /// <returns></returns>
        Task<Drawer?> GetDrawerAsync(int drawerId, int userId);

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
        Task<Drawer?> UpdateDrawerAsync(Drawer drawer, int userId);

        /// <summary>
        /// Range a Drawer into a Cave
        /// </summary>
        /// <param name="drawerId"></param>
        /// <param name="caveId"></param>
        /// <param name="userId"></param>
        /// <param name="caveLevel"></param>
        /// <returns></returns>
        Task<Drawer?> StockDrawerAsync(int drawerId, int? caveId, int userId, int? caveLevel);

        /// <summary>
        /// Delete a Drawer
        /// </summary>
        /// <returns></returns>
        Task<Drawer> DeleteDrawerAsync(int idDrawer, int userId);

    }
}
