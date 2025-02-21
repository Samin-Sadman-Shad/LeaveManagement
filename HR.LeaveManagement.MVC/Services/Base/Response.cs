using System.Net;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
        public T Data { get; set; }

        public bool Success { get; set; }
    }
}
