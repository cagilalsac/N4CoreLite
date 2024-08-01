using N4CoreLite.Contexts.Bases;
using N4CoreLite.Repositories.Bases;

namespace N4CoreLite.Repositories
{
	public class UnitOfWork : UnitOfWorkBase
	{
		public UnitOfWork(IDb db) : base(db)
		{
		}
	}
}
