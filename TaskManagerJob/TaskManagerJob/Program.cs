using Microsoft.AspNetCore.Builder;
using NLog;
using TaskManagerJob;
using TaskManagerJob.Extensions;

namespace TaskManagerJob
{

    public class Program
    {

        public async static Task Main(string[] args)
        {

            Console.WriteLine("Statement of Account, Schedule and Reccurring Payment entry point of this application");

            var builder = WebApplication.CreateBuilder(args);

            //LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            //"\\nlog.config"));


            var configuration = builder.Configuration;

            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            builder.Services.RegisterDbContext(configuration);
            builder.Services.ConfigureServices();
            builder.Services.ConfigureHttpClient();
            var app = builder.Build();

            // Executable Process of the application
            await app.ExecuteProcess();

        }
    }
}