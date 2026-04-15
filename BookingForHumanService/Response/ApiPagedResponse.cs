namespace BookingForHumanService.API.Response
{
    public class ApiPagedResponse<T> : ApiResponse<T>
    {
        public  T Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
