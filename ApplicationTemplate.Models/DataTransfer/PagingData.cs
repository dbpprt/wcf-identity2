namespace ApplicationTemplate.Models.DataTransfer
{
    public class PagingData
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PagingData(int? page = 1, int pageSize = 25)
        {
            PageSize = pageSize;
            Page = page.HasValue ? page.Value : 1;
        }
    }
}
