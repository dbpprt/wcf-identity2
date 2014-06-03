using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Models.Entities.Common;
using ApplicationTemplate.Models.Entities.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ApplicationTemplate.Infrastructure
{
    public class EntityContextBase : 
        IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>, 
        IDbContext
    {
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        private static readonly object Lock = new object();
        private static bool _databaseInitialized;

        public DbSet<Error> Errors { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new DateTime2Convention());
        }

        public EntityContextBase()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public EntityContextBase(string nameOrConnectionString) :
            base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void SetState(object o, EntityState state)
        {
            Entry(o).State = state;
        }

        public EntityState GetState(object o)
        {
            return Entry(o).State;
        }

        public void BeginTransaction()
        {
            throw new System.NotImplementedException();
        }

        public int Commit()
        {
            throw new System.NotImplementedException();
        }

        public void Rollback()
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CommitAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
