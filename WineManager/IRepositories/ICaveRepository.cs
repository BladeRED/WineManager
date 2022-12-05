using Microsoft.Extensions.Hosting;
using WineManager.Entities;

namespace WineManager.IRepositories
{
    public interface ICaveRepository
    {

        /// <summary>
        /// Get all caves
        /// </summary>
        /// <returns></returns>
        Task<List<Cave>> GetCavesAsync();

        /// <summary>
        /// Get cave from Id user
        /// </summary>
        /// <param name="UserId">Id User</param>
        /// <returns></returns>
        Task<Cave> GetByIdUserAsync(int UserId);

        /// <summary>
        /// Get drawer From id cave
        /// </summary>
        /// <param name="CaveId">Id Drawer</param>
        /// <returns></returns>
        Task<Cave> GetWithDrawerAsync(int CaveId);

        /// <summary>
        /// Get cave from Id drawer
        /// </summary>
        /// <param name="UserId">Id User</param>
        /// <returns></returns>
        Task<Cave> GetWithUserAsync(int CaveId);

        /// <summary>
        /// Get cave from Id cave
        /// </summary>
        /// <param name="idCave">Id Cave</param>
        /// <returns></returns>
        /// 
        Task<Cave?> GetByIdAsync(int idCave);

        /// <summary>
        /// Add a Cave
        /// </summary>
        /// <returns></returns>
        /// 
        Task<Cave?> AddCaveAsync(Cave cave);

        /// <summary>
        /// Update a cave
        /// </summary>
        /// <returns></returns>
        /// 
        Task<Cave?> UpdateCaveAsync(Cave cave);

        /// <summary>
        /// Delete a Cave
        /// </summary>
        /// <returns></returns>
        /// 
        Task<bool> DeleteCaveAsync(int idCave);

    }
}
