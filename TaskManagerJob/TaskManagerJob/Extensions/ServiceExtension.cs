using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerJob.Data;
using TaskManagerJob.Repositories.Implementations;
using TaskManagerJob.Repositories.Interfaces;
using TaskManagerJob.Utilities;

namespace TaskManagerJob.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<Worker>();
            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
            services.AddHttpContextAccessor();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddTransient(typeof(TimeSpan), _ => TimeSpan.FromSeconds(1D));
            var serviceProvider = services.BuildServiceProvider();
            var timespan = (TimeSpan)serviceProvider.GetRequiredService(typeof(TimeSpan));
        }


        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            //services.AddHttpClient<IHttpService, HttpService>().SetHandlerLifetime(TimeSpan.FromMinutes(5)).AddPolicyHandler(PollyPolicy.GetRetryPolicy());
            services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>().SetHandlerLifetime(TimeSpan.FromMinutes(5));
            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlConnection");
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            
            var test = configuration.GetConnectionString("TaskConnection");

        }

    }
}
