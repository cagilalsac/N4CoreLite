#nullable disable

using N4CoreLite.Settings.Bases;

namespace Web.Settings
{
	public class AppSettings : AppSettingsBase
	{
        public string Title { get; set; }

        public AppSettings(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) : base(configuration, webHostEnvironment)
		{
		}

        public AppSettings() : base(default, default)
        {
        }
    }
}
