using Business.Models;
using Business.Services;
using Business.Services.Reports;
using Business.Services.Reports.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N4CoreLite.Services.Bases;

namespace Business
{
    public static class ServiceExtensions
	{
		public static void ConfigureBusiness(this WebApplicationBuilder builder)
		{
			// Inversion of Control for services:
			builder.Services.AddScoped(typeof(ServiceBase<Ders, DersQueryModel, DersCommandModel>), typeof(DersService));
			builder.Services.AddScoped(typeof(ServiceBase<Sinif, SinifQueryModel, SinifCommandModel>), typeof(SinifService));
			builder.Services.AddScoped(typeof(ServiceBase<Ogrenci, OgrenciQueryModel, OgrenciCommandModel>), typeof(OgrenciService));
			builder.Services.AddScoped<IOgrenciRaporuService, OgrenciRaporuService>();

			// Inversion of Control for stored procedures:
			builder.Services.AddScoped<IDbProcedures, DbProcedures>();
		}
	}
}
