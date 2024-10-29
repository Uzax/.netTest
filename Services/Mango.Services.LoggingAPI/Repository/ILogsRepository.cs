using Mango.Services.LoggingAPI.Models;

namespace Mango.Services.LoggingAPI.Repository
{
    public interface ILogsRepository
    {
        Task<IEnumerable<Logs>> GetAllLogs();

        Task<Logs> getLogById(int id);

        Task addLog(Logs log);

        Task deleteLog(Logs log); 

    }
}