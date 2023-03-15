namespace RB444.Model.Model
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class CommonPagination
    {        
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int TotalRecord { get; set; }
        public string ShowPageInfo { get; set; }
    }
}
