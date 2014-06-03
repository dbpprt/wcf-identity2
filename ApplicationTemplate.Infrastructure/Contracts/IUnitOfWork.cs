using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationTemplate.Infrastructure.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        void BeginTransaction();
        int Commit();
        void Rollback();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> CommitAsync();
    }
}