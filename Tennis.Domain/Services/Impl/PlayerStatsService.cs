using Tennis.Core.Dtos;
using Tennis.Core.Repositories;
using Tennis.Domain.Services.Interfaces;

namespace Tennis.Domain.Services.Impl
{
    public class PlayerStatsService : IPlayerStatsService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerStatsService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        
        public async Task<PlayerStatsDto> GetStatisticsAsync()
        {
            var players = await _playerRepository.GetAllAsync();

            if (players == null || !players.Any())
                return new PlayerStatsDto();

            //filtrer pour avoir que les joueurs de taille et poids positifs
            var validPlayers = players
                .Where(p => p.Data.Height > 0 && p.Data.Weight > 0)
                .ToList();

            var countryRatios = players
                .GroupBy(p => p.Country.Code)
                .Select(g => new
                {
                    Country = g.Key,
                    Ratio = g.SelectMany(p => p.Data.Last).DefaultIfEmpty().Average()
                })
                .OrderByDescending(x => x.Ratio)
                .First();

            var averageImc = validPlayers.Average(p =>
            {
                var weightKg = p.Data.Weight / 1000.0;  //  g → kg
                var heightM = p.Data.Height / 100.0;    //  cm → m
                return weightKg / Math.Pow(heightM, 2);
            });
            double medianHeight = CalculateMedianHeight(validPlayers);

            return new PlayerStatsDto
            {
                BestCountry = countryRatios.Country,
                AverageImc = Math.Round(averageImc, 2),
                MedianHeight = Math.Round(medianHeight, 2)
            };
        }

        private double CalculateMedianHeight(IEnumerable<Core.Entities.Player> players)
        {
            var sortedHeights = players
                            .Select(p => p.Data.Height)
                            .OrderBy(h => h)
                            .ToList();

            double medianHeight;
            int count = sortedHeights.Count;

            if (count == 0)
            {
                medianHeight = 0;
            }
            else if (count % 2 == 1)
            {
                medianHeight = sortedHeights[count / 2];
            }
            else
            {
                medianHeight = (sortedHeights[(count / 2) - 1] + sortedHeights[count / 2]) / 2.0;
            }

            return medianHeight;
        }
    }
}
