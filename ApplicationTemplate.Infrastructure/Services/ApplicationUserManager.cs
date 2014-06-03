using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Models.DataTransfer;
using ApplicationTemplate.Models.Entities.Identity;
using ApplicationTemplate.Models.Extensions;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;

namespace ApplicationTemplate.Infrastructure.Services
{
    
    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationIdentityUser, int> _userManager;
        private bool _disposed;
        public ApplicationUserManager(
            UserManager<ApplicationIdentityUser, int> userManager,
            IDbContext context)
        {
            _userManager = userManager;
        }

        public virtual async Task<ApplicationIdentityResult> AccessFailedAsync(int userId)
        {
            var identityResult = await _userManager.AccessFailedAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> AddClaimAsync(int userId, Claim claim)
        {
            var identityResult = await _userManager.AddClaimAsync(userId, claim).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> AddLoginAsync(int userId, ApplicationUserLoginInfo login)
        {
            var identityResult = await _userManager.AddLoginAsync(userId, login.ToUserLoginInfo()).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> AddPasswordAsync(int userId, string password)
        {
            var identityResult = await _userManager.AddPasswordAsync(userId, password).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> AddToRoleAsync(int userId, string role)
        {
            var identityResult = await _userManager.AddToRoleAsync(userId, role).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> AddUserToRolesAsync(int userId, IList<string> roles)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await GetRolesAsync(userId).ConfigureAwait(false);
            // Add user to each role using UserRoleStore
            foreach (var role in roles.Where(role => !userRoles.Contains(role)))
            {
                await AddToRoleAsync(userId, role).ConfigureAwait(false);
            }

            // Call update once when all roles are added
            return await UpdateAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var identityResult = await _userManager.ChangePasswordAsync(userId, currentPassword, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> ChangePhoneNumberAsync(int userId, string phoneNumber, string token)
        {
            var identityResult = await _userManager.ChangePhoneNumberAsync(userId, phoneNumber, token).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            var applicationUser = user.ToApplicationUser();
            var flag = await _userManager.CheckPasswordAsync(applicationUser, password).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return flag;
        }

        public virtual async Task<ApplicationIdentityResult> ConfirmEmailAsync(int userId,  string token)
        {
            var identityResult = await _userManager.ConfirmEmailAsync(userId, token).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> CreateAsync(AppUser user)
        {
            var applicationUser = user.ToApplicationUser();
            var identityResult = await _userManager.CreateAsync(applicationUser).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> CreateWithPasswordAsync(AppUser user, string password)
        {
            var applicationUser = user.ToApplicationUser();
            var identityResult = await _userManager.CreateAsync(applicationUser, password).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ClaimsIdentity> CreateIdentityAsync(AppUser user, string authenticationType)
        {
            var applicationUser = user.ToApplicationUser();
            var claimsIdentity = await _userManager.CreateIdentityAsync(applicationUser, authenticationType).ConfigureAwait(false);
            user.CopyApplicationIdentityUserProperties(applicationUser);
            return claimsIdentity;
        }

        public virtual async Task<ApplicationIdentityResult> DeleteAsync(int userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId);
            if (applicationUser == null)
            {
                return new ApplicationIdentityResult(new[] { "Invalid user Id" }, false);
            }
            var identityResult = await _userManager.DeleteAsync(applicationUser).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<AppUser> FindByLoginInfoAsync(ApplicationUserLoginInfo login)
        {
            var user = await _userManager.FindAsync(login.ToUserLoginInfo()).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public virtual async Task<AppUser> FindAsync(string userName, string password)
        {
            var user = await _userManager.FindAsync(userName, password).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public virtual async Task<AppUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public virtual async Task<AppUser> FindByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public virtual async Task<AppUser> FindByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);
            return user.ToAppUser();
        }

        public virtual AppUser FindByName(string userName)
        {
            var user = _userManager.FindByName(userName);
            return user.ToAppUser();
        }

        public virtual async Task<string> GenerateChangePhoneNumberTokenAsync(int userId, string phoneNumber)
        {
             return await _userManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber).ConfigureAwait(false);
        }

        public virtual async Task<string> GenerateEmailConfirmationTokenAsync(int userId)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<string> GenerateTwoFactorTokenAsync(int userId, string twoFactorProvider)
        {
            return await _userManager.GenerateTwoFactorTokenAsync(userId, twoFactorProvider).ConfigureAwait(false);
        }

        public virtual async Task<string> GenerateUserTokenAsync(string purpose, int userId)
        {
            return await _userManager.GenerateUserTokenAsync(purpose, userId).ConfigureAwait(false);
        }

        public virtual async Task<int> GetAccessFailedCountAsync(int userId)
        {
            return await _userManager.GetAccessFailedCountAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<IList<Claim>> GetClaimsAsync(int userId)
        {
            return await _userManager.GetClaimsAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<string> GetEmailAsync(int userId)
        {
            return await _userManager.GetEmailAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> GetLockoutEnabledAsync(int userId)
        {
            return await _userManager.GetLockoutEnabledAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<DateTimeOffset> GetLockoutEndDateAsync(int userId)
        {
            return await _userManager.GetLockoutEndDateAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<IList<ApplicationUserLoginInfo>> GetLoginsAsync(int userId)
        {
            var list = await _userManager.GetLoginsAsync(userId).ConfigureAwait(false);
            return list.ToApplicationUserLoginInfoList();
        }

        public virtual async Task<string> GetPhoneNumberAsync(int userId)
        {
            return await _userManager.GetPhoneNumberAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<IList<string>> GetRolesAsync(int userId)
        {
             return await _userManager.GetRolesAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<string> GetSecurityStampAsync(int userId)
        {
            return await _userManager.GetSecurityStampAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> GetTwoFactorEnabledAsync(int userId)
        {
            return await _userManager.GetTwoFactorEnabledAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId)
        {
            return await _userManager.GetValidTwoFactorProvidersAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> HasPasswordAsync(int userId)
        {
            return await _userManager.HasPasswordAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> IsEmailConfirmedAsync(int userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> IsInRoleAsync(int userId, string role)
        {
            return await _userManager.IsInRoleAsync(userId,role).ConfigureAwait(false);
        }

        public virtual async Task<bool> IsLockedOutAsync(int userId)
        {
            return await _userManager.IsLockedOutAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<bool> IsPhoneNumberConfirmedAsync(int userId)
        {
            return await _userManager.IsPhoneNumberConfirmedAsync(userId).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> NotifyTwoFactorTokenAsync(int userId, string twoFactorProvider, string token)
        {
            var identityResult = await _userManager.NotifyTwoFactorTokenAsync(userId, twoFactorProvider, token).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemoveClaimAsync(int userId, Claim claim)
        {
            var identityResult = await _userManager.RemoveClaimAsync(userId, claim).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemoveFromRoleAsync(int userId, string role)
        {
            var identityResult = await _userManager.RemoveFromRoleAsync(userId, role).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemoveLoginAsync(int userId,
            ApplicationUserLoginInfo login)
        {
            var identityResult = await _userManager.RemoveLoginAsync(userId, login.ToUserLoginInfo()).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemovePasswordAsync(int userId)
        {
            var identityResult = await _userManager.RemovePasswordAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> RemoveUserFromRolesAsync(int userId, IList<string> roles)
        {
            var user = await FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid user Id");
            }

            var userRoles = await GetRolesAsync(user.Id).ConfigureAwait(false);
            // Remove user to each role using UserRoleStore
            foreach (var role in roles.Where(userRoles.Contains))
            {
                await RemoveFromRoleAsync(user.Id, role).ConfigureAwait(false);
            }

            // Call update once when all roles are removed
            return await UpdateAsync(user.Id).ConfigureAwait(false);
        }

        public virtual async Task<ApplicationIdentityResult> ResetAccessFailedCountAsync(int userId)
        {
            var identityResult = await _userManager.ResetAccessFailedCountAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> ResetPasswordAsync(int userId, string token,
            string newPassword)
        {
            var identityResult = await _userManager.ResetPasswordAsync(userId, token, newPassword).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task SendEmailAsync(int userId, string subject, string body)
        {
            await _userManager.SendEmailAsync(userId, subject, body).ConfigureAwait(false);
        }

        public virtual async Task SendSmsAsync(int userId, string message)
        {
            await _userManager.SendSmsAsync(userId, message).ConfigureAwait(false);
        }

        public virtual async Task SendSmsAsync(ApplicationMessage message)
        {
            if (_userManager.SmsService != null)
            {
                await _userManager.SmsService.SendAsync(message.ToIdentityMessage());
            }
        }

        public virtual async Task<bool> SendTwoFactorCodeAsync(int userId, string provider)
        {
            var token = await GenerateTwoFactorTokenAsync(userId, provider).ConfigureAwait(false);
            await NotifyTwoFactorTokenAsync(userId, provider, token).ConfigureAwait(false);
            return true;
        }

        public virtual async Task<ApplicationIdentityResult> SetEmailAsync(int userId, string email)
        {
            var identityResult = await _userManager.SetEmailAsync(userId, email).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> SetLockoutEnabledAsync(int userId, bool enabled)
        {
            var identityResult = await _userManager.SetLockoutEnabledAsync(userId, enabled).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> SetLockoutEndDateAsync(int userId,
            DateTimeOffset lockoutEnd)
        {
            var identityResult = await _userManager.SetLockoutEndDateAsync(userId, lockoutEnd).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> SetPhoneNumberAsync(int userId, string phoneNumber)
        {
            var identityResult = await _userManager.SetPhoneNumberAsync(userId, phoneNumber).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> SetTwoFactorEnabledAsync(int userId, bool enabled)
        {
            var identityResult = await _userManager.SetTwoFactorEnabledAsync(userId, enabled).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> UpdateAsync(AppUser appUser)
        {
            var identityResult = await _userManager.UpdateAsync(appUser.ToApplicationUser()).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> UpdateAsync(int userId)
        {
            var applicationUser = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (applicationUser == null)
            {
                return new ApplicationIdentityResult(new[] { "Invalid user Id" }, false);
            }
            var identityResult = await _userManager.UpdateAsync(applicationUser).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<ApplicationIdentityResult> UpdateSecurityStampAsync(int userId)
        {
            var identityResult = await _userManager.UpdateSecurityStampAsync(userId).ConfigureAwait(false);
            return identityResult.ToApplicationIdentityResult();
        }

        public virtual async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            return users.ToAppUserList();
        }

        public virtual async Task<bool> VerifyChangePhoneNumberTokenAsync(int userId, string token,
            string phoneNumber)
        {
            return await _userManager.VerifyChangePhoneNumberTokenAsync(userId, token, phoneNumber).ConfigureAwait(false);
        }

        public virtual async Task<bool> VerifyTwoFactorTokenAsync(int userId, string twoFactorProvider, string token)
        {
            return await _userManager.VerifyTwoFactorTokenAsync(userId, twoFactorProvider, token).ConfigureAwait(false);
        }

        public virtual async Task<bool> VerifyUserTokenAsync(int userId, string purpose, string token)
        {
            return await _userManager.VerifyUserTokenAsync(userId, purpose, token).ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
