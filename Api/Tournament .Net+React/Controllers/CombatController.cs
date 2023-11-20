using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CombatController : Controller
    {
        private readonly CombatService _combatService;

        public CombatController(CombatService combatService)
        {
            _combatService = combatService;
        }

        [HttpGet("Display")]
        public async Task<IActionResult> Display([FromQuery]Guid roundId)
        {
            var getResult = await _combatService.GetAllAsync(round => round.RoundId.ToString() == roundId.ToString());
            if (getResult.IsSuccessful)
            {
                return Ok(getResult.Data);
            }

            return Problem(getResult.Message);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var getByIdResult = await _combatService.GetByIdAsync(id);
            if (!getByIdResult.IsSuccessful)
            {
                return Problem(getByIdResult.Message);
            }

            var combatDetailsViewModel = new CombatDetailsViewModel();
            getByIdResult.Data.MapTo(ref combatDetailsViewModel);
            return View(combatDetailsViewModel);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CombatDetailsViewModel combatDetailsViewModel, [FromQuery]Guid roundId)
        {
            var combat = new Combat();
            combatDetailsViewModel.MapTo(ref combat);
            combat.RoundId = roundId;
            var result = await _combatService.AddAsync(combat);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok("Added successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, CombatDetailsViewModel combatDetailsViewModel)
        {
            var result = await _combatService.GetByIdAsync(id);
            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }
            var combat = result.Data;
            combatDetailsViewModel.MapTo(ref combat);
            var updResult = await _combatService.UpdateAsync(id, combat);
            if (!updResult.IsSuccessful) 
            {
                return Problem(updResult.Message);
            }

            return Ok("Updated successfully");
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var delete = await _combatService.DeleteAsync(id);
            if (!delete.IsSuccessful)
            {
                return Problem(delete.Message);
            }

            return Ok("Deleted successfully");
        }
    }
}
