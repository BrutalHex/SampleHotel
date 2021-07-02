using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fynd.Framework.Core.Domain.Configuration
{
    public static class HttpClientExtension
    {

        public static void AddHttpClient<TIRepository, TRepository>(this IServiceCollection services)
           where TIRepository : class
            where TRepository : class, TIRepository
        {
            services.AddHttpClient<TIRepository, TRepository>(client =>
            {
                client.BaseAddress = new Uri(string.Empty);
            })
                        .AddPolicyHandler(GetRetryPolicy())
              .AddPolicyHandler(GetCircuitBreakerPolicy())
              .SetHandlerLifetime(
                 TimeSpan.FromMinutes(2)
               );
        }


        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            Random jitterer = new Random();
            var retryWithJitterPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(6,    
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                                  + TimeSpan.FromMilliseconds(jitterer.Next(0, 100))
                );


            return retryWithJitterPolicy;
        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(20));
        }
    }
}
