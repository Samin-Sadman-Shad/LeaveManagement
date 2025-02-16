namespace HR.LeaveManagement.MVC.Services.Base
{
    /// <summary>
    /// Partial classes need to be inside same namespace
    /// </summary>
    public partial class Client:IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
    }
}
