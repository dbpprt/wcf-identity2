//using System;
//using System.Collections;
//using System.Data.Common;
//using System.Data.Entity;
//using System.Threading.Tasks;

//namespace Ardutecture.Core.Database
//{
//    public class MemoryDbContext : IDbContext
//    {
//        private Hashtable _dbSets;


//        public DbConnection GetConnection()
//        {
//            return null;
//        }

//        public IDbSet<T> Set<T>() where T : class
//        {
//            if (_dbSets == null)
//                _dbSets = new Hashtable();

//            var type = typeof(T).Name;

//            if (!_dbSets.ContainsKey(type))
//            {
//                var repositoryType = typeof(MemoryDbSet<>);

//                var repositoryInstance =
//                    Activator.CreateInstance(repositoryType
//                            .MakeGenericType(typeof(T)));

//                _dbSets.Add(type, repositoryInstance);
//            }

//            return (IDbSet<T>)_dbSets[type];
//        }

//        public int SaveChanges()
//        {
//            return 0;
//        }

//        public Task<int> SaveChangesAsync()
//        {
//            return Task.Run(() => SaveChanges());
//        }

//        public void SetState(object o, EntityState state)
//        {
            
//        }

//        public EntityState GetState(object o)
//        {
//            return EntityState.Unchanged;
//        }

//        public void Dispose()
//        {
            
//        }

//        public System.Data.Entity.Database Database
//        {
//            get { return null; }
//        }

//        public MemoryDbContext()
//        {
//        }
//    }
//}

namespace ApplicationTemplate.Infrastructure.Common
{
}