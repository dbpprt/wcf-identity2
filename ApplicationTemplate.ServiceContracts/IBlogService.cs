using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;
using ApplicationTemplate.Models.Entities.Blog;

namespace ApplicationTemplate.ServiceContracts
{
    [ServiceContract]
    public interface IBlogService
    {
        [OperationContract]
        Task<List<Post>> GetPosts(PagingData pagingData, bool onlyPublished = true);

        [OperationContract]
        Task<Post> GetPost(int id);

        [OperationContract]
        Task<Post> GetPost(string urlSegment);

        [OperationContract]
        Task UpdatePost(Post post);

        [OperationContract]
        Task DeletePost(int id);

        [OperationContract]
        Task<Post> AddPost(Post post);

        [OperationContract]
        Task<List<Tag>> GetTags(PagingData pagingData);

        [OperationContract]
        Task<Tag> GetTag(int id);

        [OperationContract]
        Task<Tag> GetTag(string urlSegment);

        [OperationContract]
        Task UpdateTag(Tag tag);

        [OperationContract]
        Task DeleteTag(int id);

        [OperationContract]
        Task<Tag> AddTag(Tag tag);

        [OperationContract]
        Task<List<Category>> GetCategories(PagingData pagingData);

        [OperationContract]
        Task<Category> GetCategory(int id);

        [OperationContract]
        Task<Category> GetCategory(string urlSegment);

        [OperationContract]
        Task UpdateCategory(Category category);

        [OperationContract]
        Task DeleteCategory(int id);

        [OperationContract]
        Task<Category> AddCategory(Category category);

        [OperationContract]
        Task<List<Comment>> GetComments(PagingData pagingData);

        [OperationContract]
        Task<Comment> GetComment(int id);

        [OperationContract]
        Task UpdateComment(Comment comment);

        [OperationContract]
        Task DeleteComment(int id);

        [OperationContract]
        Task<Comment> AddComment(Comment comment);
    }
}
