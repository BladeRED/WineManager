using WineManager.Entities;
using WineManager.IRepositories;
//using WineManager.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using WineManager.IRepositories;
using WineManager.DTO;
using WineManager.Repositories;

namespace WineManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CaveController : ControllerBase
    {
        ICaveRepository caveRepository;
        readonly IWebHostEnvironment environment;
        public CaveController(ICaveRepository caveRepository, IWebHostEnvironment environment)
        {
            this.caveRepository = caveRepository;
            this.environment = environment;
        }

        /// <summary>
        /// Get all caves
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<Cave>>> GetAllCaves()
        {
            var caves = await caveRepository.GetCavesAsync();

            return Ok(caves);
        }

        /// <summary>
        /// Get cave from with Id
        /// </summary>
        /// <param name="id">Id cave</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetCave(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var cave = await caveRepository.GetByIdAsync(id);
            if (cave == null)
            {
                return NotFound("No cave found");
            }
            return Ok(cave);
        }

        /// <summary>
        /// Get cave from Id Cave with Drawer
        /// </summary>
        /// <param name="id">Id Cave</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetCaveWithDrawer(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var cave = await caveRepository.GetWithDrawerAsync(id);
            if (cave == null)
            {
                return NotFound("No cave found");
            }
            return Ok(cave);
        }

        /// <summary>
        /// Get cave from Id User
        /// </summary>
        /// <param name="id">Id User</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetCaveWithUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var cave = await caveRepository.GetWithUserAsync(id);
            if (cave == null)
            {
                return NotFound("No cave found");
            }
            return Ok(cave);
        }

        /// <summary>
        /// Add cave
        /// </summary>
        /// <param name="caveDto">Return a CavePostDto object called caveDto </param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Cave>> AddCave([FromForm] CavePostDto caveDto)
        {
            var NewCave = new Cave()
            {
                CaveType = caveDto.CaveType,
                Family = caveDto.Family,
                Brand = caveDto.Brand,
                Temperature = caveDto.Temperature,
            };
            var caveAdd = await caveRepository.AddCaveAsync(NewCave);

            if (caveAdd == null)
                return Problem("Error when creating cave, see log.");

            //if (!string.IsNullOrEmpty(cave.Picture?.FileType) && cave.Picture.FileType.Length > 0)

            //{// service IWebHostEnvironment
            //    var path = Path.Combine(environment.WebRootPath, "Pictures/", cave.Picture.FileType);
            //    using (FileStream stream = new FileStream(path, FileMode.Add)) { await cave.Picture.CopyToAsync(stream); stream.Close(); }
            //}

            return Ok(caveAdd);
        }
        [HttpPost]
        public async Task<ActionResult<Cave>> AddNewCaveToUser([FromForm] CavePostToUserDto caveDto)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your drawers ! Check/ User / Login");

            var newCave = new Cave()
            {
                CaveType = caveDto.CaveType,
                Family = caveDto.Family,
                Brand = caveDto.Brand,
                Temperature = caveDto.Temperature,
                UserId = Int32.Parse(idCurrentUser.Value)
            };

            var caveAdded = await caveRepository.AddCaveAsync(newCave);
            return Ok(caveAdded);
        }

        /// <summary>
        /// Update a Cave
        /// </summary>
        /// <param name="caveDto">Return a maj CavePostDto object called caveDto </param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Cave>> UpdateCave([FromForm] CaveDto caveDto)
        {
            var cave = new Cave()
            {
                CaveId = caveDto.CaveId,
                CaveType = caveDto.CaveType,
                Family = caveDto.Family,
                Brand = caveDto.Brand,
                Temperature = caveDto.Temperature,
            };

            var caveUpdated = await caveRepository.UpdateCaveAsync(cave);

            if (caveUpdated != null)
                return Ok(caveUpdated);
            else
                return Problem("Cave was not updated, see log for details");
        }

        /// <summary>
        /// Delete a cave
        /// </summary>
        /// <param name="id">Search a cave with the id and delete it </param>
        /// <returns></returns>
        [HttpDelete("id")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Cave>> DeleteCave(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var caveDeleted = await caveRepository.DeleteCaveAsync(id);

            if (caveDeleted != null)
                return Ok(caveDeleted);
            else
                return Problem("Cave was not deleted, see log for details");
        }
    }
}


