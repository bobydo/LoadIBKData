﻿/* Copyright (C) 2019 Interactive Brokers LLC. All rights reserved. This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace IBApi
{
	/**
     * @class Bar
     * @brief The historical data bar's description.
     * @sa EClient, EWrapper
     */
    public class Bar
    {
        [JsonConstructor]
        public Bar(string time, double open, double high, double low, double close, long volume, int count, double wap)
        {
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            WAP = wap;
            Count = count;
        }
		
		/**
         * @brief The bar's date and time (either as a yyyymmss hh:mm:ss formatted string or as system time according to the request). Time zone is the TWS time zone chosen on login. 
         */
        public virtual string Time { get; private set; }

        /**
         * @brief The bar's open price
         */
        public double Open { get; private set; }
		
		/**
         * @brief The bar's high price
         */
        public double High { get; private set; }
		
		/**
         * @brief The bar's low price
         */
        public double Low { get; private set; }
		
		/**
         * @brief The bar's close price
         */
        public double Close { get; private set; }
		
		/**
         * @brief The bar's traded volume if available (only available for TRADES) 
         */
        public long Volume { get; private set; }
		
		/**
         * @brief The bar's Weighted Average Price (only available for TRADES) 
         */
        public double WAP { get; private set; }
		
		/**
         * @brief The number of trades during the bar's timespan (only available for TRADES) 
         */
        public int Count { get; private set; }
    }
}
