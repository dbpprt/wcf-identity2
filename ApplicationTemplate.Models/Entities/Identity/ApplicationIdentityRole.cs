using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationTemplate.Models.Entities.Identity
{
    public class ApplicationIdentityRole : IdentityRole<int, ApplicationIdentityUserRole>, IRole<int>
    {
        public ApplicationIdentityRole()
        {
        }

        public ApplicationIdentityRole(string name)
        {
            Name = name;
        }
    }
}