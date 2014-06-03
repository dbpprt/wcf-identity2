using System;
using ApplicationTemplate.Infrastructure.Common;

namespace ApplicationTemplate.Infrastructure.Triggers
{
    /// <summary>
    /// Generic entity validator to pass everything ^^.. when a entity needs some custom logic for validation
    /// then simply implement AbstractEntityTrigger and add it to the dependency config!
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class ArgumentNullTrigger<TEntity> : AbstractEntityTrigger<TEntity> where TEntity : class
    {
        public override void BeforeAdd(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
        }

        public override void BeforeUpdate(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
        }

        public override void BeforeDelete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
        }
    }
}
