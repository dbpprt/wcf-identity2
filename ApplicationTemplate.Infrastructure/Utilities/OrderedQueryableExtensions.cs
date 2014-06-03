using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ApplicationTemplate.Models.DataTransfer;

namespace ApplicationTemplate.Infrastructure.Utilities
{
    public static class OrderedQueryableExtensions
    {
        public static List<TEntity> ToList<TEntity>(this IOrderedQueryable<TEntity> query, PagingData pagingData)
        {
            if (pagingData == null)
                return query.ToList();

            var pageSize = pagingData.PageSize + 1;
            pagingData.HasPreviousPage = pagingData.Page > 1;

            var result = query.Skip((pagingData.Page - 1) * pagingData.PageSize).Take(pageSize).ToList();
            if (pageSize == result.Count())
            {
                pagingData.HasNextPage = true;
                return result.Take(pagingData.PageSize).ToList();
            }
            pagingData.HasNextPage = false;
            return result;
        }

        public static async Task<List<TEntity>> ToListAsync<TEntity>(this IOrderedQueryable<TEntity> query, PagingData pagingData)
        {
            if (pagingData == null)
                return await query.ToListAsync();

            var pageSize = pagingData.PageSize + 1;
            pagingData.HasPreviousPage = pagingData.Page > 1;

            var result = await query.Skip((pagingData.Page - 1) * pagingData.PageSize).Take(pageSize).ToListAsync();
            if (pageSize == result.Count())
            {
                pagingData.HasNextPage = true;
                return result.Take(pagingData.PageSize).ToList();
            }
            pagingData.HasNextPage = false;
            return result;
        }
    }
}
