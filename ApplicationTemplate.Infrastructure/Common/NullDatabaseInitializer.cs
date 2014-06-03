using System.Data.Entity;

namespace ApplicationTemplate.Infrastructure.Common
{
    public class NullDatabaseInitializer<TContext> :
        IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
        }
    }
}
