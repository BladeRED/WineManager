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

        [HttpGet]
        public async Task<ActionResult<List<Cave>>> GetAllCaves()
        {
            var caves = await caveRepository.GetCavesAsync();

            return Ok(caves);
        }

        /// <summary>
        /// Get cave from Id
        /// </summary>
        /// <param name="id">Id cave</param>
        /// <returns></returns>
        /// 

        [HttpGet("{id}")]
        public async Task<ActionResult<Cave>> GetCave(int id)
        {
            return Ok(await caveRepository.GetByIdAsync(id));
        }

        [HttpGet("{id}")]
        //[HttpGet("GetCaveWithDrawer/{id}")]
        public async Task<ActionResult<Cave>> GetCaveWithDrawer(int id)
        {
            return Ok(await caveRepository.GetWithDrawerAsync(id));
        }

        [HttpGet("{id}")]
        //[HttpGet("GetCaveWithDrawer/{id}")]
        public async Task<ActionResult<Cave>> GetCaveWithUser(int id)
        {
            return Ok(await caveRepository.GetWithUserAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(666)]


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


        [HttpPut]
        [ProducesResponseType(231)]

        public async Task<ActionResult<Cave>> UpdateCave([FromForm] CaveDto caveDto)
        {
            var cave = new Cave()
            {
                CaveId= caveDto.CaveId,
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

        [HttpDelete]
        [ProducesResponseType(244)]

        public async Task<ActionResult<Cave>> DeleteCave(int id)
        {
            var caveDeleted = await caveRepository.DeleteCaveAsync(id);

            if (caveDeleted != null)
                return Ok(caveDeleted);
            else
                return Problem("Cave was not deleted, see log for details");
        }
    }
}


