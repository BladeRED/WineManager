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
        /// Get all drawers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<Drawer>>> GetAllDrawers()
        {
            var drawers = await drawerRepository.GetDrawersAsync();

            return Ok(drawers);
        }

        /// <summary>
        /// Get drawer from Id
        /// </summary>
        /// <param name="id">Id drawer</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Drawer>> GetDrawer(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var drawer = await drawerRepository.GetByIdAsync(id);
            if (drawer == null)
            {
                return NotFound("No drawer found");
            }
            return Ok(drawer);
        }

        /// <summary>
        /// Get drawer from Id with bottles
        /// </summary>
        /// <param name="id">Id Drawer</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetDrawerWithBottles(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var drawer = await drawerRepository.GetDrawerWithBottlesAsync(id);
            if (drawer == null)
            {
                return NotFound("No drawer found");
            }
            return Ok(drawer);
        }

        /// <summary>
        /// Get drawer from Id with cave
        /// </summary>
        /// <param name="id">Id Drawer</param>
        /// <returns></returns>
        /// 
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetDrawerWithCave(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var drawer = await drawerRepository.GetDrawerWithCaveAsync(id);
            if (drawer == null)
            {
                return NotFound("No drawer found");
            }
            return Ok(drawer);
        }

        /// <summary>
        /// Get drawer from Id with user
        /// </summary>
        /// <param name="id">Id Drawer</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Cave>> GetDrawerWithUser(int id)
        {
            if (id < 1)
            {
                return BadRequest("No valuable id found in the request");
            }
            var drawer = await drawerRepository.GetDrawerWithUserAsync(id);
            if (drawer == null)
            {
                return NotFound("No drawer found");
            }
            return Ok(drawer);
        }

        /// <summary>
        /// Add drawer
        /// </summary>
        /// <param name="drawerPostDto">Return a DrawerPostDto object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Drawer>> AddDrawer([FromForm] DrawerPostDto drawerPostDto)
        {
            var NewDrawer = new Drawer()
            {
                Level = drawerPostDto.Level,
                MaxPosition = drawerPostDto.MaxPosition,
                CaveId = drawerPostDto.CaveId

            };
            var drawerAdd = await drawerRepository.AddDrawerAsync(NewDrawer);

            if (drawerAdd == null)
                return Problem("Error when creating drawer, see log.");

            //if (!string.IsNullOrEmpty(drawer.Picture?.FileType) && drawer.Picture.FileType.Length > 0)

            //{// service IWebHostEnvironment
            //    var path = Path.Combine(environment.WebRootPath, "Pictures/", drawer.Picture.FileType);
            //    using (FileStream stream = new FileStream(path, FileMode.Add)) { await drawer.Picture.CopyToAsync(stream); stream.Close(); }
            //}

            return Ok(drawerAdd);
        }

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
        /// Update a drawer
        /// </summary>
        /// <param name="drawerDto">Maj a DrawerDto object</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Drawer>> UpdateDrawer([FromForm] DrawerDto drawerDto)
        {

            var MajDrawer = new Drawer()
            {
                DrawerId = drawerDto.DrawerId,
                Level = drawerDto.Level,
                MaxPosition = drawerDto.MaxPosition,
                CaveId = drawerDto.CaveId,
            };

            var drawerUpdated = await drawerRepository.UpdateDrawerAsync(MajDrawer);

            if (drawerUpdated != null)
                return Ok(drawerUpdated);
            else
                return Problem("Drawer was not updated, see log for details");
        }

        /// <summary>
        /// Range a Drawer into a Cave
        /// The Drawer and the Cave must belong to the connected user
        /// </summary>
        /// <param name="drawerId"></param>
        /// <param name="caveId"></param>
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

            var drawerPutted = await drawerRepository.StockDrawerAsync(drawerId,caveId, userId, caveLevel);

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
            var drawerDeleted = await drawerRepository.DeleteDrawerAsync(id);

            if (drawerDeleted != null)
                return Ok(drawerDeleted);
            else
                return Problem("Drawer was not deleted, see log for details");
        }
    }
}


