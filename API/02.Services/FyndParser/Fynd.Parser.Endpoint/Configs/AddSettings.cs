using Fynd.Parser.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fynd.Parser.Endpoint.Configs
{
    public static  class AddSettingsExtension
    {

        public static  IServiceCollection AddSetting(this IServiceCollection services,IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Settings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            services.AddSingleton<AppSettings>(appSettings);
            return services;
        }
    }
}
