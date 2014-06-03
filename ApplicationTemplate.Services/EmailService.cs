using System.Threading.Tasks;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;

namespace ApplicationTemplate.Services
{
    public class EmailService : IIdentityMessageService, IEmailService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}
