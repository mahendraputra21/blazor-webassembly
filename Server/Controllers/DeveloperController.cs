using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Domains;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DeveloperController(ApplicationDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devList = await (from devs in _context.Developers.AsNoTracking()
                join position in _context.Positions.AsNoTracking()
                    on devs.PositionId equals position.PositionId
                select new
                {
                    devs.Id,
                    devs.FirstName,
                    devs.LastName,
                    devs.Email,
                    position.PositionId,
                    position.PositionName,
                    devs.Experience
                }).ToListAsync();

            return Ok(devList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existingDev = await (from devs in _context.Developers
                join position in _context.Positions
                    on devs.PositionId equals position.PositionId
                select new
                {
                    devs.Id,
                    devs.FirstName,
                    devs.LastName,
                    devs.Email,
                    position.PositionId,
                    position.PositionName,
                    devs.Experience
                }).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(existingDev);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DeveloperModel developerModel)
        {

            var developer = DeveloperMapping(developerModel);
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();
            return Ok(developer.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(DeveloperModel developerModel)
        {

            var developer = DeveloperMapping(developerModel);
            _context.Entry(developer).State = EntityState.Modified;
            _context.Update(developer);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new Developer { Id = id };
            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #region PrivateMethod
        private static Developer DeveloperMapping(DeveloperModel developerModel)
        {
            Developer? developer = null;

            developer = new()
            {
                FirstName = developerModel.FirstName,
                LastName = developerModel.LastName,
                Email = developerModel.Email,
                PositionId = developerModel.PositionId,
                Experience = developerModel.Experience
            };
            return developer;
        }

        #endregion

    }
}
