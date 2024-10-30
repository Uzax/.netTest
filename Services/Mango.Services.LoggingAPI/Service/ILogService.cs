using Mango.Services.LoggingAPI.Models.Dto;

namespace Mango.Services.LoggingAPI.Service{
    
    public interface ILogService
    {

        Task addToLogsFromPublisher(string loginString);

    }
}