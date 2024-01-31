namespace Common.Dto
{
    public class PaginationRequest<T>
    {
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;

        public T Request { get; set; }

        public PaginationRequest(int pageNumber, int pageSize = 10, T request = default)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Request = request;
        }

    }
}
