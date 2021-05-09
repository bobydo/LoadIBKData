using System.Threading.Tasks;

namespace LoadIBKData.Service
{
    public interface IHistoryDataService
    {
        Task<string> GetHost();
    }
}