using LoadIBKData.Common;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LoadIBKData.Service
{
    public class HistoryDataService : IHistoryDataService
    {
        private readonly IOptions<AppSetting> _appSetting;
        public HistoryDataService(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting;
        }

        public Task<string> GetHost()
        {
            return Task.FromResult(_appSetting.Value.Host.ToString());
        }
    }
}
