using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WineManager.Contexts;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;

namespace WineManager.Repositories
{
    public class UserRepository : IUserRepository
    {
        WineManagerContext context;
        ILogger<UserRepository> logger;

        public UserRepository(WineManagerContext context, ILogger<UserRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        private UserDto convertUserToDto(User user)
        {
            var userDto = new UserDto(user);

            return userDto;
        }
        private User convertUserDtoToUser(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                Drawers = userDto.Drawers
            };

            return user;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<User> users = await context.Users.ToListAsync();
            List<UserDto> userDtoList = new List<UserDto>();
            foreach (var user in users)
            {
                userDtoList.Add(convertUserToDto(user));
            }
            return userDtoList;
        }

        public async Task<UserDto?> GetUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                return null;
            else
            {
                var userDto = convertUserToDto(user);
                return userDto;
            }
        }

        public async Task<UserDto?> AddUserAsync(UserDto userDto, string? password)
        {
            try
            {
                var user = convertUserDtoToUser(userDto);
                if (password != null)
                    user.Password = password;
                await context.Users.AddAsync(user);

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }
            return userDto;
        }


        public async Task<UserDto?> UpdateUserAsync(UserDto userDto, string password)
        {
            try
            {

                User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
                if (user != null)
                {
                    user.Name = userDto.Name;
                    user.Email = userDto.Email;
                    user.BirthDate = userDto.BirthDate;
                    user.Password = password;

                    await context.SaveChangesAsync();
                    return userDto;
                }
                else
                {
                    logger.LogError("Item not found");

                    return null;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }
            return null;
        }

        public async Task<UserDto?> DeleteUserAsync(int id)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(p => p.UserId == id);
                UserDto? userDto = convertUserToDto(user);
                if (user != null)
                {
                    context.Users.Remove(user);

                    await context.SaveChangesAsync();
                    return userDto;
                }
                else
                {
                    logger.LogError("Item not found");

                    return null;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }
        }

        public async Task<UserDto?> GetUserWithBottlesAsync(int id)
        {
            var users = await context.Users.Include(u => u.Bottles).FirstOrDefaultAsync(b => b.UserId == id);
            var userDtoList = convertUserToDto(users);
            return userDtoList;
        }

        public async Task<UserDto?> GetUserWithDrawersAsync(int id)
        {
            var users = await context.Users.Include(u => u.Drawers).FirstOrDefaultAsync(d => d.UserId == id);
            var userDtoList = convertUserToDto(users);
            return userDtoList;
        }

        public async Task<UserDto?> GetUserWithCavesAsync(int id)
        {
            var users = await context.Users.Include(u => u.Caves).FirstOrDefaultAsync(c => c.UserId == id);
            var userDtoList = convertUserToDto(users);
            return userDtoList;
        }
    }
}
