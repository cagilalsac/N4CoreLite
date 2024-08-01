using Microsoft.AspNetCore.Http;
using N4CoreLite.Contexts.Bases;
using N4CoreLite.Records.Bases;
using N4CoreLite.Repositories.Bases;

namespace N4CoreLite.Repositories
{
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : Record, new()
	{
		public Repo(IDb db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor)
		{
		}
    }
}
