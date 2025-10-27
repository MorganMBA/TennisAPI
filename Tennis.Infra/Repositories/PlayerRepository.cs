using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Logging;
using Tennis.Core.Entities;
using Tennis.Core.Repositories;

namespace Tennis.Infra.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly DynamoDBContext _dynamoDbContext;
        private readonly ILogger<PlayerRepository> _logger;

        public PlayerRepository(IAmazonDynamoDB dynamoDbContext, ILogger<PlayerRepository> logger)
        {
            _dynamoDbContext = new DynamoDBContext(dynamoDbContext);
            _logger = logger;
        }

        public async Task AddAsync(Player player)
        {
            await _dynamoDbContext.SaveAsync(player);
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _dynamoDbContext.ScanAsync<Player>(new List<ScanCondition>{}).GetRemainingAsync();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _dynamoDbContext.LoadAsync<Player>(id);
        }
    }
}
