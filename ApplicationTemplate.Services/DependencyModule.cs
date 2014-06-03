using System.Data.Entity;
using ApplicationTemplate.Infrastructure;
using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Infrastructure.Services;
using ApplicationTemplate.Infrastructure.Triggers;
using ApplicationTemplate.Models.Entities.Identity;
using ApplicationTemplate.ServiceContracts;
using ApplicationTemplate.Services.Database;
using Autofac;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationTemplate.Services
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(ArgumentNullTrigger<>)).As(typeof(AbstractEntityTrigger<>)).InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EntityContext)).As(typeof(IDbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(EntityContext)).As(typeof(DbContext)).InstancePerLifetimeScope();

            builder.RegisterType(
                typeof (
                    UserStore
                        <ApplicationIdentityUser, ApplicationIdentityRole, int, ApplicationIdentityUserLogin,
                            ApplicationIdentityUserRole, ApplicationIdentityUserClaim>))
                .As(typeof (IUserStore<ApplicationIdentityUser, int>))
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof (RoleStore<ApplicationIdentityRole, int, ApplicationIdentityUserRole>))
                .As(typeof (IRoleStore<ApplicationIdentityRole, int>))
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(ApplicationUserManager))
                .As(typeof(IApplicationUserManager))
                .InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ApplicationIdentityUser)).As(typeof(IUser<int>)).InstancePerLifetimeScope();

            builder.Register(b => IdentityFactory.CreateUserManager(
                b.Resolve<IEmailService>(), 
                b.Resolve<ISmsService>(), 
                b.Resolve<IUserStore<ApplicationIdentityUser, int>>()
            )).InstancePerLifetimeScope();

            builder.Register(b => IdentityFactory.CreateRoleManager(
                b.Resolve<IRoleStore<ApplicationIdentityRole, int>>()
            )).InstancePerLifetimeScope();

        }
    }
}
