using System.Data.Entity;
using System.Linq;
using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Models.Entities.Blog;

namespace ApplicationTemplate.Services.Database.Repositories
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(IDbContext context) : base(context)
        {
        }

        public override IQueryable<Post> Query()
        {
            return base.Query()
                .Include(entity => entity.Comments)
                .Include(entity => entity.Tags)
                .Include(entity => entity.Category);
        }
    }
}
