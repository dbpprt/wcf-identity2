using System;
using ApplicationTemplate.Models.Entities.Identity;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;

namespace ApplicationTemplate.Services
{
    public class IdentityFactory
    {
        public static UserManager<ApplicationIdentityUser, int> CreateUserManager(
            IEmailService emailService,
            ISmsService smsService,
            IUserStore<ApplicationIdentityUser, int> userStore)
        {
            var manager = new UserManager<ApplicationIdentityUser, int>(
                userStore
                );

            manager.UserValidator = new  UserValidator<ApplicationIdentityUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationIdentityUser, int>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationIdentityUser, int>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });

            manager.EmailService = emailService;
            manager.SmsService = smsService;

            return manager;
        }

        public static RoleManager<ApplicationIdentityRole, int> CreateRoleManager(IRoleStore<ApplicationIdentityRole, int> roleStore)
        {
            return new RoleManager<ApplicationIdentityRole, int>(
                roleStore
                //new RoleStore<ApplicationIdentityRole, int, ApplicationIdentityUserRole>(context)
                );
        }
    }
}
