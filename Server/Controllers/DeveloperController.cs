using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Domains;
using Blazor.Learner.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DeveloperController(ApplicationDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get All Developers
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get Spesific Developer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existingDev = await (from devs in _context.Developers.AsNoTracking()
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
                }).FirstOrDefaultAsync(x => x.Id == id);

            return Ok(existingDev);
        }

        /// <summary>
        /// Add New Developer
        /// </summary>
        /// <param name="developerModel"></param>
        /// <returns>A newly created Developer</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Developer
        ///     {
        ///        "firstName": "Jhon",
        ///        "lastName": "Doe",
        ///        "email": "jhon.doe@mailinator.com",
        ///        "experience": 9,
        ///        "positionId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created Developer</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        public async Task<IActionResult> Post(DeveloperModel developerModel)
        {

           if(developerModel.PositionId < 1)
              developerModel.PositionId = await _context.Positions.Select(x => x.PositionId).FirstOrDefaultAsync();

            var developer = DeveloperAddMapping(developerModel);
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();
            return Ok(developer.Id);
        }

        /// <summary>
        /// Update Existing Developer
        /// </summary>
        /// <param name="developerModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(DeveloperModel developerModel)
        {

            var developer = DeveloperUpdateMapping(developerModel);
            _context.Developers.Update(developer);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deletes a spesific Developer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new Developer { Id = id };
            _context.Remove(dev);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        #region PrivateMethod
        private  static Developer DeveloperAddMapping(DeveloperModel developerModel)
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

        private static Developer DeveloperUpdateMapping(DeveloperModel developerModel)
        {
            Developer? developer = null;

            developer = new()
            {
                Id = developerModel.Id,
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
