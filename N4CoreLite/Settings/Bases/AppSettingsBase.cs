using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace N4CoreLite.Settings.Bases
{
	public class AppSettingsBase
	{
		public string Name { get; protected set; } = "AppSettings";

        protected readonly IConfiguration _configuration;
		protected readonly IWebHostEnvironment _webHostEnvironment;

		public AppSettingsBase(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
		{
			_configuration = configuration;
			_webHostEnvironment = webHostEnvironment;
		}

		public virtual AppSettingsBase Bind()
		{
			_configuration.GetSection(Name).Bind(this);
			return this;
		}
	}
}
