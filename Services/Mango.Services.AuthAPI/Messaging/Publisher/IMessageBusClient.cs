using Mango.Services.AuthAPI.Models.Dto;

namespace Mango.Services.AuthAPI.Messaging.Publisher
{

    public interface IMessageBusClient
    {

        void publishNewLoginAttempt(LoginRequestPublishDto loginRequestPublishDto);
        

    }
}