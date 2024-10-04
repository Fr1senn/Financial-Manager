using System.Net;

namespace financial_manager.Entities.Shared
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

        public static ApiResponse Succeed(
            string? message = null,
            HttpStatusCode statusCode = HttpStatusCode.OK
        )
        {
            return new ApiResponse(true, statusCode, message);
        }

        public static ApiResponse Fail(
            string message,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest
        )
        {
            return new ApiResponse(false, statusCode, message);
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public PagedResult(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T? Result { get; set; }

        public ApiResponse(
            bool isSuccess,
            HttpStatusCode statusCode,
            T? result,
            string? message = null
        )
            : base(isSuccess, statusCode, message)
        {
            Result = result;
        }

        public static ApiResponse<T> Succeed(T? result)
        {
            return new ApiResponse<T>(true, HttpStatusCode.OK, result);
        }

        public static ApiResponse<PagedResult<T>> Succeed(IEnumerable<T> data, int totalCount)
        {
            var pagedResult = new PagedResult<T>(data, totalCount);
            return new ApiResponse<PagedResult<T>>(true, HttpStatusCode.OK, pagedResult);
        }
    }
}
