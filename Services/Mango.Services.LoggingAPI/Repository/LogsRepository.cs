using Mango.Services.LoggingAPI.Data;
using Mango.Services.LoggingAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace Mango.Services.LoggingAPI.Repository
{

    public class LogsRepository : ILogsRepository
    {

        private readonly AppDbContext _context;
        
        public LogsRepository(AppDbContext context)
        {
            _context = context; 
        }
        
        

        public async Task<IEnumerable<Logs>> GetAllLogs()
        {
            var result = await _context.Logs.ToListAsync();
            return result; 
        }

        public async Task<Logs> getLogById(int id)
        {
            return await _context.Logs.FirstOrDefaultAsync(l => l.Id == id);
        }

        public Task addLog(Logs log)
        {
            _context.Logs.Add(log);
            return Task.CompletedTask;
        }

        public Task deleteLog(Logs log)
        {
            _context.Logs.Remove(log);
            return Task.CompletedTask;
        }


        private bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0); 
        }
    }
}