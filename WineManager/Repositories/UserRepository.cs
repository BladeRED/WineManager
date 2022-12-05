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
            var userDto = new UserDto
            {
                Name = user.Name,
                BirthDate = user.BirthDate,
                Email = user.Email
            };

            return userDto;
        }
        private User convertUserDtoToUser(UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email
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
                User? user = await context.Users.FindAsync(id);
                if (user != null)
                {
                    user.FirstName = param.FirstName;
                    user.LastName = param.LastName;
                    user.Email = param.Email;
                    user.Password = param.Password;

                    await context.SaveChangesAsync();
                    return user;
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
    }
}
