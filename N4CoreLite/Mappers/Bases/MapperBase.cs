#nullable disable

using AutoMapper;
using N4CoreLite.Records.Bases;

namespace N4CoreLite.Mappers.Bases
{
	public abstract class MapperBase<TEntity, TQueryModel, TCommandModel> where TEntity : Record, new() where TQueryModel : Record, new() where TCommandModel : Record, new()
	{
        public MapperConfiguration Configuration { get; protected set; }

        protected Profile[] _profiles;

        protected MapperBase()
        {
            Configuration = new MapperConfiguration(Configure);
        }

        private void Configure(IMapperConfigurationExpression mapperConfigurationExpression)
        {
			mapperConfigurationExpression.CreateMap(typeof(TEntity), typeof(TQueryModel));
			mapperConfigurationExpression.CreateMap(typeof(TCommandModel), typeof(TEntity));
			mapperConfigurationExpression.CreateMap(typeof(TEntity), typeof(TCommandModel));
            if (_profiles is not null)
                mapperConfigurationExpression.AddProfiles(_profiles);
		}

        public void Set(params Profile[] profiles)
        {
            _profiles = profiles;
            Configuration = new MapperConfiguration(Configure);
        }

        public virtual TEntity Map(TCommandModel commandModel, TEntity entity = null)
        {
            MapperConfiguration configuration = new MapperConfiguration(c =>
            {
                c.CreateMap(commandModel.GetType(), typeof(TEntity));
                if (_profiles is not null)
                    c.AddProfiles(_profiles);
            });
            Mapper mapper = new Mapper(configuration);
            entity = entity ?? new TEntity();
            mapper.Map(commandModel, entity);
            return entity;
        }

        public virtual TCommandModel Map(TEntity entity)
        {
            MapperConfiguration configuration = new MapperConfiguration(c =>
            {
                c.CreateMap(entity.GetType(), typeof(TCommandModel));
                if (_profiles is not null)
                    c.AddProfiles(_profiles);
            });
            Mapper mapper = new Mapper(configuration);
            return mapper.Map<TCommandModel>(entity);
        }
    }
}
