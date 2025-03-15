namespace API.Helpers
{
    public class PaginationHeader(int currenPage, int itemsPerPage, int totalItems, int totalPages)
    {
        public int CurrentPage { get; set; } = currenPage;
        public int ItemsPerPage { get; set; } = itemsPerPage;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages { get; set; } = totalPages;

    }
}
