using Dealeron.SalesTaxes.Application.Common.Interfaces;
using Dealeron.SalesTaxes.Infrastructure.Persistence;
using Dealeron.SalesTaxes.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Dealeron.SalesTaxes.Infrastructure
{
    public static class ConfigueServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(ISalesTaxesRepository<>), typeof(SaleTaxesRepository<>));
            services.AddTransient<ISaleTaxesContext, SaleTaxesContext>();
            services.AddDbContext<SaleTaxesContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("SaleTaxesDb"));
            });
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IBillingService, BillingService>();
            return services;
        }
    }
}