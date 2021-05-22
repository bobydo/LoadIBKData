using LoadIBKData.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using IBApi;
using System.Collections.Generic;
using System.Threading;
using LoadIBKData.Repository;
using Pirce = LoadIBKData.Entities.Price;
using LoadIBKData.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace LoadIBKData.Service
{
    public class HistoryDataService<Price> : IHistoryDataService
    {
        private readonly IOptions<AppSetting> _appSetting;
        private readonly ILogger<HistoryDataService<Price>> _logger;
        private readonly APIDbContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public bool IsConnected { get; set; } = false;
        private EReaderMonitorSignal signal = new EReaderMonitorSignal();
        public HistoryDataService(
            ILogger<HistoryDataService<Price>> logger
            , IOptions<AppSetting> appSetting
            , APIDbContext context
            , IServiceScopeFactory serviceScopeFactory)
        {
            _appSetting = appSetting;
            _logger = logger;
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task GetDataAsync()
        {
            _logger.LogInformation("HistoricalData starts");
            await CallFistOneAsync();
        }

        private async Task CallFistOneAsync()
        {
            EWrapperImpl ibClient = new EWrapperImpl(_context);
            // Amount of time up to the end date
            // Bar size
            String strBarSize = _appSetting.Value.strBarSize.ToString();
            // Data type TRADES= OHLC Trades with volume
            String strWhatToShow = "TRADES";
            string strDuration = _appSetting.Value.Days.ToString() + " D";
            int requestId = 1;
            string Host = _appSetting.Value.Host;
            int Port = Int32.Parse(_appSetting.Value.Port);
            int ClientId = Int32.Parse(_appSetting.Value.ClientId);
            ibClient.ClientSocket.eConnect(Host, Port, ClientId);


            // https://interactivebrokers.github.io/tws-api/connection.html
            EClientSocket clientSocket = ibClient.ClientSocket;

            //Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
            var reader = new EReader(clientSocket, signal);
            reader.Start();
            //Once the messages are in the queue, an additional thread can be created to fetch them
            new Thread(() => { while (clientSocket.IsConnected()) { signal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();


            // Now call reqHistoricalData with parameters:
            // tickerId    - A unique identifer for the request
            // Contract    - The security being retrieved
            // endDateTime - The ending date and time for the request
            // durationStr - The duration of dates/time for the request
            // barSize     - The size of each data bar
            // WhatToShow  - Te data type such as TRADES
            // useRTH      - 1 = Use Real Time history
            // formatDate  - 3 = Date format YYYYMMDD
            // historicalDataOptions 
            string endDate = _appSetting.Value.endDate.ToString();
            string symbol = _appSetting.Value.Symbol;
            // Create a new contract to specify the security we are searching for
            Contract contract = new Contract();
            contract.SecType = _appSetting.Value.SecType;
            contract.Symbol = symbol;
            contract.Exchange = _appSetting.Value.Exchange;
            contract.Currency = _appSetting.Value.Currency;
            contract.PrimaryExch = _appSetting.Value.PrimaryExch;
            // Create a new TagValue List object (for API version 9.71) 
            List<TagValue> historicalDataOptions = new List<TagValue>();
            ibClient.Symbol = symbol;
            ibClient.ClientSocket.reqHistoricalData(requestId++, contract, endDate, strDuration,
                                                    strBarSize, strWhatToShow, 1, 1, false,
                                                    historicalDataOptions);
            //https://medium.com/net-core/repository-pattern-implementation-in-asp-net-core-21e01c6664d7

            Console.ReadKey();
            // Disconnect from TWS
            ibClient.ClientSocket.eDisconnect();
        }
    }
}
