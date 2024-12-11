using Moq;
using NUnit.Framework;
using System.Linq.Expressions;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data;

namespace TravelAgencyWebApp.Service.Tests
{
	[TestFixture]
	public class AgentServiceTests
	{
		private Mock<IRepository<Agent, Guid>>? _mockAgentRepository;
		private AgentService? _agentService;

		[SetUp]
		public void Setup()
		{
			_mockAgentRepository = new Mock<IRepository<Agent, Guid>>();
			_agentService = new AgentService(_mockAgentRepository.Object);
		}

		[Test]
		public async Task GetByUserIdAsync_ValidUserId_ReturnsAgent()
		{
			// Arrange
			string userId = Guid.NewGuid().ToString();
			var parsedUserId = Guid.Parse(userId);
			var expectedAgent = new Agent { UserId = parsedUserId, IsDeleted = false };

			_mockAgentRepository!.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<Expression<Func<Agent, bool>>>()))
			   .ReturnsAsync(expectedAgent);

			// Act
			var result = await _agentService!.GetByUserIdAsync(userId);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result!.UserId, Is.EqualTo(parsedUserId));
		}

		[Test]
		public async Task GetByUserIdAsync_InvalidUserId_ReturnsNull()
		{
			// Arrange
			string invalidUserId = "invalid-guid";

			// Act
			var result = await _agentService!.GetByUserIdAsync(invalidUserId);

			// Assert
			Assert.That(result, Is.Null);
		}

		[Test]
		public async Task AddAsync_WhenCalled_AddsAgent()
		{
			// Arrange
			var newAgent = new Agent { UserId = Guid.NewGuid() };

			// Act
			await _agentService!.AddAsync(newAgent);

			// Assert
			_mockAgentRepository!.Verify(repo => repo.AddAsync(It.Is<Agent>(a => a == newAgent)), Times.Once);
		}

		[Test]
		public async Task FirstOrDefaultAsync_WhenCalled_ReturnsAgent()
		{
			// Arrange
			var agentPredicate = new Agent { UserId = Guid.NewGuid(), IsDeleted = false };
			Expression<Func<Agent, bool>> predicate = a => a.UserId == agentPredicate.UserId;

			_mockAgentRepository!.Setup(repo => repo.FirstOrDefaultAsync(predicate))
				.ReturnsAsync(agentPredicate);

			// Act
			var result = await _agentService!.FirstOrDefaultAsync(predicate);

			// Assert
			Assert.That(result, Is.EqualTo(agentPredicate));
		}

		[Test]
		public async Task DeleteAsyncHard_ValidAgent_ReturnsTrue()
		{
			// Arrange
			var agentToDelete = new Agent { UserId = Guid.NewGuid() };

			_mockAgentRepository!.Setup(repo => repo.DeleteAsyncHard(agentToDelete))
				.ReturnsAsync(true);

			// Act
			var result = await _agentService!.DeleteAsyncHard(agentToDelete);

			// Assert
			Assert.That(result, Is.True);
			_mockAgentRepository.Verify(repo => repo.DeleteAsyncHard(agentToDelete), Times.Once);
		}

		[Test]
		public void DeleteAsyncHard_NullAgent_ThrowsArgumentNullException()
		{
			// Act & Assert
			Assert.ThrowsAsync<ArgumentNullException>(() => _agentService!.DeleteAsyncHard(null!));
		}
	}
}
	

