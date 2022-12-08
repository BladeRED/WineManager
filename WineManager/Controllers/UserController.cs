using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;
using Azure;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using WineManager.Repositories;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.IO;
using WineManager.Helpers;

namespace WineManager.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserRepository userRepository;
        IBottleRepository bottleRepository;
        ICaveRepository caveRepository;
        IDrawerRepository drawerRepository;
        readonly IWebHostEnvironment environment;

        public UserController(IUserRepository userRepository, IBottleRepository bottleRepository, ICaveRepository caveRepository, IDrawerRepository drawerRepository, IWebHostEnvironment environment)
        {
            this.userRepository = userRepository;
            this.bottleRepository = bottleRepository;
            this.caveRepository = caveRepository;
            this.drawerRepository = drawerRepository;
            this.environment = environment;
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
        /// Get user from id with his drawers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserDtoGet>> GetUserWithDrawers(int id)
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
        public async Task<ActionResult<UserDtoGet>> GetUserWithCaves(int id)
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

        /// <summary>
        /// Get all caves of current logged user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<CaveDtoLight>>> GetUserCaves()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your caves ! Check/ User / Login");
            var userDto = await userRepository.GetUserWithCavesAsync(Int32.Parse(idCurrentUser.Value));
            if (userDto == null)
                return NotFound("The User is not found.");
            var caves = userDto.Caves;
            if (caves != null)
            {
                return Ok(caves);
            }
            return NotFound("No cave found.");
        }

        /// <summary>
        /// Get all caves of current logged user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<DrawerDtoLight>>> GetUserDrawers()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your drawers ! Check/ User / Login");
            var userDto = await userRepository.GetUserWithDrawersAsync(Int32.Parse(idCurrentUser.Value));
            if (userDto == null)
                return NotFound("The User is not found.");
            var drawers = userDto.Drawers;
            if (drawers != null)
            {
                return Ok(drawers);
            }
            return NotFound("No drawer found.");
        }

        /// <summary>
        /// Get all caves of current logged user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<BottleDtoLight>>> GetUserBottles()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your bottles ! Check/ User / Login");
            var userDto = await userRepository.GetUserWithBottlesAsync(Int32.Parse(idCurrentUser.Value));
            if (userDto == null)
                return NotFound("The User is not found.");
            var bottles = userDto.Bottles;
            if (bottles != null)
            {
                return Ok(bottles);
            }
            return NotFound("No bottle found.");
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
        public async Task<ActionResult<UserDtoGet?>> GetUserWithBottles(int id)
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
        /// Login of a user from email and password
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LoginUser([DefaultValue("test@test.com")] string login, [DefaultValue("test")] string pwd)
        {
            var userCreated = await userRepository.LoginUserAsync(login, pwd);
            if (userCreated == null)
                return Problem("Error in login, verify your password and email.");
            Claim emailClaim = new(ClaimTypes.Email, userCreated.Email);
            Claim nameClaim = new(ClaimTypes.Name, userCreated.Name);
            Claim dobClaim = new(ClaimTypes.DateOfBirth, userCreated.BirthDate.ToString());
            Claim idClaim = new(ClaimTypes.NameIdentifier, userCreated.UserId.ToString());
            ClaimsIdentity identity = new(new List<Claim> { emailClaim, nameClaim, dobClaim, idClaim }, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
            ClaimsPrincipal(identity));
            return Ok($"{userCreated.Name} logged");
        }

        /// <summary>
        /// Log out the current user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok("Logout");
        }

        /// <summary>
        /// Export the list of bottles,caves and drawer of the current user
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportListUser()
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to export ! Check/ User / Login");
            var response = await userRepository.ExportListUserAsync(int.Parse(idCurrentUser.Value));
            string usersJson = JsonSerializer.Serialize(response, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles });
            var path = Path.Combine(environment.WebRootPath, "ListUsers/");
            string fileName = path + idCurrentUser.Value + ".json";

            System.IO.File.WriteAllText(fileName, usersJson);

            return Ok(fileName);
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
        /// Add a user
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="email"></param>
        /// <param name="birthDate"> format example: "2000-05-23" (without the string on SWAGGER) </param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult?> SignUp([FromForm] UserPostDto userDto, bool CGU)
        {
            var age = (DateTime.Now - userDto.BirthDate).TotalDays / 365.25;
            if (CGU)
            {
                if (age >= 18)
                {

                    var user = await AddUser(userDto);
                    if (user != null)
                    {
                        var userConnected = await userRepository.LoginUserAsync(userDto.Email, userDto.Password);
                        return Ok($"{userConnected.Name} logged");
                    }
                    return BadRequest("Error in the request");
                }
                else
                    return Problem("You are not worthy.");
            }
            return Problem("CGU not accepted");
        }

        /// <summary>
        /// Import a json from the user
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult?> ImportListUser(IFormFile formFile)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log before import a list ! Check/ User / Login");
            var id = int.Parse(idCurrentUser.Value);
            if (!string.IsNullOrEmpty(formFile.FileName) && formFile.FileName.Length > 0)
            {
                if (formFile.ContentType == "application/json")
                {
                    var str = await formFile.ReadAsStringAsync();
                    var fileJson = JsonSerializer.Deserialize<ListDTO>(str);
                    var bottles = fileJson.Bottles;
                    foreach (var item in bottles)
                    {
                        var b = new Bottle(item, id);
                        await bottleRepository.AddBottleAsync(b);
                    }
                    var caves = fileJson.Caves;
                    foreach (var item in caves)
                    {
                        var c = new Cave(item, id);
                        await caveRepository.AddCaveAsync(c);
                    }
                    var drawers = fileJson.Drawers;
                    foreach (var item in drawers)
                    {
                        var c = new Drawer(item, id);
                        await drawerRepository.AddDrawerAsync(c);
                    }
                    return Ok("This is ok");
                }
                return Problem("No json file found");
            }
            return Problem("No file found");
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
    }
}
