using Moq;
using Tennis.Core.Dtos;
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

        [Test]
        public async Task GetByIdAsync_ShouldReturnPlayer_WhenExists()
        {
            var player = new Player { Id = 1, Firstname = "Serena", Lastname = "Williams" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);

            var result = await _service.GetByIdAsync(1);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Firstname, Is.EqualTo("Serena"));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Player?)null);

            var result = await _service.GetByIdAsync(99);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddAsync_ShouldCallRepositoryOnce()
        {
            var newPlayer = new PlayerDto { Id = 10, Firstname = "Venus", Lastname = "Williams" };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Player>())).Returns(Task.CompletedTask);

            await _service.AddAsync(newPlayer);

            _mockRepo.Verify(r => r.AddAsync(It.Is<Player>(p => p.Id == 10)), Times.Once);
            _mockRepo.Verify(r => r.AddAsync(It.Is<Player>(p => p.Firstname.Contains("Venus"))), Times.Once);
        }
    }
}
