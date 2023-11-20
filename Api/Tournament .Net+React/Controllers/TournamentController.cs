using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.Models.Enums;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TournamentController : Controller
    {
        private readonly TournamentService _tournamentService;

        public TournamentController(TournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display()
        {
            var getResult = await _tournamentService.GetAllAsync();
            if (getResult.IsSuccessful)
            {
                return Ok(getResult.Data);
            }

            return Problem(getResult.Message);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _tournamentService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return Problem(getByIdResult.Message);
            }

            var tournamentDetailViewModel = new TournamentDetailsViewModel();
            getByIdResult.Data.MapTo(ref tournamentDetailViewModel);
            return Ok(tournamentDetailViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]TournamentDetailsViewModel tournamentDetailsViewModel)
        {
            var tournament = new Tournament();
            tournamentDetailsViewModel.MapTo(ref tournament);
            var result = await _tournamentService.AddAsync(tournament);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok("Added successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id, TournamentDetailsViewModel tournamentDetailsViewModel)
        {
            var result = await _tournamentService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }
            var tournament = result.Data;
            tournamentDetailsViewModel.MapTo(ref tournament);
            var updResult = await _tournamentService.UpdateAsync(id, tournament);
            if (updResult.IsSuccessful) 
            {
                return Problem(updResult.Message);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _tournamentService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return Problem(delete.Message);
            }

            return Ok("Deleted successfully");
        }
    }
}
