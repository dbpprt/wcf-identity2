using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;

namespace ApplicationTemplate.ServiceContracts
{

    [ServiceContract]
    public interface IApplicationUserManager
    {

        [OperationContract]
        Task<ApplicationIdentityResult> AccessFailedAsync(int userId);

        [OperationContract]
        Task<ApplicationIdentityResult> AddClaimAsync(int userId, Claim claim);

        [OperationContract]
        Task<ApplicationIdentityResult> AddLoginAsync(int userId, ApplicationUserLoginInfo login);

        [OperationContract]
        Task<ApplicationIdentityResult> AddPasswordAsync(int userId, string password);

        [OperationContract]
        Task<ApplicationIdentityResult> AddToRoleAsync(int userId, string role);

        //[OperationContract]
        //ApplicationIdentityResult AddToRole(int userId, string role);

        [OperationContract]
        Task<ApplicationIdentityResult> AddUserToRolesAsync(int userId, IList<string> roles);

        [OperationContract]
        Task<ApplicationIdentityResult> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

        [OperationContract]
        Task<ApplicationIdentityResult> ChangePhoneNumberAsync(int userId, string phoneNumber, string token);

        [OperationContract]
        Task<bool> CheckPasswordAsync(AppUser user, string password);

        [OperationContract]
        Task<ApplicationIdentityResult> ConfirmEmailAsync(int userId, string token);

        [OperationContract]
        Task<ApplicationIdentityResult> CreateAsync(AppUser user);

        [OperationContract]
        Task<ApplicationIdentityResult> CreateWithPasswordAsync(AppUser user, string password);

        //[OperationContract]
        //ClaimsIdentity CreateIdentity(AppUser user, string authenticationType);

        [OperationContract]
        Task<ClaimsIdentity> CreateIdentityAsync(AppUser user, string authenticationType);

        //[OperationContract]
        //ApplicationIdentityResult Create(AppUser user);

        //[OperationContract]
        //ApplicationIdentityResult CreateWithPassword(AppUser user, string password);

        [OperationContract]
        Task<ApplicationIdentityResult> DeleteAsync(int userId);

        //[OperationContract]
        //Task<AppUser> FindAsync(ApplicationUserLoginInfo login);

        [OperationContract]
        Task<AppUser> FindAsync(string userName, string password);

        [OperationContract]
        Task<AppUser> FindByLoginInfoAsync(ApplicationUserLoginInfo login);

        [OperationContract]
        Task<AppUser> FindByEmailAsync(string email);

        //[OperationContract]
        //AppUser FindById(int userId);

        [OperationContract]
        Task<AppUser> FindByIdAsync(int userId);

        [OperationContract]
        Task<AppUser> FindByNameAsync(string userName);

        //[OperationContract]
        //AppUser FindByName(string userName);

        [OperationContract]
        Task<string> GenerateChangePhoneNumberTokenAsync(int userId, string phoneNumber);

        [OperationContract]
        Task<string> GenerateEmailConfirmationTokenAsync(int userId);

        [OperationContract]
        Task<string> GeneratePasswordResetTokenAsync(int userId);

        [OperationContract]
        Task<string> GenerateTwoFactorTokenAsync(int userId, string twoFactorProvider);

        [OperationContract]
        Task<string> GenerateUserTokenAsync(string purpose, int userId);

        [OperationContract]
        Task<int> GetAccessFailedCountAsync(int userId);

        [OperationContract]
        Task<IList<Claim>> GetClaimsAsync(int userId);

        [OperationContract]
        Task<string> GetEmailAsync(int userId);

        [OperationContract]
        Task<bool> GetLockoutEnabledAsync(int userId);

        [OperationContract]
        Task<DateTimeOffset> GetLockoutEndDateAsync(int userId);

        //[OperationContract]
        //IList<ApplicationUserLoginInfo> GetLogins(int userId);

        [OperationContract]
        Task<IList<ApplicationUserLoginInfo>> GetLoginsAsync(int userId);

