using WineManager.DTO;

namespace WineManager.IRepositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserAsync(int id);
        Task<UserDto?> AddUserAsync(UserDto userDto, string? password);
        Task<UserDto?> UpdateUserAsync(UserDto userDto, string? password);
        Task<UserDto?> DeleteUserAsync(int id);
        //Task<UserDto> LoginUser(string login, string pwd);
        Task<UserDto?> GetUserWithBottlesAsync(int id);
        Task<UserDto?> GetUserWithDrawersAsync(int id);
        Task<UserDto?> GetUserWithCavesAsync(int id);


    }
}
