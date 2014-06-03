using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ApplicationTemplate.Annotations;
using ApplicationTemplate.ServiceContracts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace ApplicationTemplate.Web.Identity
{
    [UsedImplicitly]
    public static class SecurityStampValidator
    {
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity(
            TimeSpan validateInterval)
        {
            return async context =>
            {
                var currentUtc = DateTimeOffset.UtcNow;
                if (context.Options != null && context.Options.SystemClock != null)
                    currentUtc = context.Options.SystemClock.UtcNow;
                var issuedUtc = context.Properties.IssuedUtc;
                var validate = !issuedUtc.HasValue;
                if (issuedUtc.HasValue)
                    validate = currentUtc.Subtract(issuedUtc.Value) > validateInterval;
                if (validate)
                {
                    var resolver = DependencyResolver.Current;
                    var manager = resolver.GetService<IApplicationUserManager>();

                    var userId = int.Parse(context.Identity.GetUserId());
                    if (manager != null)
                    {
                        var user = await manager.FindByIdAsync(userId).ConfigureAwait(false);
                        var reject = true;
                        if (user != null)
                        {
                            var securityStamp = context.Identity.FindFirstValue("AspNet.Identity.SecurityStamp");
                            if (securityStamp == await manager.GetSecurityStampAsync(userId).ConfigureAwait(false))
                            {
                                reject = false;
                                var identity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                                if (identity != null)
                                {
                                    context.OwinContext.Authentication.SignIn(new[]
                                    {
                                        identity
                                    });
                                }
                            }
                        }
                        if (reject)
                        {
                            context.RejectIdentity();
                            context.OwinContext.Authentication.SignOut(new string[1]
                            {
                                context.Options.AuthenticationType
                            });
                        }
                    }
                }
            };
        }
    }
}
