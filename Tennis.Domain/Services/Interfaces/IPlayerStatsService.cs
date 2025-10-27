using Tennis.Core.Dtos;

namespace Tennis.Domain.Services.Interfaces
{
    public interface IPlayerStatsService
    {
        Task<PlayerStatsDto> GetStatisticsAsync();
    }
}
