using LoadIBKData.Backend;
using LoadIBKData.Common;
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
        protected IBClient ibClient;

        /// <summary>
        /// https://dfederm.com/building-a-console-app-with-.net-generic-host/
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            return host.RunAsync();
            //int port;
            //string host = ;
            //try
            //{
            //    port = Int32.Parse();
            //    ibClient.ClientId = Int32.Parse(this.clientid_CT.Text);
            //    ibClient.ClientSocket.eConnect(host, port, ibClient.ClientId);

            //    var reader = new EReader(ibClient.ClientSocket, signal);

            //    reader.Start();

            //    new Thread(() => { while (ibClient.ClientSocket.IsConnected()) { signal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();
            //}
            //catch (Exception)
            //{
            //    HandleErrorMessage(new ErrorMessage(-1, -1, "Please check your connection attributes."));
            //}
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureServices((_, services) =>
                    {
                        services.AddSingleton<IConfigurationFactory, ConfigurationFactory>()
                                .AddHostedService<ConsoleHostedService>();
                    });
        internal sealed class ConsoleHostedService : IHostedService
        {
            private int? _exitCode;
            private readonly ILogger _logger;
            private readonly IHostApplicationLifetime _appLifetime;
            private readonly IConfigurationFactory _configurationFactory;

            public ConsoleHostedService(
                ILogger<ConsoleHostedService> logger,
                IHostApplicationLifetime appLifetime,
                IConfigurationFactory configurationFactory)
            {
                _logger = logger;
                _appLifetime = appLifetime;
                _configurationFactory = configurationFactory;
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
                            _logger.LogInformation("Hello World!");
                            _logger.LogInformation(_configurationFactory.Host);
                            _exitCode = 0;
                            await Task.Delay(1000);
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
