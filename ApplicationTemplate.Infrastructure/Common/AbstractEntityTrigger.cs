namespace ApplicationTemplate.Infrastructure.Common
{
    /// <summary>
    /// Implements a basic contract for custom entity validation on crud-actions
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AbstractEntityTrigger<TEntity> where TEntity : class
    {
        public virtual void BeforeAdd(TEntity entity) { }
        public virtual void BeforeUpdate(TEntity update) { }
        public virtual void BeforeDelete(TEntity entity) { }

        public virtual void AfterAdd(TEntity entity) { }
        public virtual void AfterUpdate(TEntity update) { }
        public virtual void AfterDelete(TEntity entity) { }
    }
}
