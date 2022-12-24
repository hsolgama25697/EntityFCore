namespace EFCore.Models
{
    public class Pager
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pager()
        {

        }
        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startpage = currentPage - 5;
            int endpage = currentPage + 5;

            if (startpage <= 0)
            {
                endpage = endpage - (startpage - 1);
                startpage = 1;
            }

            if (endpage > startpage)
            {
                endpage = totalPages;
                if (endpage > 10)
                {
                    startpage = endpage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startpage;
            EndPage = endpage;
        }
    }
}
