using System.Threading.Tasks;
using System.Web.Mvc;
using ApplicationTemplate.Models.DataTransfer;
using ApplicationTemplate.Models.Entities.Identity;
using ApplicationTemplate.ServiceContracts;
using ApplicationTemplate.Web.Identity;

namespace ApplicationTemplate.Web.Controllers
{
    public class HomeController : AsyncController
    {
        private readonly IApplicationUserManager _userManager;
        private readonly IApplicationRoleManager _roleManager;
        private readonly IIdentityService _identityService;

        public HomeController(IApplicationUserManager userManager, IIdentityService identityService, IApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _identityService = identityService;
            _roleManager = roleManager;
        }

        // GET: Home
        public async Task<ActionResult> Index()
        {
            var x = _identityService.CreateTwoFactorRememberBrowserIdentity(5);

            return Content("test");
        }

        const string name = "admin@admin.com";
        const string password = "Admin@123456";
        const string roleName = "Admin";
        
        public async Task<ActionResult> Create()
        {
            

            //Create Role Admin if it does not exist
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                role = new ApplicationRole { Name = roleName };
                await _roleManager.CreateAsync(role);
            }

            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                user = new AppUser { UserName = name, Email = name };
                var createResult = await _userManager.CreateWithPasswordAsync(user, password);
                
            }
            else
            {
                await _userManager.SetLockoutEnabledAsync(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = await _userManager.GetRolesAsync(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                await _userManager.AddToRoleAsync(user.Id, role.Name);
            }

            return Content("test");
        }

        public async Task<ActionResult> Login()
        {
            var result = await _identityService.PasswordSignIn(name, password, true, false);

            return Content("test");
        }
    }
}