using System.Web;
using System.Web.Mvc;
using ApplicationTemplate.Annotations;
using ApplicationTemplate.Web;
using ApplicationTemplate.Web.Identity;
using Autofac;
using Autofac.Integration.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DependencyConfig), "Start")]

namespace ApplicationTemplate.Web
{
        public static class DependencyConfig
    {
        [UsedImplicitly]
        public static void Start()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule<AutofacWebTypesModule>();

            builder.RegisterModule(new ServiceConfig());

            builder.RegisterType(typeof (IdentityService)).As(typeof (IIdentityService)).InstancePerRequest();

            builder.Register(b => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
         

        }
    }
}
