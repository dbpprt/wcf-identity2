using ApplicationTemplate.Infrastructure.Contracts;

namespace ApplicationTemplate.Infrastructure.Common
{
    public class ServiceBase<TService>
    {
        //[Dependency]
        //protected ILogger<TService> Logger { get; set; }

        public ServiceBase(IUnitOfWork context)
        {
            Context = context;
        }

        protected IUnitOfWork Context;
    }
}
