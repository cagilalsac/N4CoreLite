using N4CoreLite.Mappers;
using N4CoreLite.Mappers.Bases;
using N4CoreLite.Repositories;
using N4CoreLite.Repositories.Bases;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N4CoreLite.Settings.Bases;

namespace N4CoreLite
{
	public static class ServiceExtensions
	{
		public static void ConfigureCore(this WebApplicationBuilder builder)
		{
			// Inversion of Control for HttpContext:
			builder.Services.AddHttpContextAccessor();

            // Inversion of Control for repositories:
            builder.Services.AddScoped(typeof(RepoBase<>), typeof(Repo<>));
            builder.Services.AddScoped<UnitOfWorkBase, UnitOfWork>();

			// Inversion of Control for mappers:
			builder.Services.AddSingleton(typeof(MapperBase<,,>), typeof(Mapper<,,>));
		}

		public static void ConfigureCore(this WebApplication application)
		{
            #region App Settings
            var appSettingsBase = application.Services.GetRequiredService<AppSettingsBase>();
            appSettingsBase.Bind();
            #endregion
        }
    }
}
