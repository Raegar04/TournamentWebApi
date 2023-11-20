using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrophyController : Controller
    {
        private readonly TrophyService _service;

        public TrophyController(TrophyService trophyService)
        {
            _service = trophyService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TrophyDetailsViewModel trophyDetailsViewModel, [FromQuery]Guid roundId)
        {
            var trophy = new Trophy();
            trophyDetailsViewModel.MapTo(ref trophy);
            trophy.RoundId = roundId;
            var result = await _service.AddAsync(trophy);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok("Added successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id) 
        {
            var result = await _service.DeleteAsync(id);
            if (!result.IsSuccessful) 
            {
                return Problem(result.Message);
            }

            return Ok("Deleted successfully");
        }
    }
}
