using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationTemplate.Infrastructure.Common;
using ApplicationTemplate.Infrastructure.Contracts;
using ApplicationTemplate.Infrastructure.Utilities;
using ApplicationTemplate.Models.DataTransfer;
using ApplicationTemplate.Models.Entities.Blog;
using ApplicationTemplate.ServiceContracts;

namespace ApplicationTemplate.Services
{
    public class BlogService : ServiceBase<BlogService>, IBlogService
    {
        private readonly Lazy<IRepository<Post>> _posts;
        private readonly Lazy<IRepository<Tag>> _tags;
        private readonly Lazy<IRepository<Comment>> _comments;
        private readonly Lazy<IRepository<Category>> _categories;

        private IRepository<Post> Posts { get { return _posts.Value; } }
        private IRepository<Tag> Tags { get { return _tags.Value; } }
        private IRepository<Comment> Comments { get { return _comments.Value; } }
        private IRepository<Category> Categories { get { return _categories.Value; } }

        public BlogService(
            IUnitOfWork context, 
            Lazy<IRepository<Category>> categories, 
            Lazy<IRepository<Comment>> comments, 
            Lazy<IRepository<Tag>> tags, 
            Lazy<IRepository<Post>> posts)
            : base(context)
        {
            _categories = categories;
            _comments = comments;
            _tags = tags;
            _posts = posts;
        }

        public Task<List<Post>> GetPosts(PagingData pagingData, bool onlyPublished = true)
        {
            var query = Posts.Query();

            if (onlyPublished)
                query = query.Where(entity => entity.PostStatus == PostStatus.Published);

            return query
                .OrderBy(entity => entity.TimeWritten)
                .ToListAsync(pagingData);
        }

        public Task<Post> GetPost(int id)
        {
            return Posts.Query()
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<Post> GetPost(string urlSegment)
        {
            return Posts.Query()
                .FirstOrDefaultAsync(entity => entity.UrlSegment == urlSegment);
        }

        public Task UpdatePost(Post post)
        {
            return Posts.UpdateAsync(post);
        }

        public async Task DeletePost(int id)
        {
            var target = await GetPost(id);

            if (target == null)
                throw new ArgumentException("id");

            await Posts.DeleteAsync(target);
        }

        public Task<Post> AddPost(Post post)
        {
            return Posts.InsertAsync(post);
        }

        public Task<List<Tag>> GetTags(PagingData pagingData)
        {
            return Tags.Query()
                .OrderBy(entity => entity.Name)
                .ToListAsync(pagingData);
        }

        public Task<Tag> GetTag(int id)
        {
            return Tags.Query()
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<Tag> GetTag(string urlSegment)
        {
            return Tags.Query()
                .FirstOrDefaultAsync(entity => entity.UrlSegment == urlSegment);
        }

        public Task UpdateTag(Tag tag)
        {
            return Tags.UpdateAsync(tag);
        }

        public async Task DeleteTag(int id)
        {
            var target = await GetTag(id);

            if (target == null)
                throw new ArgumentException("id");

            await Tags.DeleteAsync(target);
        }

        public Task<Tag> AddTag(Tag tag)
        {
            return Tags.InsertAsync(tag);
        }

        public Task<List<Category>> GetCategories(PagingData pagingData)
        {
            return Categories.Query()
                .OrderBy(entity => entity.Name)
                .ToListAsync(pagingData);
        }

        public Task<Category> GetCategory(int id)
        {
            return Categories.Query()
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<Category> GetCategory(string urlSegment)
        {
            return Categories.Query()
                .FirstOrDefaultAsync(entity => entity.UrlSegment == urlSegment);
        }

        public Task UpdateCategory(Category category)
        {
            return Categories.UpdateAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var target = await GetCategory(id);

            if (target == null)
                throw new ArgumentException("id");

            await Categories.DeleteAsync(target);
        }

        public Task<Category> AddCategory(Category category)
        {
            return Categories.InsertAsync(category);
        }

        public Task<List<Comment>> GetComments(PagingData pagingData)
        {
            return Comments.Query()
                .OrderBy(entity => entity.TimeWritten)
                .ToListAsync(pagingData);
        }

        public Task<Comment> GetComment(int id)
        {
            return Comments.Query()
                .FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task UpdateComment(Comment comment)
        {
            return Comments.UpdateAsync(comment);
        }

        public async Task DeleteComment(int id)
        {
            var target = await GetComment(id);

            if (target == null)
                throw new ArgumentException("id");

            await Comments.DeleteAsync(target);
        }

        public Task<Comment> AddComment(Comment comment)
        {
            return Comments.InsertAsync(comment);
        }
    }
}
