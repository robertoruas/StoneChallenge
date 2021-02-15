using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UsageCalculation.Service;
using UsageCalculation.Service.Interface;

namespace UsageCalculation.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();


                    var configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddEnvironmentVariables()
                                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                                                .Build();

                    services.Configure<ConfigHelper>(configuration.GetSection(nameof(ConfigHelper)));

                    services.AddSingleton<IConfigHelper>(sp => sp.GetRequiredService<IOptions<ConfigHelper>>().Value);

                    services.AddSingleton<IWorkerService, WorkerService>();
                    services.AddSingleton<ICustomerHelperService, CustomerHelperService>();
                    services.AddSingleton<IChargeHelperService, ChargeHelperService>();
                });
    }
}
