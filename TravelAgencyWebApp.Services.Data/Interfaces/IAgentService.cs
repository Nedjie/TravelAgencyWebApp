using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyWebApp.Data.Models;

namespace TravelAgencyWebApp.Services.Data.Interfaces
{
	public interface IAgentService
	{
		Task<Agent?> GetByUserIdAsync(string userId);
		Task AddAsync(Agent agent);
		Task<Agent?> FirstOrDefaultAsync(Expression<Func<Agent, bool>> predicate);
		Task<bool> DeleteAsyncHard(Agent agent);
	}
}
