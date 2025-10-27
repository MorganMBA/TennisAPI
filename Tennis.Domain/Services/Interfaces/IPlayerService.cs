using Tennis.Core.Dtos;
using Tennis.Core.Entities;

namespace Tennis.Domain.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllAsync();
        Task<PlayerDto?> GetByIdAsync(int id);
        Task AddAsync(PlayerDto player);
    }
}
