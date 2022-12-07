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

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<User> users = await context.Users.ToListAsync();
            if (users == null)
            {
                logger.LogError("Item not found");
                return null;
            }
            List<UserDto> userDtoList = new List<UserDto>();
            foreach (var user in users)
            {
                userDtoList.Add(new UserDto(user));
            }
            return userDtoList;
        }

        /// <summary>
        /// Get user from Id 
        /// </summary>
        /// <param name="id">Id user</param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                logger.LogError("Item not found");
                return null;
            }
            else
            {
                var userDto = new UserDto(user);
                return userDto;
            }
        }

        /// <summary>
        /// Add a User
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto?> AddUserAsync(UserPostDto userPostDto)
        {
            try
            {
                var isEmailExist = await context.Users.AnyAsync(u => u.Email == userPostDto.Email);
                if (isEmailExist)
                {
                    logger.LogError("The email already exists.");

                    return null;
                }

                var user = new User(userPostDto);

                context.Users.Add(user);

                var userDto = new UserDto(userPostDto);

                await context.SaveChangesAsync();

                return userDto;
            }
            catch (Exception e)
            {
                logger.LogError(e?.InnerException?.ToString());

                return null;
            }
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto?> UpdateUserAsync(UserPutDto userPutDto)
        {
            try
            {
                var isEmailNewExist = await context.Users.AnyAsync(u => u.Email == userPutDto.NewEmail);
                if (isEmailNewExist)
                {
                    logger.LogError("The new email already exists.");

                    return null;
                }
                var isCorrectPassword = await context.Users.AnyAsync(u => u.Password == userPutDto.CurrentPassword);
                if (!isCorrectPassword)
                {
                    logger.LogError("The current password is not correct.");

                    return null;
                }
                var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userPutDto.CurrentEmail);
                if (user != null)
                {
                    if (userPutDto.NewName != null)
                        user.Name = userPutDto.NewName;
                    if (userPutDto.NewEmail != null)
                        user.Email = userPutDto.NewEmail;
                    if (userPutDto.NewPassword != null)
                        user.Password = userPutDto.NewPassword;
                    if (userPutDto.NewBirthDate != null)
                        user.BirthDate = (DateTime)userPutDto.NewBirthDate;

                    await context.SaveChangesAsync();
                    var userDto = new UserDto(user);
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

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto?> DeleteUserAsync(int id)
        {
            try
            {
                User? user = await context.Users.FirstOrDefaultAsync(p => p.UserId == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return new UserDto(user);
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

        /// <summary>
        /// Get user from Id with his bottles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserWithBottlesAsync(int id)
        {
            var user = await context.Users.Include(u => u.Bottles).Where(b => b.UserId == id).Select(u => new UserDto(u,u.Bottles)).FirstOrDefaultAsync();
            if (user == null)
            {
                logger.LogError("Item not found");
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get user from Id with his drawers.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserWithDrawersAsync(int id)
        {
            var user = await context.Users.Include(u => u.Drawers).Where(d => d.UserId == id).Select(u=> new UserDto(u,u.Drawers)).FirstOrDefaultAsync();
            if (user == null)
            {
                logger.LogError("Item not found");
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get user from Id with his caves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDto?> GetUserWithCavesAsync(int id)
        {
            var user = await context.Users.Include(u => u.Caves).Where(c=>c.UserId == id).Select(u=>new UserDto(u,u.Caves)).FirstOrDefaultAsync();
            if (user == null)
            {
                logger.LogError("Item not found");
                return null;
            }
            return user;
        }
    }
}
