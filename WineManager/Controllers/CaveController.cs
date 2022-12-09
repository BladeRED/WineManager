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
        /// Get cave from with Id
        /// </summary>
        /// <param name="caveId">Id cave</param>
        /// <returns></returns>
        [HttpGet("{caveId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetCave(int caveId)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
            {
                return Problem("You must log in order to see your Bottle ! Check/ User / Login");
            }
            var currentUserId = Int32.Parse(idCurrentUser.Value);

            var cave = await caveRepository.GetCaveAsync(caveId, currentUserId);
            if (cave == null)
            {
                return NotFound("No cave found");
            }
            return Ok(cave);
        }

        /// <summary>
        /// Add cave
        /// </summary>
        /// <param name="caveDto">Return a CavePosToUsertDto object called caveDto </param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<Cave>> AddNewCaveToUser([FromForm] CavePostToUserDto caveDto)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to add cave ! Check/ User / Login");

            var newCave = new Cave()
            {
                CaveType = caveDto.CaveType,
                Family = caveDto.Family,
                Brand = caveDto.Brand,
                Temperature = caveDto.Temperature,
                UserId = Int32.Parse(idCurrentUser.Value),
                NbMaxDrawer = caveDto.NbMaxDrawer,
                NbMaxBottlePerDrawer = caveDto.NbMaxBottlePerDrawer
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
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to delete your cave ! Check/ User / Login");

            var caveDeleted = await caveRepository.DeleteCaveAsync(id, int.Parse(idCurrentUser.Value));

            if (caveDeleted != null)
                return Ok(caveDeleted);
            else
                return Problem("Cave was not deleted, see log for details");
        }
    }
}


