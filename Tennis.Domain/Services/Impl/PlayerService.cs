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

        public Task<IEnumerable<PlayerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PlayerDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
