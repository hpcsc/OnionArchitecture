
namespace OnionArchitecture.Core.Infrastructure.Repositories
{
    public class PaginationInfo
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public string BaseUrl { get; set; }
        public int TotalItems { get; set; }

        public PaginationInfo(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }
    }
}
