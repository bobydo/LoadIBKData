using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LoadIBKData.Common
{
    public class AppSetting 
    {
        public string Host { get;set; }
        public string Port { get;set; }
        public string ClientId { get;set; }
        public string Currency { get;set; }
        public int Days { get;set; }
        public string Exchange { get;set; }
        public string PrimmaryExch { get;set; }
        public string SecType { get;set; }
        public string Symbol { get; set; }
    }
}
