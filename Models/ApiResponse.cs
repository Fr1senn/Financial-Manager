using System.Net;

namespace financial_manager.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; } = null!;

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode, string? message = null)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
            Message = message;
        }

        public static ApiResponse Succeed(string? message = null)
        {
            return new ApiResponse(true, HttpStatusCode.OK, message);
        }

        public static ApiResponse Fail(string message)
        {
            return new ApiResponse(false, HttpStatusCode.BadRequest, message);
        }

        public static ApiResponse Fail(HttpStatusCode statusCode, string message)
        {
            return new ApiResponse(false, statusCode, message);
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Result { get; set; }

        public ApiResponse(bool isSuccess, HttpStatusCode statusCode, T result, string? message = null)
            : base(isSuccess, statusCode, message)
        {
            Result = result;
        }

        public static ApiResponse<T> Succeed(T result)
        {
            return new ApiResponse<T>(true, HttpStatusCode.OK, result);
        }
    }
}
