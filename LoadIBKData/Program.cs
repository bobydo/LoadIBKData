using LoadIBKData.Common;
using LoadIBKData.Data;
using LoadIBKData.Entities;
using LoadIBKData.Repository;
using LoadIBKData.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoadIBKData
{
    class Program
    {
        /// <summary>
        /// https://dfederm.com/building-a-console-app-with-.net-generic-host/
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            return host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureLogging((hostContext, logging)=>{
                        logging.ClearProviders();
                        logging.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                        //https://github.com/serilog/serilog-extensions-logging-file
                        logging.AddFile(hostContext.Configuration.GetSection("Logging"));
                        //appsettings.Development.json has higher priority
                        //logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); 
                        logging.AddDebug(); // only show up on debug output
                        logging.AddConsole(); // only show up on console
                        //EventSource,EventLog,TraceSource,AzureAppServiceFile,AzureAppServiceBlog,ApplicationInsights
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHostedService<ConsoleHostedService>()
                                .AddSingleton<IHistoryDataService, HistoryDataService<Price>>()
                                .AddSingleton<IHistoricPriceUnitOfWork, HistoricPriceUnitOfWork>()
                                .AddSingleton<IGenericRepository<Price>, GenericRepository<Price>>();
                        services.AddOptions<AppSetting>().Bind(hostContext.Configuration.GetSection("AppSetting"));
                        var connstring = hostContext.Configuration["ConnectionStrings:MyConnection"];
                        services.AddDbContext<APIDbContext>(options =>
                            options.UseSqlServer(connstring));
                        //services.AddScoped<EFCorePriceRepository>();
                    });
        internal sealed class ConsoleHostedService : IHostedService
        {
            private int? _exitCode;
            private readonly ILogger _logger;
            private readonly IHostApplicationLifetime _appLifetime;
            private readonly IHistoryDataService _historyDataService;

            public ConsoleHostedService(
                ILogger<ConsoleHostedService> logger,
                IHostApplicationLifetime appLifetime,
                IHistoryDataService historyDataService)
            {
                _logger = logger;
                _appLifetime = appLifetime;
                _historyDataService = historyDataService;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

                _appLifetime.ApplicationStarted.Register(() =>
                {
                    Task.Run(async () =>
                    {
                        try
                        {
                            await _historyDataService.GetDataAsync();
                            _exitCode = 0;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Unhandled exception!");
                        }
                        finally
                        {
                            // Stop the application once the work is done
                            _appLifetime.StopApplication();
                        }
                    });
                });

                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                _logger.LogDebug($"Exiting with return code: {_exitCode}");
                // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
                Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
                return Task.CompletedTask;
            }
        }
    }
}