        [OperationContract]
        Task<string> GetPhoneNumberAsync(int userId);

        //[OperationContract]
        //IList<string> GetRoles(int userId);

        [OperationContract]
        Task<IList<string>> GetRolesAsync(int userId);

        [OperationContract]
        Task<string> GetSecurityStampAsync(int userId);

        [OperationContract]
        Task<bool> GetTwoFactorEnabledAsync(int userId);

        [OperationContract]
        Task<IList<string>> GetValidTwoFactorProvidersAsync(int userId);

        [OperationContract]
        Task<bool> HasPasswordAsync(int userId);

        [OperationContract]
        Task<bool> IsEmailConfirmedAsync(int userId);

        [OperationContract]
        Task<bool> IsInRoleAsync(int userId, string role);

        [OperationContract]
        Task<bool> IsLockedOutAsync(int userId);

        [OperationContract]
        Task<bool> IsPhoneNumberConfirmedAsync(int userId);

        [OperationContract]
        Task<ApplicationIdentityResult> NotifyTwoFactorTokenAsync(int userId, string twoFactorProvider, string token);

        [OperationContract]
        Task<ApplicationIdentityResult> RemoveClaimAsync(int userId, Claim claim);

        [OperationContract]
        Task<ApplicationIdentityResult> RemoveFromRoleAsync(int userId, string role);

        [OperationContract]
        Task<ApplicationIdentityResult> RemoveLoginAsync(int userId,
            ApplicationUserLoginInfo login);

        [OperationContract]
        Task<ApplicationIdentityResult> RemovePasswordAsync(int userId);

        [OperationContract]
        Task<ApplicationIdentityResult> RemoveUserFromRolesAsync(int userId, IList<string> roles);

        [OperationContract]
        Task<ApplicationIdentityResult> ResetAccessFailedCountAsync(int userId);

        [OperationContract]
        Task<ApplicationIdentityResult> ResetPasswordAsync(int userId, string token,
            string newPassword);

        [OperationContract]
        Task SendEmailAsync(int userId, string subject, string body);

        [OperationContract]
        Task SendSmsAsync(int userId, string message);

        //[OperationContract]
        //Task SendSmsAsync(ApplicationMessage message);

        [OperationContract]
        Task<bool> SendTwoFactorCodeAsync(int userId, string provider);

        [OperationContract]
        Task<ApplicationIdentityResult> SetEmailAsync(int userId, string email);

        //[OperationContract]
        //ApplicationIdentityResult SetLockoutEnabled(int userId, bool enabled);

        [OperationContract]
        Task<ApplicationIdentityResult> SetLockoutEnabledAsync(int userId, bool enabled);

        [OperationContract]
        Task<ApplicationIdentityResult> SetLockoutEndDateAsync(int userId,
            DateTimeOffset lockoutEnd);

        [OperationContract]
        Task<ApplicationIdentityResult> SetPhoneNumberAsync(int userId, string phoneNumber);

        [OperationContract]
        Task<ApplicationIdentityResult> SetTwoFactorEnabledAsync(int userId, bool enabled);

        [OperationContract]
        Task<ApplicationIdentityResult> UpdateAsync(AppUser appUser);

        [OperationContract]
        Task<ApplicationIdentityResult> UpdateSecurityStampAsync(int userId);

        //[OperationContract]
        //IEnumerable<AppUser> GetUsers();

        [OperationContract]
        Task<IEnumerable<AppUser>> GetUsersAsync();

        [OperationContract]
        Task<bool> VerifyChangePhoneNumberTokenAsync(int userId, string token,
            string phoneNumber);

        [OperationContract]
        Task<bool> VerifyTwoFactorTokenAsync(int userId, string twoFactorProvider, string token);

        [OperationContract]
        Task<bool> VerifyUserTokenAsync(int userId, string purpose, string token);
        
        void Dispose();
        
        void Dispose(bool disposing);
    }
}