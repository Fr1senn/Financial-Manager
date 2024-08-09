namespace financial_manager.Models
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }

        public OperationResult(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }
    }
}
