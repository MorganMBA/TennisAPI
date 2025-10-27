using Microsoft.AspNetCore.Mvc;
using Moq;
using Tennis.API.Controllers;
using Tennis.Core.Dtos;
using Tennis.Domain.Services.Interfaces;

namespace Tennis.Tests
{
    [TestFixture]
    public class PlayersControllerTests
    {
        private Mock<IPlayerService> _mockService;
        private PlayersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IPlayerService>();
            _controller = new PlayersController(_mockService.Object);
        }

        [Test]
        public async Task GetPlayers_ShouldReturnOk_WhenPlayersExist()
        {
            var players = new List<PlayerDto>
            {
                new PlayerDto { Id = 1, Firstname = "Roger", Lastname = "Federer" }
            }.AsEnumerable();
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(players);

            var response = await _controller.GetPlayers();

            Assert.That(response, Is.InstanceOf<OkObjectResult>());
            var ok = response as OkObjectResult;
            Assert.That(ok!.Value, Is.EqualTo(players));
        }

        [Test]
        public async Task GetPlayerById_ShouldReturnNotFound_WhenNoPlayer()
        {
            _mockService.Setup(s => s.GetByIdAsync(42)).ReturnsAsync((PlayerDto?)null);

            var response = await _controller.GetPlayerById(42);

            Assert.That(response, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetPlayerById_ShouldReturnOk_WhenPlayerExists()
        {
            var player = new PlayerDto { Id = 7, Firstname = "Djokovic", Lastname = "Novak" };
            _mockService.Setup(s => s.GetByIdAsync(7)).ReturnsAsync(player);

            var response = await _controller.GetPlayerById(7);

            var ok = response as OkObjectResult;
            Assert.That(ok, Is.Not.Null);
            Assert.That(ok!.Value, Is.EqualTo(player));
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}
