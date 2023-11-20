using KnightTournament.BLL.Implementations;
using KnightTournament.Models.Enums;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;
using KnightTournament.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoundController : Controller
    {
        private readonly RoundService _roundService;

        public RoundController(RoundService roundService)
        {
            _roundService = roundService;

        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display(Guid tournamentId)
        {
            var getResult = await _roundService.GetAllAsync(round => round.TournamentId.ToString() == tournamentId.ToString());
            if (getResult.IsSuccessful)
            {
                return Ok(getResult.Data);
            }

            return Problem(getResult.Message);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _roundService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return Problem(getByIdResult.Message);
            }

            var roundDetailsViewModel = new RoundDetailsViewModel();
            getByIdResult.Data.MapTo(ref roundDetailsViewModel);
            return Ok(roundDetailsViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]RoundDetailsViewModel roundDetailsViewModel, [FromQuery]Guid tournamentId)
        {
            var round = new Round();
            roundDetailsViewModel.MapTo(ref round);
            round.TournamentId = tournamentId;
            var result = await _roundService.AddAsync(round);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok("Added successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, RoundDetailsViewModel tournamentDetailsViewModel)
        {
            var result = await _roundService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }
            var round = result.Data;
            tournamentDetailsViewModel.MapTo(ref round);
            var updResult = await _roundService.UpdateAsync(id, round);
            if (!updResult.IsSuccessful)
            {
                return Problem(updResult.Message);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var delete = await _roundService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return Problem(delete.Message);
            }

            return Ok("Deleted successfully");
        }
    }
}
