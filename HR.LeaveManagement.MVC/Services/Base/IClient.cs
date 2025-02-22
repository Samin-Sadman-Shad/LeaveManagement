namespace HR.LeaveManagement.MVC.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
        public bool ReadResponse { get; set; }
    }
}
