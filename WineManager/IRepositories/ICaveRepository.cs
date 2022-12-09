using Microsoft.Extensions.Hosting;
using WineManager.DTO;
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
        Task<CaveDtoGet> GetWithDrawerAsync(int CaveId);

        /// <summary>
        /// Get cave from Id drawer
        /// </summary>
        /// <param name="UserId">Id User</param>
        /// <returns></returns>
        Task<CaveDtoGet> GetWithUserAsync(int CaveId);

        /// <summary>
        /// Get cave from Id cave
        /// </summary>
        /// <param name="caveId">Cave's Id</param>
        /// <param name="userId">User's Id</param>
        /// <returns></returns>
        Task<Cave?> GetCaveAsync(int caveId, int userId);

        /// <summary>
        /// Add a Cave
        /// </summary>
        /// <returns></returns>
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
        Task<Cave> DeleteCaveAsync(int idCave, int userId);

    }
}
