using Moq;
using Tennis.Core.Dtos;
using Tennis.Core.Entities;
using Tennis.Core.Repositories;
using Tennis.Domain.Services.Impl;

namespace Tennis.Tests
{
    [TestFixture]
    public class PlayerStatsServiceTests
    {
        private Mock<IPlayerRepository> _mockRepo;
        private PlayerStatsService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPlayerRepository>();
            _service = new PlayerStatsService(_mockRepo.Object);
        }

        [Test]
        public async Task CalculateStats_ShouldReturnCorrectStats()
        {
            var players = new List<Player>
            {
                new Player
                {
                    Country =  new Country { Code = "FRA" },
                    Data = new PlayerData { Rank = 1, Points = 100, Weight = 80, Height = 180, Age = 25, Last = new List<int> { 1, 1, 0, 1, 1 } }
                },
                new Player
                {
                    Country = new Country { Code = "USA" },
                    Data = new PlayerData { Rank = 2, Points = 90, Weight = 75, Height = 185, Age = 30, Last = new List<int> { 0, 1, 0, 0, 1 } }
                }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);
            var result = await _service.GetStatisticsAsync();

            Assert.That(result.MedianHeight, Is.EqualTo(182.5));
            Assert.That(result.BestCountry, Is.EqualTo("FRA"));
        }
    }
}
