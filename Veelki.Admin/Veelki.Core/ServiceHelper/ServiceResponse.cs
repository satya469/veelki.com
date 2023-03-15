﻿namespace Veelki.Core.ServiceHelper
{
    public class ServiceResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
