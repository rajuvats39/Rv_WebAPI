namespace Rv_WebAPI.Utility
{
    public class ResponseMessage
    {
        public bool? IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }  
    }
}
