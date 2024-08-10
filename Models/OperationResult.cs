using financial_manager.Models.Enums;

namespace financial_manager.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public HttpResponseCode HttpResponseCode { get; set; }
        public string? Message { get; set; }

        public OperationResult(bool isSuccess, HttpResponseCode httpResponseCode, string? message = null)
        {
            IsSuccess = isSuccess;
            HttpResponseCode = httpResponseCode;
            Message = message;
        }
    }
}
