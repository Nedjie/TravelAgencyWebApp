using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAgencyWebApp.Data;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;
using TravelAgencyWebApp.Services.Data.Interfaces;

namespace TravelAgencyWebApp.Services.Data
{
	public class AgentService:IAgentService
	{
		private readonly IRepository<Agent, Guid> _agentRepository;

		public AgentService(IRepository<Agent, Guid> agentRepository)
		{
			_agentRepository = agentRepository;
		}

		public async Task<Agent?> GetByUserIdAsync(string userId)
		{
            if (Guid.TryParse(userId, out Guid parsedUserId))
            {
                return await _agentRepository.FirstOrDefaultAsync(a => a.UserId == parsedUserId && !a.IsDeleted);
            }
            else
            {
                Console.WriteLine($"Invalid UserId format: {userId}");
                return null;
            }
        }

		public async Task AddAsync(Agent agent)
		{
			await _agentRepository.AddAsync(agent);
		}

		public async Task<Agent?> FirstOrDefaultAsync(Expression<Func<Agent, bool>> predicate)
		{
			return await _agentRepository.FirstOrDefaultAsync(predicate);
		}

		public async Task<bool> DeleteAsyncHard(Agent agent)
		{
			if (agent == null) throw new ArgumentNullException(nameof(agent));

			return await _agentRepository.DeleteAsyncHard(agent);
		}
	}
}

