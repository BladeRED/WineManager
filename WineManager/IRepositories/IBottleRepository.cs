using WineManager.DTO;
using WineManager.Entities;

namespace WineManager.IRepositories
{
    public interface IBottleRepository
    {
        /// <summary>
        /// Get all bottles form DB
        /// </summary>
        /// <returns></returns>
        Task<List<Bottle>> GetAllBottlesAsync();

        /// <summary>
        /// Get bottle from Id.
        /// </summary>
        /// <param name="id">Id bottle</param>
        /// <returns></returns>
        Task<Bottle> GetBottleAsync(int id);

        /// <summary>
        /// Get bottle from Id with his user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BottleDtoGet> GetBottleWithUserAsync(int id);

        /// <summary>
        /// Get bottle from Id with his drawer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BottleDtoGet> GetBottleWithDrawerAsync(int id);

        /// <summary>
        /// Add a new bottle.
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        Task<Bottle> AddBottleAsync(Bottle bottle);

        /// <summary>
        /// Duplicate a new bottle, with a quantity for multiply the add requests.
        /// </summary>
        /// <param name="bottleDupl"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task<List<Bottle>> DuplicateBottleAsync(List<Bottle> Bottles, int quantity);

        /// <summary>
        /// Update bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bottle"></param>
        /// <returns></returns>
        Task<Bottle> UpdateBottleAsync(BottleDtoPut bottlePutDto);

        /// <summary>
        /// Delete bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Bottle> DeleteBottleAsync(int id);
    }
}

