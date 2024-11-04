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


        public Task addToLogsFromPublisher(LoginFromAuthDto loginFromAuthDto)
        {

            try
            {
                
                var log = new Logs()
                {
                    fromService = loginFromAuthDto.serviceName,
                    Message = loginFromAuthDto.Username + " --> " + loginFromAuthDto.Message + " --IP---> " + loginFromAuthDto.fromIP,
                    timeStamp = loginFromAuthDto.Time
                };

                _logsRepository.addLog(log);
                _logsRepository.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not add Platform to DB {e.Message}");
            }
           
            
           return Task.CompletedTask;
        }
    }
}