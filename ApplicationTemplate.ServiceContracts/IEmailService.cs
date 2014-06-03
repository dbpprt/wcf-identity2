using System.ServiceModel;
using Microsoft.AspNet.Identity;

namespace ApplicationTemplate.ServiceContracts
{
    [ServiceContract]
    public interface IEmailService : IIdentityMessageService
    {

    }
}
