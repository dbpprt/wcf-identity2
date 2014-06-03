using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationTemplate.Infrastructure.Contracts;

namespace ApplicationTemplate.Infrastructure.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContext Context;
        protected readonly IDbSet<TEntity> DbSet;

        protected AbstractEntityTrigger<TEntity> TriggerEngine { get; set; }

        public Repository(IDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        //public void Update(TEntity entity)
        //{
        //    _dbSet.Attach(entity);
        //    _context.SetState(entity, EntityState.Modified);
        //}

        //public void Delete(TEntity entity)
        //{
        //    _dbSet.Attach(entity);
        //    _dbSet.Remove(entity);
        //}

        //public TEntity Insert(TEntity entity)
        //{
        //    _dbSet.Attach(entity);
        //    _context.SetState(entity, EntityState.Added);

        //    return entity;
        //}

        //public void InsertRange(IEnumerable<TEntity> entities)
        //{
        //    foreach (var entity in entities)
        //    {
        //        Insert(entity);
        //    }
        //}

        public virtual void Update(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeUpdate(entity);
            DbSet.Attach(entity);
            Context.SetState(entity, EntityState.Modified);

            if (saveChanges)
            {
                Context.SaveChanges();
                if (TriggerEngine != null) TriggerEngine.AfterUpdate(entity);
            }
        }

        public virtual void Delete(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeDelete(entity);
            DbSet.Attach(entity);
            DbSet.Remove(entity);

            if (saveChanges)
            {
                Context.SaveChanges();
                if (TriggerEngine != null) TriggerEngine.AfterDelete(entity);
            }
        }

        public virtual TEntity Insert(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeAdd(entity);
            DbSet.Attach(entity);
            Context.SetState(entity, EntityState.Added);

            if (saveChanges)
            {
                Context.SaveChanges();
                if (TriggerEngine != null) TriggerEngine.AfterDelete(entity);
            }

            return entity;
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            entities.ToList().ForEach(entity => Insert(entity, saveChanges));
        }

        public virtual async Task UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeUpdate(entity);
            DbSet.Attach(entity);
            Context.SetState(entity, EntityState.Modified);

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
                if (TriggerEngine != null) TriggerEngine.AfterUpdate(entity);
            }
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeDelete(entity);
            DbSet.Attach(entity);
            DbSet.Remove(entity);

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
                if (TriggerEngine != null) TriggerEngine.AfterDelete(entity);
            }
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool saveChanges = true)
        {
            if (TriggerEngine != null) TriggerEngine.BeforeAdd(entity);
            DbSet.Attach(entity);
            Context.SetState(entity, EntityState.Added);

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
                if (TriggerEngine != null) TriggerEngine.AfterDelete(entity);
            }

            return entity;
        }

        public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true)
        {
            var tasks = new List<Task>();
            entities.ToList().ForEach(entity => tasks.Add(InsertAsync(entity, saveChanges)));
            return Task.WhenAll(tasks);
        }

        public virtual IQueryable<TEntity> Query()
        {
            return DbSet;
        }
    }
}
