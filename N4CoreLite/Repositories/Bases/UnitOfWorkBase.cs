using N4CoreLite.Contexts.Bases;

namespace N4CoreLite.Repositories.Bases
{
	public abstract class UnitOfWorkBase : IDisposable
	{
		private readonly IDb _db;

		protected UnitOfWorkBase(IDb db)
		{
			_db = db;
		}

		public virtual async Task<int> SaveAsync(CancellationToken cancellationToken = default)
		{
			return await _db.SaveChangesAsync(cancellationToken);
		}

		public void Dispose()
		{
			_db?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
