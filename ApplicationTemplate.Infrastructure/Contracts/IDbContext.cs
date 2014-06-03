using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTemplate.Infrastructure.Contracts
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : class;
        
        Task<int> SaveChangesAsync();
        
        void SetState(object o, EntityState state);
        
        EntityState GetState(object o);
        
        int SaveChanges();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        void BeginTransaction();
       
        int Commit();
        
        void Rollback();
        
        Task<int> CommitAsync();
        
        void Dispose();
    }

}
