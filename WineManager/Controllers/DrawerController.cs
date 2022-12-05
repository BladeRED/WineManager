using WineManager.Entities;
using WineManager.IRepositories;
//using WineManager.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using WineManager.IRepositories;

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

        [HttpGet]
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
        /// 

        [HttpGet("{id}")]
        public async Task<ActionResult<Drawer>> GetDrawer(int id)
        {
            return Ok(await drawerRepository.GetByIdAsync(id));
        }

        [HttpPost]
        [ProducesResponseType(666)]


        public async Task<ActionResult<Drawer>> AddDrawer(Drawer drawer)
        {


            var MyDrawer = new Drawer()
            {
                Level = drawer.Level,
                MaxPosition = drawer.MaxPosition,
              
            };
            var drawerAddd = await drawerRepository.AddDrawerAsync(drawer);

            if (drawerAddd == null)
                return Problem("Error when creating drawer, see log.");

            //if (!string.IsNullOrEmpty(drawer.Picture?.FileType) && drawer.Picture.FileType.Length > 0)

            //{// service IWebHostEnvironment
            //    var path = Path.Combine(environment.WebRootPath, "Pictures/", drawer.Picture.FileType);
            //    using (FileStream stream = new FileStream(path, FileMode.Add)) { await drawer.Picture.CopyToAsync(stream); stream.Close(); }
            //}

            return Ok(drawerAddd);
        }


        [HttpPut]
        [ProducesResponseType(231)]

        public async Task<ActionResult<Drawer>> UpdateDrawer(Drawer drawer)
        {
            var drawerUpdated = await drawerRepository.UpdateDrawerAsync(drawer);

            if (drawerUpdated != null)
                return Ok(drawerUpdated);
            else
                return Problem("Drawer was not updated, see log for details");
        }

        [HttpDelete]
        [ProducesResponseType(244)]

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


