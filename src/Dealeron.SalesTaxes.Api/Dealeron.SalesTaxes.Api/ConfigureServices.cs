using System.Reflection;

namespace Dealeron.SalesTaxes.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddLogging(x => x.AddConsole()
            .SetMinimumLevel(LogLevel.Debug));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
