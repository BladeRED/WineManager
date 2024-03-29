﻿using WineManager.DTO;
using WineManager.Entities;

namespace WineManager.IRepositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get all users form DB
        /// </summary>
        /// <returns></returns>
        Task<List<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Get user from Id.
        /// </summary>
        /// <param name="id">Id user</param>
        /// <returns></returns>
        Task<UserDto?> GetUserAsync(int id);

        /// <summary>
        /// Add a new user.
        /// </summary>
        /// <param name="userPostDto"></param>
        /// <returns></returns>
        Task<UserDto?> AddUserAsync(UserPostDto userPostDto);

        /// <summary>
        /// Update user from Id.
        /// </summary>
        /// <param name="userPutDto"></param>
        /// <returns></returns>
        Task<UserDto?> UpdateUserAsync(UserPutDto userPutDto);

        /// <summary>
        /// Delete user from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDto?> DeleteUserAsync(int id);

        /// <summary>
        /// Get user from Id with his bottles.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDtoGet?> GetUserWithBottlesAsync(int id);

        /// <summary>
        /// Get user from Id with his drawer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDtoGet?> GetUserWithDrawersAsync(int id);

        /// <summary>
        /// Get user from Id with his caves.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UserDtoGet?> GetUserWithCavesAsync(int id);

        /// <summary>
        /// Login user from email and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Task<User> LoginUserAsync(string login, string pwd);

        /// <summary>
        /// Export the list of bottles,caves and drawer of the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ListDTO> ExportListUserAsync(int id);
    }
}
