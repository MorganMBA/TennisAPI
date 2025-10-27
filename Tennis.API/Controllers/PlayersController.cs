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
        private readonly IPlayerStatsService _playerStatsService;
        public PlayersController(IPlayerService playerService, IPlayerStatsService playerStatsService)
        {
            _playerService = playerService;
            _playerStatsService = playerStatsService;
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

        [HttpGet("stats")]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _playerStatsService.GetStatisticsAsync();

            if (stats == null)
                return NoContent();

            return Ok(stats);
        }
    }
}
