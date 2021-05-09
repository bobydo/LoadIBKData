using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace LoadIBKData.Common
{
    public class ConfigurationFactory : IConfigurationFactory
    {
        public string Host => ConfigurationManager.AppSettings["apiHostUrl"];
        public string Port => ConfigurationManager.AppSettings["userName"];
        public string ClientId => ConfigurationManager.AppSettings["password"];
        public string SendingEmailAddress => ConfigurationManager.AppSettings["SendingEmailAddress"];
        public string RecipientEmailAddress => ConfigurationManager.AppSettings["RecipientEmailAddress"];
        public string Symbol => ConfigurationManager.AppSettings["Symbol"];
        public string SecType => ConfigurationManager.AppSettings["SecType"];
        public string Currency => ConfigurationManager.AppSettings["USD"];
        public string Exchange => ConfigurationManager.AppSettings["Exchange"];
        public string PrimmaryExch => ConfigurationManager.AppSettings["PrimmaryExch"];
        public int Days => int.Parse(ConfigurationManager.AppSettings["Days"]);
    }
}
