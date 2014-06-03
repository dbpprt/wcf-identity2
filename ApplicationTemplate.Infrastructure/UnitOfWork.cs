using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;

namespace ApplicationTemplate.Infrastructure
{
    public class UnitOfWork : UnitOfWorkBase
    {
        public UnitOfWork(IDbContext context) 
            : base(context)
        {
           


        }
    }
}
