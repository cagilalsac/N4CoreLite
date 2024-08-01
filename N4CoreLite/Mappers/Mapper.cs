using N4CoreLite.Mappers.Bases;
using N4CoreLite.Records.Bases;

namespace N4CoreLite.Mappers
{
	public class Mapper<TEntity, TQueryModel, TCommandModel> : MapperBase<TEntity, TQueryModel, TCommandModel>
		where TEntity : Record, new() where TQueryModel : Record, new() where TCommandModel : Record, new()
	{
	}
}
