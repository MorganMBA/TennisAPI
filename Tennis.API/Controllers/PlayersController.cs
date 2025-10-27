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
        private readonly ILogger<PlayersController> _logger;
        public PlayersController(IPlayerService playerService, IPlayerStatsService playerStatsService, ILogger<PlayersController> logger)
        {
            _playerService = playerService;
            _playerStatsService = playerStatsService;
            _logger = logger;
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
            try
            {
                var player = await _playerService.GetByIdAsync(id);
                return Ok(player);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Joueur introuvable (ID={Id})", id);
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDto player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _playerService.AddAsync(player);
                _logger.LogInformation("Joueur ajouté : {Firstname} {Lastname}", player.Firstname, player.Lastname);
                return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Erreur de validation lors de l'ajout d'un joueur");
                return BadRequest(new { message = ex.Message });
            }
            
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
