using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TravelAgencyWebApp.Data.Models;
using TravelAgencyWebApp.Data.Repository.Interfaces;

namespace TravelAgencyWebApp.Data.Repository
{
	public class Repository<TType, TId>(ApplicationDbContext context) : IRepository<TType, TId> where TType : class
	{
		private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
		private readonly DbSet<TType> _dbSet = context.Set<TType>();


		public TType? GetById(TId id)
		{
			return this._dbSet.Find(id);
		}

		public async Task<TType?> GetByIdAsync(TId id)
		{
			return await this._dbSet.FindAsync(id);
		}

		public TType? FirstOrDefault(Func<TType, bool> predicate)
		{
			return _dbSet.AsEnumerable().FirstOrDefault(predicate);
		}

		public async Task<TType?> FirstOrDefaultAsync(Expression<Func<TType, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		public IEnumerable<TType> GetAll() // ПРОВЕРИ ДАЛИ РАБОТИ ПРАВИЛНО!!!!!
		{
			return this._dbSet.AsEnumerable();
		}

		public async Task<IEnumerable<TType>> GetAllAsync()
		{
			return await this._dbSet.ToListAsync();
		}

		public IQueryable<TType> Query()
		{
			return _dbSet; // This provides an IQueryable for the associated entity
		}

		public IQueryable<TType> GetAllAttached()
		{
			return _dbSet.AsQueryable();
		}

		public void Add(TType item)
		{
			_dbSet.Add(item);
			_context.SaveChanges();
		}

		public async Task AddAsync(TType item)
		{
			await _dbSet.AddAsync(item);
			await _context.SaveChangesAsync();
		}

		public void AddRange(TType[] items)
		{
			_dbSet.AddRange(items);
			_context.SaveChanges();
		}

		public async Task AddRangeAsync(TType[] items)
		{
			await _dbSet.AddRangeAsync(items);
			await _context.SaveChangesAsync();
		}

		public bool Delete(TType entity)
		{
			if (entity == null) return false;
			_dbSet.Remove(entity);
			return _context.SaveChanges() > 0;
		}

		public async Task<bool> DeleteAsync(TType entity)
		{
            ArgumentNullException.ThrowIfNull(entity);
            _dbSet.Remove(entity);
			return await _context.SaveChangesAsync() > 0;
		}

		public bool Update(TType item)
		{
			_dbSet.Update(item);
			return _context.SaveChanges() > 0;
		}

		public async Task<bool> UpdateAsync(TType item)
		{
			_dbSet.Update(item);
			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<TType?> GetIncludingAsync(TId id, params Expression<Func<TType, object>>[] includes)
		{
			IQueryable<TType> query = _dbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.FirstOrDefaultAsync(e => EF.Property<TId>(e, "Id")!.Equals(id));
		}

		public async Task<IEnumerable<TType>> GetAllIncludingAsync(params Expression<Func<TType, object>>[] includes)
		{
			IQueryable<TType> query = _dbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.ToListAsync();
		}
	}
}

