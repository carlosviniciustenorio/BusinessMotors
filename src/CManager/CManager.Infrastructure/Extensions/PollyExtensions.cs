using CManager.Integration.Clients;
using Polly;
using Polly.Retry;

namespace CManager.Infrastructure.Extensions
{
    public static class PollyExtensions
    {
        public static IServiceCollection AddPolly(this IServiceCollection services)
        {
            services.AddSingleton<AsyncPolicy>(CreateWaitAndRetryPolicy(new[]
                        {
                            TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(4), TimeSpan.FromSeconds(7)
                        }));

            //services.AddHttpClient<ICatalogService, CatalogService>(client => client.BaseAddress = new Uri("https://localhost:7001/api/anuncio/getAll?take=10&skip=0"))
            //        .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //        .AddPolicyHandler(GetRetryPolicy());

            services.AddHttpClient<ICatalogService, CatalogService>(client => client.BaseAddress = new Uri("http://googla.com/"));

            return services;
        }

        //public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        //{
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        //        .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));
        //}

        public static AsyncRetryPolicy CreateWaitAndRetryPolicy(IEnumerable<TimeSpan> sleepsBeetweenRetries)
        {
            return Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(
                    sleepDurations: sleepsBeetweenRetries,
                    onRetry: (_, span, retryCount, _) =>
                    {
                        var previousBackgroundColor = Console.BackgroundColor;
                        var previousForegroundColor = Console.ForegroundColor;

                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Out.WriteLineAsync($" ***** {DateTime.Now:HH:mm:ss} | " +
                            $"Retentativa: {retryCount} | " +
                            $"Tempo de Espera em segundos: {span.TotalSeconds} **** ");

                        Console.BackgroundColor = previousBackgroundColor;
                        Console.ForegroundColor = previousForegroundColor;
                    });
        }
    }
}