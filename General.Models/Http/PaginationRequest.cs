namespace General.Models.Http
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SortBy { get; set; }
    }
}
