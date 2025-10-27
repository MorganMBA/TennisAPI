using Tennis.Core.Dtos;
using Tennis.Core.Repositories;
using Tennis.Domain.Mapper;
using Tennis.Domain.Services.Interfaces;

namespace Tennis.Domain.Services.Impl
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }
        public async Task AddAsync(PlayerDto playerDto)
        {
            var player = PlayerMapper.ToEntity(playerDto);
            await _playerRepository.AddAsync(player);
        }

        public async Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return players.Select(PlayerMapper.ToDto);
        }

        public async Task<PlayerDto?> GetByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);

            if (player == null)
                return null;

            return PlayerMapper.ToDto(player);
        }
    }
}
