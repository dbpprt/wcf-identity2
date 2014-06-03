using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;

namespace ApplicationTemplate.Web.Identity
{
    public interface IIdentityService
    {
        void Challenge(string redirectUri, string xsrfKey, int? userId, params string[] authenticationTypes);
        ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(int userId);
        Task<SignInStatus> ExternalSignIn(ApplicationExternalLoginInfo loginInfo, bool isPersistent);
        IEnumerable<ApplicationAuthenticationDescription> GetExternalAuthenticationTypes();
        Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType);
        ApplicationExternalLoginInfo GetExternalLoginInfo();
        ApplicationExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue);
        Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync();
        Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue);
        Task<int?> GetVerifiedUserIdAsync();
        Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
        Task<SignInStatus> SignInOrTwoFactor(AppUser user, bool isPersistent);
        void SignIn(params ClaimsIdentity[] identities);
        void SignIn(bool isPersistent, params ClaimsIdentity[] identities);
        Task SignInAsync(AppUser user, bool isPersistent, bool rememberBrowser);
        void SignOut(params string[] authenticationTypes);
        Task<bool> TwoFactorBrowserRememberedAsync(int userId);
        Task<SignInStatus> TwoFactorSignIn(string provider, string code, bool isPersistent, bool rememberBrowser);
    }
}