namespace BetSettle
{
    public class CommonReturnResponse
    {
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
