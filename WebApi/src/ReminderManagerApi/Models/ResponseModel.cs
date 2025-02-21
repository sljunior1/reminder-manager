using System.Net;

namespace ReminderManagerApi.Models
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public bool Success { get; set; } = false;
    }
}
