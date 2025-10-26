using Moq;
using Tennis.Core.Entities;
using Tennis.Core.Repositories;
using Tennis.Domain.Services.Impl;

namespace Tennis.Tests
{
    [TestFixture]
    public class PlayerServiceTests
    {
        private Mock<IPlayerRepository> _mockRepo;
        private PlayerService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IPlayerRepository>();
            _service = new PlayerService(_mockRepo.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnPlayers()
        {
            var players = new List<Player>
            {
                new Player { Id = 1, Firstname = "Novak", Lastname = "Djokovic" },
                new Player { Id = 2, Firstname = "Rafael", Lastname = "Nadal" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

            var result = await _service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

    }
}
