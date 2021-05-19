using System.Threading.Tasks;

namespace LoadIBKData.Service
{
    public interface IHistoryDataService
    {
        bool IsConnected { get; set; }

        Task GetDataAsync();
    }
}