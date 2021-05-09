using LoadIBKData.Backend;
using LoadIBKData.Common;
using LoadIBKData.Connection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace LoadIBKData.Service
{
    public class HistoryDataService : IHistoryDataService
    {
        private readonly IOptions<AppSetting> _appSetting;
        private readonly ILogger<HistoryDataService> _logger;
        private IBClient ibClient;
        private bool isConnected = false;
        private EReaderMonitorSignal signal = new EReaderMonitorSignal();
        public HistoryDataService(
            ILogger<HistoryDataService> logger
            ,IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting;
            _logger = logger;
        }

        public Task<string> GetData()
        {
            Connect();
            return Task.FromResult(_appSetting.Value.Host.ToString());
        }
        #region private
        private void Connect()
        {
            try
            {
                _logger.LogInformation("Connecting to server");
                string Host = _appSetting.Value.Host;
                int Port = Int32.Parse(_appSetting.Value.Port);
                ibClient = new IBClient(signal);
                ibClient.ClientId = Int32.Parse(_appSetting.Value.ClientId);
                ibClient.ClientSocket.eConnect(Host, Port, ibClient.ClientId);

                var reader = new EReader(ibClient.ClientSocket, signal);
                reader.Start();
                isConnected = true;
                _logger.LogInformation("Connected to server");
            }
            catch (Exception ex)
            {
                string error = "Please check your connection attributes.";
            }
        }
        #endregion
    }
}
