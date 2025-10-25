using Microsoft.AspNetCore.Mvc;
using Tennis.Core.Dtos;
using Tennis.Domain.Services.Interfaces;

namespace Tennis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;
        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDto player)
        {
            await _playerService.AddAsync(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }
    }
}
