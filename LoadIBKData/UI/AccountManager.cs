/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */
using LoadIBKData.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LoadIBKData.UI
{
    class AccountManager
    {
        private const int ACCOUNT_ID_BASE = 50000000;

        private const int ACCOUNT_SUMMARY_ID = ACCOUNT_ID_BASE + 1;

        private const string ACCOUNT_SUMMARY_TAGS = "AccountType,NetLiquidation,TotalCashValue,SettledCash,AccruedCash,BuyingPower,EquityWithLoanValue,PreviousEquityWithLoanValue,"
             +"GrossPositionValue,ReqTEquity,ReqTMargin,SMA,InitMarginReq,MaintMarginReq,AvailableFunds,ExcessLiquidity,Cushion,FullInitMarginReq,FullMaintMarginReq,FullAvailableFunds,"
             +"FullExcessLiquidity,LookAheadNextChange,LookAheadInitMarginReq ,LookAheadMaintMarginReq,LookAheadAvailableFunds,LookAheadExcessLiquidity,HighestSeverity,DayTradesRemaining,Leverage";

        private IBClient ibClient;
        private List<string> managedAccounts;

        private bool accountSummaryRequestActive = false;
        private bool accountUpdateRequestActive = false;
        private string currentAccountSubscribedToTupdate;


        public void HandleAccountSummaryEnd()
        {
            accountSummaryRequestActive = false;
        }

        public void RequestPositions()
        {
            ibClient.ClientSocket.reqPositions();
        }

        public void RequestFamilyCodes()
        {
            ibClient.ClientSocket.reqFamilyCodes();
        }

        public IBClient IbClient
        {
            get { return ibClient; }
            set { ibClient = value; }
        }
    }
}
