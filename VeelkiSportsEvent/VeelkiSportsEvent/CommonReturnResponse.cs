using System;

namespace VeelkiSportsEvent
{
    public class CommonReturnResponse
    {
        public Boolean IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
