using Microsoft.AspNetCore.Authentication.Cookies;
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

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var userDtoList = await userRepository.GetAllUsersAsync();

            return Ok(userDtoList);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var userDto = await userRepository.GetUserAsync(id);

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
        public async Task<ActionResult<UserDto>> AddUser([FromForm] UserPostDto userDto)
        {
            
            var userCreated = await userRepository.AddUserAsync(userDto);

            if (userCreated != null)
                return Ok(userCreated);
            else
                return Problem("User not created");
        }
        /// <summary>
        ///
        /// <param name="birthDate"> format example: "2000-05-23" (without the string on SWAGGER) </param>
        /// </summary>
        /// <param name="userPutDto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromForm] UserPutDto userPutDto)
        {
            var userModified = await userRepository.UpdateUserAsync(userPutDto);

            if (userModified != null)
                return Ok(userModified);
            else
                return Problem("User not modified");
        }

        [HttpDelete]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            var userRemoved = await userRepository.DeleteUserAsync(id);
            if (userRemoved != null)
                return Ok(userRemoved);
            else
                return Problem("User not removed");
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserWithBottles(int id)
        {
            var userDto = await userRepository.GetUserWithBottlesAsync(id);

            return Ok(userDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserWithDrawers(int id)
        {
            var userDto = await userRepository.GetUserWithDrawersAsync(id);

            return Ok(userDto);
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<UserDto>> GetUserWithCaves(int id)
        {
            var userDto = await userRepository.GetUserWithCavesAsync(id);

            return Ok(userDto);
        }



    }
}
