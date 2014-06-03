using System.ServiceModel;
using System.ServiceModel.Description;
using ApplicationTemplate.ServiceContracts;
using Autofac;
using Autofac.Integration.Wcf;

namespace ApplicationTemplate.Web
{
    public class ServiceConfig : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new ChannelFactory<IApplicationUserManager>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost/AppTemplateBackend/UserManager"))
            ).SingleInstance();

            builder.Register(c => new ChannelFactory<IApplicationRoleManager>(
                new BasicHttpBinding(),
                new EndpointAddress("http://localhost/AppTemplateBackend/RoleManager"))
            ).SingleInstance();

            builder.Register(c => c.Resolve<ChannelFactory<IApplicationRoleManager>>().CreateChannel())
                .UseWcfSafeRelease();
            builder.Register(c => c.Resolve<ChannelFactory<IApplicationUserManager>>().CreateChannel())
                .UseWcfSafeRelease();

        }
    }

    public static class Extensions
    {
        public static ChannelFactory<T> WithBehaviour<T>(this ChannelFactory<T> channelFactory, IEndpointBehavior behavior)
        {
            channelFactory.Endpoint.Behaviors.Add(behavior);
            return channelFactory;
        } 
    }
}