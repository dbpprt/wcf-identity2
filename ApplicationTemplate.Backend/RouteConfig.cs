using System.ServiceModel.Activation;
using System.Web.Routing;
using ApplicationTemplate.Annotations;
using ApplicationTemplate.Backend;
using ApplicationTemplate.ServiceContracts;
using Autofac.Integration.Wcf;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RouteConfig), "Start")]


namespace ApplicationTemplate.Backend
{

    public static class RouteConfig
    {
        [UsedImplicitly]
        public static void Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute(
                "UserManager", 
                new AutofacServiceHostFactory(), 
                typeof(IApplicationUserManager)));

            routes.Add(new ServiceRoute(
                "RoleManager",
                new AutofacServiceHostFactory(),
                typeof(IApplicationRoleManager)));
        }
    }
}
