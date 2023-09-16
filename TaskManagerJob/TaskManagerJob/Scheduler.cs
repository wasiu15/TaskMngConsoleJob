using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerJob
{
    public static class Scheduler
    {

        public async static Task ExecuteProcess(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var worker = scope.ServiceProvider.GetService<Worker>();
            await worker.ExecuteProcessAsync();
        }


    }
}
