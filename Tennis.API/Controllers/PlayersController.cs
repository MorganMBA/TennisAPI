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

        /// <summary>
        /// Récupère la liste de tous les joueurs.
        /// </summary>
        /// <remarks>
        /// Renvoie un code **204** si aucun joueur n'est trouvé.
        /// </remarks>
        /// <returns>Une liste de joueurs.</returns>
        /// <response code="200">Liste des joueurs récupérée avec succès.</response>
        /// <response code="204">Aucun joueur trouvé.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlayerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _playerService.GetAllAsync();

            if (players == null || !players.Any())
                return NoContent();

            return Ok(players);
        }

        /// <summary>
        /// Récupère les informations d’un joueur spécifique.
        /// </summary>
        /// <param name="id">Identifiant du joueur.</param>
        /// <returns>Un joueur correspondant à l’identifiant spécifié.</returns>
        /// <response code="200">Joueur trouvé et retourné avec succès.</response>
        /// <response code="404">Aucun joueur trouvé avec cet identifiant.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Ajoute un nouveau joueur dans la base.
        /// </summary>
        /// <param name="player">Objet contenant les informations du joueur à ajouter.</param>
        /// <returns>Le joueur ajouté.</returns>
        /// <response code="201">Joueur créé avec succès.</response>
        /// <response code="400">Requête invalide (données manquantes ou invalides).</response>
        [HttpPost]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Calcule et retourne les statistiques globales des joueurs.
        /// </summary>
        /// <remarks>
        /// Les statistiques incluent :
        /// - Le pays ayant le meilleur ratio de victoires  
        /// - L’IMC moyen des joueurs  
        /// - La médiane de la taille des joueurs  
        /// </remarks>
        /// <returns>Un objet contenant les statistiques globales.</returns>
        /// <response code="200">Statistiques calculées avec succès.</response>
        /// <response code="204">Aucune donnée trouvée pour calculer les statistiques.</response>
        [HttpGet("stats")]
        [ProducesResponseType(typeof(PlayerStatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetStatistics()
        {
            var stats = await _playerStatsService.GetStatisticsAsync();

            return Ok(stats);
        }
    }
}
