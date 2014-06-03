using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationTemplate.Models.Entities.Identity
{
    public class ApplicationIdentityUser :
        IdentityUser<int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
    {
    }
}