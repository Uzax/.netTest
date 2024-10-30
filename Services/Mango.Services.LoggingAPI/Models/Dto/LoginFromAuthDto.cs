namespace Mango.Services.LoggingAPI.Models.Dto
{

    public class LoginFromAuthDto
    {
        
        public string Username { get; set; }
        public DateTime Time { get; set; }
        public string serviceName { get; set; }
        public string Message { get; set; }
        public string fromIP { get; set; }
        
    }
}