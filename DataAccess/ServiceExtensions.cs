using DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N4CoreLite.Contexts.Bases;

namespace DataAccess
{
    public static class ServiceExtensions
	{
		public static void ConfigureDataAccess(this WebApplicationBuilder builder)
		{
			// Inversion of Control for DbContext:
			builder.Services.AddDbContext<IDb, Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
		}
	}
}
