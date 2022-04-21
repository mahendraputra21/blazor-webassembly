using Blazor.Learner.Server.Data;
using Blazor.Learner.Shared.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Learner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PositionController(ApplicationDBContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get All Positions 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPosition()
        {
            var positions = await _context.Positions.Select(x => new
                {
                    x.PositionId,
                    x.PositionName
                })
                .ToListAsync();
            return Ok(positions);
        }

        /// <summary>
        /// Get Spesific Position By Id
        /// </summary>
        /// <param name="id"> ID position to return</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPositionById(int id)
        {
            var positionById = await _context.Positions
                .Where(x => x.PositionId == id)
                .Select(x => x.PositionName)
                .ToListAsync();

            return Ok(positionById);
        }

        /// <summary>
        /// Add New Position
        /// </summary>
        /// <param name="position">Input param for add new Position</param>
        /// <returns>A newly created Position</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Position
        ///     {
        ///        "PositionName": "Internal Auditor"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created Position</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        public async Task<IActionResult> Post(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return Ok(position);
        }
    }
}
