using ApplicationTemplate.Annotations;
using ApplicationTemplate.Backend;
using ApplicationTemplate.Infrastructure.Services;
using ApplicationTemplate.ServiceContracts;
using ApplicationTemplate.Services;
using Autofac;
using Autofac.Integration.Wcf;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DependencyConfig), "Start")]
namespace ApplicationTemplate.Backend
{

    public static class DependencyConfig
    {
        [UsedImplicitly]
        public static void Start()
        {
            var container = new ContainerBuilder();
            container.RegisterModule<DependencyModule>();
            RegisterDependencies(container);
            AutofacHostFactory.Container = container.Build();
        }

        private static void RegisterDependencies(ContainerBuilder container)
        {
            container.RegisterType(typeof(EmailService)).As(typeof(IEmailService)).InstancePerLifetimeScope();
            container.RegisterType(typeof(SmsService)).As(typeof(ISmsService)).InstancePerLifetimeScope();
            container.RegisterType(typeof(ApplicationUserManager)).As(typeof(IApplicationUserManager)).InstancePerLifetimeScope();
            container.RegisterType(typeof(ApplicationRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerLifetimeScope();
        }
    }
}
