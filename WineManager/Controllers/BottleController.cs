using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;

namespace WineManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        IBottleRepository bottleRepository;
        public BottleController(IBottleRepository bottleRepository)
        {
            this.bottleRepository = bottleRepository;
        }

        /// <summary>
        /// Get all bottles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Bottle>>> GetAllBottles()
        {
            var bottles = await bottleRepository.GetAllBottlesAsync();

            return Ok(bottles);
        }

        /// <summary>
        /// Get bottle from Id.
        /// </summary>
        /// <param name="id">Id bottle</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bottle>> GetBottle(int id)
        {

            if (id == null)
            {

                return NotFound("No bottle found");

            }
            return Ok(await bottleRepository.GetBottleAsync(id));
        }

        /// <summary>
        /// Get bottle from Id with his bottle.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bottle>> GetBottleWithUser(int id)
        {
            return Ok(await bottleRepository.GetBottleWithUserAsync(id));
        }

        /// <summary>
        /// Get bottle from Id with his article.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bottle>> GetBottleWithDrawer(int id)
        {
            return Ok(await bottleRepository.GetBottleWithDrawerAsync(id));
        }

        /// <summary>
        /// Add a new bottle.
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Bottle>> AddBottle([FromForm] BottleDto bottleDto)
        {
            //var identity = User?.Identity as ClaimsIdentity;
            //var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            //if (idCurrentUser == null)
            //    return Problem("You must log before create an article ! Check/ User / Login");
            //bottle.UserId = Int32.Parse(idCurrentUser.Value);
            var newBottle = new Bottle()
            {
                Name= bottleDto.Name,
                Vintage=bottleDto.Vintage,
                StartKeepingYear=bottleDto.StartKeepingYear,
                EndKeepingYear=bottleDto.EndKeepingYear,
                Color=bottleDto.Color,
                Designation=bottleDto.Designation,
            };
            var bottleCreated = await bottleRepository.AddBottleAsync(newBottle);

            if (bottleCreated != null)
                return Ok(bottleCreated);
            else
                return Problem("Bottle non créé, cf log");
        }

        /// <summary>
        /// Update bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bottle"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Bottle>> UpdateBottle(int id, Bottle bottle)
        {
            //var identity = User?.Identity as ClaimsIdentity;
            //var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            //if (idCurrentUser == null)
            //    return Problem("You must log before create an article ! Check/ User / Login");
            //int userId = Int32.Parse(idCurrentUser.Value);
            //if ((await bottleRepository.GetBottleAsync(id)).UserId != userId)
            //    return Problem("You must the author in order to update this article");
            //bottle.UserId = Int32.Parse(idCurrentUser.Value);
            var bottleUpdated = await bottleRepository.UpdateBottleAsync(id, bottle);

            if (bottleUpdated != null)
                return Ok(bottleUpdated);
            else
                return Problem("Bottle non modifié, cf log");
        }

        /// <summary>
        /// Delete bottle from Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Bottle>> DeleteBottle(int id)
        {

            //var identity = User?.Identity as ClaimsIdentity;
            //var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            //if (idCurrentUser == null)
            //    return Problem("You must log before create an article ! Check/ User / Login");
            //int userId = Int32.Parse(idCurrentUser.Value);
            //if ((await bottleRepository.GetBottleAsync(id)).UserId != userId)
            //    return Problem("You must the author in order to update this article");
            var bottleDelated = await bottleRepository.DeleteBottleAsync(id);

            if (bottleDelated != null)
                return Ok(bottleDelated);
            else
                return Problem("Bottle non effacé, cf log");
        }
    }
}

