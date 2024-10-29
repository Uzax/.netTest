using Mango.Services.LoggingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.LoggingAPI.Controller
{
    
    [Route("api/logs")]
    [ApiController]
    public class LogsAPIController : ControllerBase
    {
        private readonly ILogsRepository _logsRepository;


        public LogsAPIController(ILogsRepository logsRepository)
        {
            _logsRepository = logsRepository; 
            
        }

        [HttpGet]
        public async Task<ActionResult> getAllLogs()
        {
            var result = await _logsRepository.GetAllLogs();
            return Ok(result);
        }
        
        
        
    }
}