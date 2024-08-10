using financial_manager.Models.Enums;

namespace financial_manager.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public HttpResponseCode HttpResponseCode { get; set; }
        public string? Message { get; set; }

        public OperationResult(bool success, string? message = null)
        public OperationResult(bool isSuccess, HttpResponseCode httpResponseCode, string? message = null)
        {
            Success = success;
            HttpResponseCode = httpResponseCode;
            Message = message;
        }
    }
}
