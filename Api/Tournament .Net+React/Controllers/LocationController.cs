using KnightTournament.BLL.Implementations;
using KnightTournament.Extensions;
using KnightTournament.Models;
using KnightTournament.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KnightTournament.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly LocationService _service;

        public LocationController(LocationService locationService)
        {
            _service = locationService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(LocationDetailsViewModel locationDetailsViewModel)
        {
            var location = new Location();
            locationDetailsViewModel.MapTo(ref location);
            var result = await _service.AddAsync(location);

            if (!result.IsSuccessful)
            {
                return Problem(result.Message);
            }

            return Ok("created successfully");
        }
    }
}
