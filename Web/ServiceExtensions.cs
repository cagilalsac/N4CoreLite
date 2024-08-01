using N4CoreLite.Settings.Bases;
using Web.Settings;

namespace Web
{
    public static class ServiceExtensions
	{
		public static void ConfigureWeb(this WebApplicationBuilder builder)
		{
			// Inversion of Control for services:
			builder.Services.AddSingleton<AppSettingsBase, AppSettings>();
        }
    }
}
