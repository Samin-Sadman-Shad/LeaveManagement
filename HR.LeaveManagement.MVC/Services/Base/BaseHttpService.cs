using HR.LeaveManagement.MVC.Contracts;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.MVC.Services.Base
{
    public class BaseHttpService
    {
        private  IClient _client { get; set; }
        private ILocalStorageService _localStorage { get; set;}

        public BaseHttpService(IClient client, ILocalStorageService storage)
        {
            _client = client;
            _localStorage = storage;
        }

        protected void AddBearerToken()
        {
            if (_localStorage.DoesExist("token"))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", _localStorage.getStorageValue<string>("token"));
            }
        }
    }
}
