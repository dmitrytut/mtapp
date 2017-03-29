using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace MtApp.Data
{
	public class Repository<T> : IRepository<T> where T : BaseEntity, new()
	{
		private SQLiteAsyncConnection db;

		public Repository(SQLiteAsyncConnection db)
		{
			this.db = db;
		}

		public virtual AsyncTableQuery<T> AsQueryable() =>
			db.Table<T>();

		public virtual async Task<List<T>> Get() =>
			await db.Table<T>().ToListAsync().ConfigureAwait(false);

		public virtual async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
		{
			var query = db.Table<T>();

			if (predicate != null)
				query = query.Where(predicate);

			if (orderBy != null)
				query = query.OrderBy<TValue>(orderBy);

			return await query.ToListAsync().ConfigureAwait(false);
		}

		public virtual async Task<T> Get(int id) =>
			 await db.FindAsync<T>(id).ConfigureAwait(false);

		public virtual async Task<T> Get(Expression<Func<T, bool>> predicate) =>
		await db.FindAsync<T>(predicate).ConfigureAwait(false);

		public virtual async Task<int> Insert(T entity) =>
			 await db.InsertAsync(entity).ConfigureAwait(false);

		public virtual async Task<int> Update(T entity) =>
			 await db.UpdateAsync(entity).ConfigureAwait(false);

		public virtual async Task<int> InsertOrUpdate(T entity) => 
			entity.Id == 0 
		          ? await Insert(entity).ConfigureAwait(false) 
			          : await Update(entity).ConfigureAwait(false);

		public virtual async Task<int> Delete(T entity) =>
			 await db.DeleteAsync(entity).ConfigureAwait(false);
	}
}
