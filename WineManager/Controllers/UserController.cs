﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;

namespace WineManager.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var userDtoList = await userRepository.GetAllUsersAsync();

            return Ok(userDtoList);
        }

        /// <summary>
        /// Get user from Id
        /// </summary>
        /// <param name="id">Id user</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto?>> GetUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("No id valuable found in the request");
            }
            var userDto = await userRepository.GetUserAsync(id);
            if (userDto == null)
                return NotFound("User in not found.");

            return Ok(userDto);
        }

        /// <summary>
        /// Add a user
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="email"></param>
        /// <param name="birthDate"> format example: "2000-05-23" (without the string on SWAGGER) </param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> AddUser([FromForm] UserPostDto userDto)
        {
            var userCreated = await userRepository.AddUserAsync(userDto);

            if (userCreated != null)
                return Ok(userCreated);
            else
                return Problem("User not created");
        }

        /// <summary>
        /// Update a user from email
        /// </summary>
        /// <param name="birthDate"> format example: "2000-05-23" (without the string on SWAGGER) </param>
        /// <param name="userPutDto"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UserDto>> UpdateUser([FromForm] UserPutDto userPutDto)
        {
            var userModified = await userRepository.UpdateUserAsync(userPutDto);

            if (userModified != null)
                return Ok(userModified);
            else
                return Problem("User not modified");
        }

        /// <summary>
        /// Delete an User
        /// </summary>
        /// <param name="id">Find a user by its id and delete it</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var userRemoved = await userRepository.DeleteUserAsync(id);
            if (userRemoved != null)
                return Ok(userRemoved);
            else
                return NotFound("The User is not found.");
        }

        /// <summary>
        /// Get user from id with his bottles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto?>> GetUserWithBottles(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var userDto = await userRepository.GetUserWithBottlesAsync(id);
            if (userDto == null)
                return NotFound("The User is not found.");
            else
                return Ok(userDto);
        }

        /// <summary>
        /// Get user from id with his drawers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUserWithDrawers(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var userDto = await userRepository.GetUserWithDrawersAsync(id);
            if (userDto == null)
                return NotFound("The User is not found.");
            else
                return Ok(userDto);
        }

        /// <summary>
        /// Get user from id with his caves
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDto>> GetUserWithCaves(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var userDto = await userRepository.GetUserWithCavesAsync(id);
            if (userDto == null)
                return NotFound("The User is not found.");
            else
                return Ok(userDto);
        }
    }
}