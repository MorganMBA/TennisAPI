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

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetAllAsync();

            if (players == null || !players.Any())
                return NoContent();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById(int id)
        {
            var player = await _playerService.GetByIdAsync(id);

            if (player == null) return NotFound(new { message = $"Le joueur avec l'id {id} est introuvable." });

            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDto player)
        {
            await _playerService.AddAsync(player);
            return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
        }
    }
}
