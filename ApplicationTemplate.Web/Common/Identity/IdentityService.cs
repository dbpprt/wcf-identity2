using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;
using ApplicationTemplate.Models.Extensions;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ApplicationTemplate.Web.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IApplicationUserManager _userManager;

        public IdentityService(
            IApplicationUserManager userManager, 
            IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        public virtual void Challenge(string redirectUri, string xsrfKey, int? userId, params string[] authenticationTypes)
        {
            var properties = new AuthenticationProperties { RedirectUri = redirectUri };
            if (userId != null)
            {
                properties.Dictionary[xsrfKey] = userId.ToString();
            }
            _authenticationManager.Challenge(properties, authenticationTypes);
        }

        public virtual ClaimsIdentity CreateTwoFactorRememberBrowserIdentity(int userId)
        {
            return _authenticationManager.CreateTwoFactorRememberBrowserIdentity(userId.ToString());
        }

        public virtual async Task<SignInStatus> ExternalSignIn(ApplicationExternalLoginInfo loginInfo, bool isPersistent)
        {
            var user = await _userManager.FindByLoginInfoAsync(loginInfo.Login).ConfigureAwait(false);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await _userManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
            {
                return SignInStatus.LockedOut;
            }
            return await SignInOrTwoFactor(user, isPersistent).ConfigureAwait(false);
        }

        public virtual IEnumerable<ApplicationAuthenticationDescription> GetExternalAuthenticationTypes()
        {
            return _authenticationManager.GetExternalAuthenticationTypes().ToApplicationAuthenticationDescriptionList();
        }

        public virtual async Task<ClaimsIdentity> GetExternalIdentityAsync(string externalAuthenticationType)
        {
            return await _authenticationManager.GetExternalIdentityAsync(externalAuthenticationType).ConfigureAwait(false);
        }

        public virtual ApplicationExternalLoginInfo GetExternalLoginInfo()
        {
            return _authenticationManager.GetExternalLoginInfo().ToApplicationExternalLoginInfo();
        }

        public virtual ApplicationExternalLoginInfo GetExternalLoginInfo(string xsrfKey, string expectedValue)
        {
            return _authenticationManager.GetExternalLoginInfo(xsrfKey, expectedValue).ToApplicationExternalLoginInfo();
        }

        public virtual async Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync()
        {
            var externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync().ConfigureAwait(false);
            return externalLoginInfo.ToApplicationExternalLoginInfo();
        }

        public virtual async Task<ApplicationExternalLoginInfo> GetExternalLoginInfoAsync(string xsrfKey, string expectedValue)
        {
            var externalLoginInfo = await _authenticationManager.GetExternalLoginInfoAsync(xsrfKey, expectedValue).ConfigureAwait(false);
            return externalLoginInfo.ToApplicationExternalLoginInfo();
        }

        public virtual async Task<int?> GetVerifiedUserIdAsync()
        {
            var result = await _authenticationManager.AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).ConfigureAwait(false);
            if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
            {
                return int.Parse(result.Identity.GetUserId());
            }
            return null;
        }
        public virtual async Task<SignInStatus> PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await _userManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
            {
                return SignInStatus.LockedOut;
            }
            if (await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false))
            {
                return await SignInOrTwoFactor(user, isPersistent).ConfigureAwait(false);
            }
            if (shouldLockout)
            {
                // If lockout is requested, increment access failed count which might lock out the user
                await _userManager.AccessFailedAsync(user.Id).ConfigureAwait(false);
                if (await _userManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
                {
                    return SignInStatus.LockedOut;
                }
            }
            return SignInStatus.Failure;
        }

        public virtual async Task<SignInStatus> SignInOrTwoFactor(AppUser user, bool isPersistent)
        {
            if (await _userManager.GetTwoFactorEnabledAsync(user.Id).ConfigureAwait(false) &&
                !await TwoFactorBrowserRememberedAsync(user.Id).ConfigureAwait(false))
            {
                var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                SignIn(identity);
                return SignInStatus.RequiresTwoFactorAuthentication;
            }
            await SignInAsync(user, isPersistent, false).ConfigureAwait(false);
            return SignInStatus.Success;

        }

        public virtual void SignIn(params ClaimsIdentity[] identities)
        {
            _authenticationManager.SignIn(identities);
        }

        public virtual void SignIn(bool isPersistent, params ClaimsIdentity[] identities)
        {
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identities);
        }

        public virtual async Task SignInAsync(AppUser user, bool isPersistent, bool rememberBrowser)
        {
            // Clear any partial cookies from external or two factor partial sign ins
            SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            var userIdentity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            if (rememberBrowser)
            {
                var rememberBrowserIdentity = CreateTwoFactorRememberBrowserIdentity(user.Id);
                _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity, rememberBrowserIdentity);
            }
            else
            {
                _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
            }
        }

        public void SignOut(params string[] authenticationTypes)
        {
            _authenticationManager.SignOut(authenticationTypes);
        }

        public virtual async Task<bool> TwoFactorBrowserRememberedAsync(int userId)
        {
            return await _authenticationManager.TwoFactorBrowserRememberedAsync(userId.ToString()).ConfigureAwait(false);
        }

        public virtual async Task<SignInStatus> TwoFactorSignIn(string provider, string code, bool isPersistent, bool rememberBrowser)
        {
            var userId = await GetVerifiedUserIdAsync().ConfigureAwait(false);
            if (userId == null)
            {
                return SignInStatus.Failure;
            }
            var user = await _userManager.FindByIdAsync(userId.Value).ConfigureAwait(false);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            if (await _userManager.IsLockedOutAsync(user.Id).ConfigureAwait(false))
            {
                return SignInStatus.LockedOut;
            }
            if (await _userManager.VerifyTwoFactorTokenAsync(user.Id, provider, code).ConfigureAwait(false))
            {
                // When token is verified correctly, clear the access failed count used for lockout
                await _userManager.ResetAccessFailedCountAsync(user.Id).ConfigureAwait(false);
                await SignInAsync(user, isPersistent, rememberBrowser).ConfigureAwait(false);
                return SignInStatus.Success;
            }
            // If the token is incorrect, record the failure which also may cause the user to be locked out
            await _userManager.AccessFailedAsync(user.Id).ConfigureAwait(false);
            return SignInStatus.Failure;
        }

    }
}