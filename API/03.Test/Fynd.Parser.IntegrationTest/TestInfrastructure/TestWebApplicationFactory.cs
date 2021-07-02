
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
 

namespace Parser.IntegrationTest.Infrastructure
{
    public class TestWebApplicationFactory<TStartup>
                  : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IHostBuilder CreateHostBuilder() =>
            base.CreateHostBuilder().UseEnvironment("Staging");

        public ServiceProvider ServiceProvider { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                ServiceProvider = services.BuildServiceProvider();
            });
        }

    }
}
