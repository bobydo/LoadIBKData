using LoadIBKData.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using IBApi;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoadIBKData.Service
{
    public class HistoryDataService : IHistoryDataService
    {
        private readonly IOptions<AppSetting> _appSetting;
        private readonly ILogger<HistoryDataService> _logger;
        //private IBClient ibClient;

        public bool IsConnected { get; set; } = false;
        private EReaderMonitorSignal signal = new EReaderMonitorSignal();
        public HistoryDataService(
            ILogger<HistoryDataService> logger
            ,IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting;
            _logger = logger;
        }

        public void GetData()
        {
            _logger.LogInformation("HistoricalData starts");
            CallFistOne();
            //CallAnother();
        }

        private void CallFistOne()
        {
            EWrapperImpl ibClient = new EWrapperImpl();
            // Ending date for the time series
            String strEndDate = "20210509 16:00:00";
            // Amount of time up to the end date
            String strDuration = "1 W";
            // Bar size
            String strBarSize = "1 min";
            // Data type TRADES= OHLC Trades with volume
            String strWhatToShow = "TRADES";
            string days = _appSetting.Value.Days.ToString() + " D";
            int requestId = 1;
            string Host = _appSetting.Value.Host;
            int Port = Int32.Parse(_appSetting.Value.Port);
            int ClientId = Int32.Parse(_appSetting.Value.ClientId);
            ibClient.ClientSocket.eConnect(Host, Port, ClientId);

            EClientSocket clientSocket = ibClient.ClientSocket;

            //Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
            var reader = new EReader(clientSocket, signal);
            reader.Start();
            //Once the messages are in the queue, an additional thread can be created to fetch them
            new Thread(() => { while (clientSocket.IsConnected()) { signal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();


            // Create a new contract to specify the security we are searching for
            Contract contract = new Contract();
            contract.SecType = _appSetting.Value.SecType;
            contract.Symbol = _appSetting.Value.Symbol;
            contract.Exchange = _appSetting.Value.Exchange;
            contract.Currency = _appSetting.Value.Currency;
            contract.PrimaryExch = _appSetting.Value.PrimaryExch;
            // Create a new TagValue List object (for API version 9.71) 
            List<TagValue> historicalDataOptions = new List<TagValue>();

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
            ibClient.ClientSocket.reqHistoricalData(requestId, contract, "", strDuration,
                                                    strBarSize, strWhatToShow, 1, 1, true,
                                                    historicalDataOptions);
            // Pause to review data
            Console.ReadKey();
            // Disconnect from TWS
            ibClient.ClientSocket.eDisconnect();
        }

        private static void CallAnother()
        {
            EWrapperImpl ibClient = new EWrapperImpl();
            ibClient.ClientSocket.eConnect("127.0.0.1", 7496, 0);
            TimeSpan delay = new TimeSpan(0, 0, 15);
            Thread.Sleep(delay);

            Contract contract = new Contract();
            contract.Symbol = "IBM";
            contract.SecType = "STK";
            contract.Exchange = "SMART";
            contract.Currency = "USD";

            List<TagValue> BogusmktDataOptions = new List<TagValue>();

            ibClient.ClientSocket.reqHistoricalData(2, contract, "20210225 17:11:11 GMT",
                "2 D", "1 hour", "BID_ASK", 1, 1, false, BogusmktDataOptions);

            Console.ReadKey();
            ibClient.ClientSocket.cancelHistoricalData(2);
            ibClient.ClientSocket.eDisconnect();
        }
    }
}
