using System.Linq.Expressions;

namespace TravelAgencyWebApp.Data.Repository.Interfaces
{
    public interface IRepository<TType, TId>
    {
        TType? GetById(TId id);
        Task<TType?> GetByIdAsync(TId id);
        TType? FirstOrDefault(Func<TType, bool> predicate);
        Task<TType?> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate);
        IEnumerable<TType> GetAll();
        Task<IEnumerable<TType>> GetAllAsync();
        IQueryable<TType> Query();
		IQueryable<TType> GetAllAttached();
        void Add(TType item);
        Task AddAsync(TType item);
        void AddRange(TType[] items);
        Task AddRangeAsync(TType[] items);
        bool Delete(TType entity);
        Task<bool> DeleteAsync(TType entity);
        Task<bool> SoftDeleteAsync(TType entity);
        bool Update(TType item);
        Task<bool> UpdateAsync(TType item);
        Task<TType?> GetIncludingAsync(TId id, params Expression<Func<TType, object>>[] includes);
        Task<IEnumerable<TType>> GetAllIncludingAsync(params Expression<Func<TType, object>>[] includes);
        Task<IEnumerable<TType>> GetByUserIdAsync(Guid userId, params Expression<Func<TType, object>>[] includes);
	}
}
