using System.ComponentModel.DataAnnotations;

namespace Mango.Services.LoggingAPI.Models
{

    public class Logs
    {
        [Key] 
        [Required]
        public int Id { get; set; }

        [Required]
        public String fromService { get; set; }
        
        [Required]
        public String Message { get; set; }

        [Required]
        public DateTime timeStamp { get; set; }
        
    }
}