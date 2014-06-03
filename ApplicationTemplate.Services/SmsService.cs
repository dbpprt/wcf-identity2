using System.Threading.Tasks;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;

namespace ApplicationTemplate.Services
{
    public class SmsService : IIdentityMessageService, ISmsService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
