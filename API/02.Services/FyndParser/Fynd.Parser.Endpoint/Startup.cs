using Fynd.Parser.Application.WebRepository;
using Fynd.Parser.ApplicationContract.WebRepository;
using Fynd.Parser.Endpoint.Configs;
using Fynd.Parser.Endpoint.Grpc.Map;
using Fynd.Parser.Endpoint.Grpc.services;
using Fynd.Framework.Core.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Fynd.Parser.ApplicationContract;
using Fynd.Parser.Application;

namespace Fynd.Parser.Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(Configuration.GetSection("Kestrel"));
            services.AddSetting(Configuration);

            services.AddTransient<IDataExtractorService, DataExtractorService>();
           
            services.AddControllers();
            services.AddAutoMapper(typeof(ParserConvertorProfile));
            services.AddGrpc();
        }

  
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<CorsMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<Fynd.Parser.Endpoint.Grpc.services.DataExtractorService>();
            });
           
        }
    }
}
