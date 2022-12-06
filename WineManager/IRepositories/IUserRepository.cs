using WineManager.DTO;

namespace WineManager.IRepositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserAsync(int id);
        Task<UserDto?> AddUserAsync(UserPostDto userPostDto);
        Task<UserDto?> UpdateUserAsync(UserPutDto userPutDto);
        Task<UserDto?> DeleteUserAsync(int id);
        Task<UserDto?> GetUserWithBottlesAsync(int id);
        Task<UserDto?> GetUserWithDrawersAsync(int id);
        Task<UserDto?> GetUserWithCavesAsync(int id);


    }
}
