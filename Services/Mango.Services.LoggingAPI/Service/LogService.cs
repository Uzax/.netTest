using Mango.Services.LoggingAPI.Models;
using Mango.Services.LoggingAPI.Models.Dto;
using Mango.Services.LoggingAPI.Repository;

namespace Mango.Services.LoggingAPI.Service
{

    public class LogService : ILogService
    {

        private readonly ILogsRepository _logsRepository; 
        public LogService( ILogsRepository logsRepository)
        {

            _logsRepository = logsRepository; 
        }


        public Task addToLogsFromPublisher(string loginString)
        {
            Console.WriteLine($"----Recived Message from MQ ---> {loginString}");
            
            
            // var loginFromAuthDto = new LoginFromAuthDto()
            // {
            //     Username = loginString
            //     
            // }
            // var log = new Logs()
            // {
            //     fromService = loginFromAuthDto.serviceName,
            //     Message = loginFromAuthDto.Username + " --> " + loginFromAuthDto.Message + " --IP---> " + loginFromAuthDto.fromIP,
            //     timeStamp = loginFromAuthDto.Time
            // };
            
            throw new NotImplementedException();
        }
    }
}