using LoadIBKData.Backend;
using LoadIBKData.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace LoadIBKData
{
    class Program
    {
        protected IBClient ibClient;
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
                        services.AddSingleton<IConfigurationFactory, ConfigurationFactory>()
                                .AddTransient<OperationLogger>());

        static void ExemplifyScoping(IServiceProvider services, string scope)
        {
            //using IServiceScope serviceScope = services.CreateScope();
            //IServiceProvider provider = serviceScope.ServiceProvider;

            //OperationLogger logger = provider.GetRequiredService<OperationLogger>();
            //logger.LogOperations($"{scope}-Call 1 .GetRequiredService<OperationLogger>()");

            //Console.WriteLine("...");

            //logger = provider.GetRequiredService<OperationLogger>();
            //logger.LogOperations($"{scope}-Call 2 .GetRequiredService<OperationLogger>()");

            Console.WriteLine();
        }
    }
}
