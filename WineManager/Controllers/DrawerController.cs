using WineManager.Entities;
using WineManager.IRepositories;
//using WineManager.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using WineManager.DTO;
using WineManager.Repositories;

namespace WineManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DrawerController : ControllerBase
    {
        IDrawerRepository drawerRepository;

        readonly IWebHostEnvironment environment;
        public DrawerController(IDrawerRepository drawerRepository, IWebHostEnvironment environment)
        {
            this.drawerRepository = drawerRepository;
            this.environment = environment;
        }

        /// <summary>
        /// Get drawer from drawer's ID.
        /// </summary>
        /// <param name="drawerId">Drawer's ID</param>
        /// <returns></returns>
        [HttpGet("{drawerId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drawer>> GetDrawer(int drawerId)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
            {
                return Problem("You must log in order to see your Drawer ! Check/ User / Login");
            }
            var currentUserId = Int32.Parse(idCurrentUser.Value);


            var drawer = await drawerRepository.GetDrawerAsync(drawerId, currentUserId);
            if (drawer == null)
            {
                return NotFound("No drawer found");
            }
            return Ok(drawer);
        }

        /// <summary>
        /// Add drawer
        /// </summary>
        /// <param name="drawerDto">Return a DrawerPostDto object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Drawer>> AddNewDrawerToUser([FromForm] DrawerPostToUserDto drawerDto)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your drawers ! Check/ User / Login");

            var newDrawer = new Drawer()
            {
                MaxPosition = drawerDto.MaxPosition,
                Level = drawerDto.Level,
                CaveId = drawerDto.CaveId,
                UserId = Int32.Parse(idCurrentUser.Value)
            };

            var drawerAdded = await drawerRepository.AddDrawerAsync(newDrawer);
            if (drawerAdded == null)
                return Problem("Error when creating drawer, see log.");

            return Ok(drawerAdded);
        }

        /// <summary>
        /// Range a Drawer into a Cave
        /// The Drawer and the Cave must belong to the connected user
        /// </summary>
        /// <param name="drawerId"></param>
        /// <param name="caveId"></param>
        /// <param name="caveLevel"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Drawer>> StockDrawer(int drawerId, int caveId, int caveLevel)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your drawers ! Check/ User / Login");

            var userId = Int32.Parse(idCurrentUser.Value);

            var drawerPutted = await drawerRepository.StockDrawerAsync(drawerId, caveId, userId, caveLevel);

            if (drawerPutted != null)
                return Ok(drawerPutted);
            else
                return Problem("Drawer was not updated, see log for details");
        }


        /// <summary>
        /// Delete drawer
        /// </summary>
        /// <param name="id">Find a drawer by its id and delete it</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Drawer>> DeleteDrawer(int id)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to delete your drawer ! Check/ User / Login");

            var drawerDeleted = await drawerRepository.DeleteDrawerAsync(id, int.Parse(idCurrentUser.Value));

            if (drawerDeleted != null)
                return Ok(drawerDeleted);
            else
                return Problem("Drawer was not deleted, see log for details");
        }
    }
}


