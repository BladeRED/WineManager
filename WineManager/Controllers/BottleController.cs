using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WineManager.DTO;
using WineManager.Entities;
using WineManager.IRepositories;
using WineManager.Repositories;

namespace WineManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        IBottleRepository bottleRepository;
        IUserRepository userRepository;
        public BottleController(IBottleRepository bottleRepository)
        {
            this.bottleRepository = bottleRepository;
        }

        /// <summary>
        /// Get bottle from Id.
        /// </summary>
        /// <param name="bottleId">Bottle's ID.</param>
        /// <returns></returns>
        [HttpGet("{bottleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Bottle>> GetBottle(int bottleId)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
            {
                return Problem("You must log in order to see your Bottle ! Check/ User / Login");
            }
            var currentUserId = Int32.Parse(idCurrentUser.Value);

            var bottle = await bottleRepository.GetBottleAsync(bottleId, currentUserId);
            if (bottle == null)
            {
                return NotFound("No bottle found");
            }
            return Ok(bottle);
        }

        /// <summary>
        /// Add a new bottle.
        /// </summary>
        /// <param name="bottle"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Bottle>> AddNewBottleToUser([FromForm] BottleDto bottleDto)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to add bottles ! Check/ User / Login");

            var newBottle = new Bottle()
            {
                Name = bottleDto.Name,
                Vintage = bottleDto.Vintage,
                StartKeepingYear = bottleDto.StartKeepingYear,
                EndKeepingYear = bottleDto.EndKeepingYear,
                Color = bottleDto.Color,
                Designation = bottleDto.Designation,
                UserId = Int32.Parse(idCurrentUser.Value)
            };

            var bottleAdded = await bottleRepository.AddBottleAsync(newBottle);
            return Ok(bottleAdded);
        }

        /// <summary>
        /// Duplicate a new bottle, with a quantity for multiply the add requests.
        /// </summary>
        /// <param name="bottleDupl"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Bottle>> DuplicateBottle([FromForm] BottleDtoDupl bottleDupl, int quantity)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to duplicate bottles ! Check/ User / Login");
            int userId = Int32.Parse(idCurrentUser.Value);
            List<Bottle> Bottles = new List<Bottle>();

            for (int i = 0; i < quantity; i++)
            {
                Bottle ListBottle = new Bottle()
                {
                    Name = bottleDupl.Name,
                    Vintage = bottleDupl.Vintage,
                    StartKeepingYear = bottleDupl.StartKeepingYear,
                    EndKeepingYear = bottleDupl.EndKeepingYear,
                    Color = bottleDupl.Color,
                    Designation = bottleDupl.Designation,
                    UserId = userId
                };

                Bottles.Add(ListBottle);
            }
            var bottleCreated = await bottleRepository.DuplicateBottleAsync(Bottles, quantity);
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
        [ProducesResponseType(500)]
        public async Task<ActionResult<Bottle>> UpdateBottle([FromForm] BottleDtoPut bottleDtoPut)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log before updating a bootle ! Check/ User / Login");
            int userId = Int32.Parse(idCurrentUser.Value);

            var MajBottle = new BottleDtoPut()
            {
                BottleId = bottleDtoPut.BottleId,
                Name = bottleDtoPut.Name,
                Vintage = bottleDtoPut.Vintage,
                Designation = bottleDtoPut.Designation,
                StartKeepingYear = bottleDtoPut.StartKeepingYear,
                EndKeepingYear = bottleDtoPut.EndKeepingYear,
                Color = bottleDtoPut.Color,

            };
            var bottleUpdated = await bottleRepository.UpdateBottleAsync(MajBottle, userId);

            if (bottleUpdated != null)
                return Ok(bottleUpdated);
            else
                return Problem("Bottle non modifié, cf log");
        }

        /// <summary>
        /// Stock Bottle into a cave
        /// </summary>
        /// <param name="bottleDtoStock"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Bottle>> StockBottle([FromForm] BottleDtoStock bottleDtoStock)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to see your bottles ! Check/ User / Login");

            var userId = Int32.Parse(idCurrentUser.Value);

            var bottleStocked = await bottleRepository.StockBottleAsync(bottleDtoStock, userId);

            if (bottleStocked != null)
                return Ok(bottleStocked);
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
        [ProducesResponseType(404)]
        public async Task<ActionResult<Bottle>> DeleteBottle(int id)
        {
            var identity = User?.Identity as ClaimsIdentity;
            var idCurrentUser = identity?.FindFirst(ClaimTypes.NameIdentifier);
            if (idCurrentUser == null)
                return Problem("You must log in order to delete your bottles ! Check/ User / Login");

            var bottleDeleted = await bottleRepository.DeleteBottleAsync(id, int.Parse(idCurrentUser.Value));

            if (bottleDeleted != null)
                return Ok(bottleDeleted);
            else
                return NotFound("Bottle not found");
        }
    }
}

