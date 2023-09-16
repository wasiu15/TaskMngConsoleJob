using Microsoft.AspNetCore.Builder;
using NLog;
using TaskManagerJob.Extensions;

namespace TaskManagerJob
{

    public class Program
    {

        public async static Task Main(string[] args)
        {

            Console.WriteLine("=========================================");
            Console.WriteLine(" TASK MANAGEMENT CONSOLE JOB ENTRY POINT");
            Console.WriteLine(" TASK MANAGEMENT CONSOLE JOB ENTRY POINT");
            Console.WriteLine("=========================================");

            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
            "\\nlog.config"));


            var configuration = builder.Configuration;
            
            //  REGISTING OUR SERVICES
            builder.Services.RegisterDbContext(configuration);
            builder.Services.ConfigureServices();
            builder.Services.ConfigureHttpClient();
            var app = builder.Build();

            // EXECUTABLE PROCESS OF THE APPLICATION
            await app.ExecuteProcess();

            Console.WriteLine("===========================================");
            Console.WriteLine(" TASK MANAGEMENT CONSOLE JOB EXITING POINT");
            Console.WriteLine(" TASK MANAGEMENT CONSOLE JOB EXITING POINT");
            Console.WriteLine("===========================================");

        }
    }
}