//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;

//namespace Ardutecture.Core
//{
//    // TODO: DEBUG THE ASYNC STUFF?! LOOKS NASTY!
//    public class TransactionalRepository<TEntity>  : Repository<TEntity> where TEntity : class
//    {
//        public IsolationLevel IsolationLevel { get; private set; }

//        public TransactionalRepository(IDbContext context, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted) 
//            : base(context)
//        {
//            IsolationLevel = isolationLevel;
//        }

//        public override TEntity Insert(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    var result = base.Insert(entity, saveChanges);
//                    scope.Commit();

//                    return result;
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override void Update(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    base.Update(entity, saveChanges);
//                    scope.Commit();
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override void Delete(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    base.Delete(entity, saveChanges);
//                    scope.Commit();
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override void InsertRange(IEnumerable<TEntity> entities, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    base.InsertRange(entities, saveChanges);
//                    scope.Commit();
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override Task<TEntity> InsertAsync(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    var result = base.InsertAsync(entity, saveChanges);
//                    scope.Commit();

//                    return result;
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override Task UpdateAsync(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    var task = base.UpdateAsync(entity, saveChanges);
//                    scope.Commit();

//                    return task;
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override Task DeleteAsync(TEntity entity, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    var task = base.DeleteAsync(entity, saveChanges);
//                    scope.Commit();

//                    return task;
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }

//        public override Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
//        {
//            using (var scope = Context.Database.BeginTransaction(IsolationLevel))
//            {
//                try
//                {
//                    var task = base.InsertRangeAsync(entities, saveChanges);
//                    scope.Commit();

//                    return task;
//                }
//                catch (Exception)
//                {
//                    scope.Rollback();
//                    throw;
//                }
//            }
//        }
//    }
//}

namespace ApplicationTemplate.Infrastructure.Common
{
}