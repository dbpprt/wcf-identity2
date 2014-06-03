using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationTemplate.Infrastructure.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Update(TEntity entity, bool saveChanges = true);
        void Delete(TEntity entity, bool saveChanges = true);
        TEntity Insert(TEntity entity, bool saveChanges = true);
        void InsertRange(IEnumerable<TEntity> entities, bool saveChanges = true);
        Task UpdateAsync(TEntity entity, bool saveChanges = true);
        Task DeleteAsync(TEntity entity, bool saveChanges = true);
        Task<TEntity> InsertAsync(TEntity entity, bool saveChanges = true);
        Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true);
        IQueryable<TEntity> Query();
    }
}
