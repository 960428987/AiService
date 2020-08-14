using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VoxelCloud.AiServiceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureLogging((context, loggingBuilder) =>
            {
                //loggingBuilder.AddFilter("System", LogLevel.Information);
                //loggingBuilder.AddFilter("Microsoft", LogLevel.Information);
                var path = context.HostingEnvironment.ContentRootPath;
                loggingBuilder.AddLog4Net($"{path}/config/log4net.config");//�����ļ�
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
